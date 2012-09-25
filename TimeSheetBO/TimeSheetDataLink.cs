using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public abstract class TimeSheetDataLink
    {
        private static bool _traceLogEnabled = false;
        private readonly IDictionary _fieldMappings = new Hashtable();
        private readonly IList _attributeNames = new ArrayList();
        protected readonly Type _TimeSheetBaseType;
        protected string _tableName;
        protected string _PrimaryKey = "guidfield";
        protected string _lockingHint = null;
        protected static readonly IList EmptyList = null;

        private static Logger _log = new Logger("MQBusinessLayer.log");
        private static Logger _traceLog = new Logger("MQTrace.log");
        private static Logger _performanceLog = new Logger("MQPerformace.log");
        public abstract TimeSheetBase Init();

        protected string LockingHintClause 
        {
            get 
            {
                if (_lockingHint == null) 
                {
                    return string.Empty;
                } 
                else 
                {
                    return string.Format(" WITH ({0}) ", _lockingHint);
                }
            }
        }

        public static IDataReader ExecuteReader(IDbCommand command) 
        {
            UInt64 start = HiResTimer.CurrentCount;
            IDataReader reader = command.ExecuteReader();
            LogCommandExecutionTime(HiResTimer.GetSeconds(start), command);
            return reader;
        }

        public static int ExecuteNonQuery(IDbCommand command) 
        {
            UInt64 start = HiResTimer.CurrentCount;
            int result = command.ExecuteNonQuery();
            LogCommandExecutionTime(HiResTimer.GetSeconds(start), command);
            return result;
        }

        public static object ExecuteScalar(IDbCommand command) 
        {
            UInt64 start = HiResTimer.CurrentCount;
            object result = command.ExecuteScalar();
            LogCommandExecutionTime(HiResTimer.GetSeconds(start), command);
            return result;
        }

        protected void AddFieldMapping(string attributeName, string fieldName, bool hasDefault, bool isRequired) 
        {
            TimeSheetFieldMap map = new TimeSheetFieldMap(_TimeSheetBaseType, attributeName, fieldName, hasDefault, isRequired);
            _fieldMappings[attributeName] = map;
            _attributeNames.Add(attributeName);
        }

        public IDictionary FieldMappings 
        {
            get 
            {
                return _fieldMappings;
            }
        }

        public IList AttributeNames 
        {
            get 
            {
                return _attributeNames;
            }
        }

        public string TableName 
        {
            get 
            {
                return _tableName;
            }
        }

        public string PrimaryKey 
        {
            get 
            {
                return _PrimaryKey;
            }
        }

        public bool traceLogEnabled
        {
            get { return _traceLogEnabled; }
            set { _traceLogEnabled = value; }
        }

        protected TimeSheetDataLink(Type TimeSheetBaseType, string tableName) 
        {
            _TimeSheetBaseType = TimeSheetBaseType;
            _tableName = tableName;
            _PrimaryKey = "guidfield";
        }

        public virtual ICollection FindReferencesTo(TimeSheetContext context, TimeSheetBase obj) 
        {
            return EmptyList;
        }

        public List<TimeSheetBase> Load(TimeSheetBase baseObj, TimeSheetContext contextParam, string where, string orderby, params object[] args)
        {
            List<TimeSheetBase> Result = new List<TimeSheetBase>();
            TimeSheetContext context = TimeSheetContext.Create(contextParam);
            string sql = string.Empty;
            try
            {
                sql = CreateSelectStatement(where, orderby);
                SqlCommand command = context.CreateCommand(sql);
                if (args.Length > 1)
                {
                    int maxLength = (args.Length / 2);
                    maxLength = maxLength != 0 ? maxLength - 1 : 0;
                    int SCnt = 0;
                    for (int PCnt = 0; PCnt <= maxLength; PCnt++)
                    {
                        SqlParameter oParam = new SqlParameter("@" + args[SCnt].ToString(), args[SCnt+1]);
                        command.Parameters.Add(oParam);
                        SCnt += 2;
                    }
                }
                context.OpenConnection();

                using (IDataReader dataReader = ExecuteReader(command))
                {
                    while(dataReader.Read())
                    {
                        TimeSheetBase obj = (TimeSheetBase)Activator.CreateInstance(baseObj.GetType(), new object[] { baseObj.User });
                        obj.SetAttributes(dataReader);
                        obj.SetLoaded();
                        Result.Add(obj);
                    }
                }
            }
            catch (SqlException e)
            {
                LogSqlException("Exception caught in Load().", e);
            }
            catch (Exception)
            {
                LogSqlCommand(LogLevel.Error, sql, null);
            }
            finally
            {
                context.CloseConnection(contextParam);
            }
            return Result;
        }

        public void LoadSingle(TimeSheetBase o, TimeSheetContext contextParam, string where, params object[] args)
        {
            _log.Debug("Load stack trace: " + new StackTrace(true));
            TimeSheetContext context = TimeSheetContext.Create(contextParam);
            string sql = string.Empty;
            try
            {
                sql = CreateSelectStatement(where);
                SqlCommand command = context.CreateCommand(sql);
                if (args.Length > 1)
                {
                    int Cnt = 0;
                    for (int PCnt = 0; PCnt <= args.Length % 2; PCnt++)
                    {
                        SqlParameter oParam = new SqlParameter("@" + args[PCnt].ToString(), args[Cnt + 1]);
                        command.Parameters.Add(oParam);
                        Cnt += 2;
                    }
                }
                context.OpenConnection();

                using (IDataReader dataReader = ExecuteReader(command))
                {
                    if (dataReader.Read())
                    {
                        o.SetAttributes(dataReader);
                        o.SetLoaded();
                    }
                    else
                    {
                        string message = string.Format("No object read from database; table = {0}, guidfield = {1}",TableName,PrimaryKey);
                    }
                }
            }
            catch (SqlException e)
            {
                LogSqlException("Exception caught in Load().", e);
            }
            catch (Exception)
            {
                LogSqlCommand(LogLevel.Error, sql, null);
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }
        }



        private string CreateInsertStatement(TimeSheetBase o)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ").Append(TableName);
            sql.Append(LockingHintClause);
            sql.Append(" ( ");
            TimeSheetBase clean = o.CleanObject;
            bool first = true;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
            {
                object value = mapping.FieldInfo.GetValue(o);
                if (clean == null ||
                    !object.Equals(value, mapping.FieldInfo.GetValue(clean)))
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sql.Append(", ");
                    }
                    sql.Append(mapping.FieldName);
                }
            }

            sql.Append(" ) VALUES ( ");

            first = true;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
            {
                object value = mapping.FieldInfo.GetValue(o);
                if (clean == null ||
                    !object.Equals(value, mapping.FieldInfo.GetValue(clean)))
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sql.Append(", ");
                    }
                    sql.Append("@").Append(mapping.FieldName);
                }
            }
            sql.Append(" )");

            string sqlString = sql.ToString();
            _log.Debug("SQL Insert Statement: " + sqlString);
            return sqlString;
        }

        // Inserts a MQ object into the database
        public bool Insert(TimeSheetBase o, TimeSheetContext contextParam) 
        {
            bool result = false;
            TimeSheetContext context = TimeSheetContext.Create(contextParam);
            SqlCommand command = null;
            string sql = string.Empty;

            try
            {
                TimeSheetBase clean = o.CleanObject;
                sql = CreateInsertStatement(o);
                command = context.CreateCommand(sql);
                foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
                {
                    _log.Debug("Create(): Getting direct value for " + mapping.FieldName);
                    object value = mapping.FieldInfo.GetValue(o);
                    if (clean == null ||
                        !object.Equals(value, mapping.FieldInfo.GetValue(clean)))
                    {
                        if (value == null)
                        {
                            value = DBNull.Value;
                        }
                        string paramName = "@" + mapping.FieldName;
                        SqlParameter param = new SqlParameter(paramName, value);
                        _log.Debug("Create(): SqlParam = " + param);
                        command.Parameters.Add(param);
                        _log.Debug("Create(): SqlParameter added");
                    }
                }

                context.OpenConnection();
                ExecuteNonQuery(command);
                result = true;
            }
            catch (SqlException e)
            {
                LogSqlException("Exception caught in Create().", e);
            }
            catch (Exception)
            {
                LogSqlCommand(LogLevel.Error, sql, command);
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }

            return result;
        }

        public bool Update(TimeSheetBase o, TimeSheetContext contextParam) 
        {
            bool result = false;
            TimeSheetContext context = TimeSheetContext.Create(contextParam);
            SqlCommand command = null;
            string sql = string.Empty;
            try
            {
                command = context.CreateCommand(string.Empty);

                TimeSheetBase clean = o.CleanObject;
                ArrayList fieldNamesToSet = new ArrayList();
                object guidvalue = null;

                foreach (TimeSheetFieldMap fieldMap in _fieldMappings.Values)
                {
                    _log.Debug("Update: fieldMap.AttributeName = " + fieldMap.AttributeName);
                    if (fieldMap.FieldName == PrimaryKey)
                    {
                        guidvalue = fieldMap.FieldInfo.GetValue(o); 
                    }
                    // if the attribute value is the same as the clean object's value, don't add the parameter
                    object attributeValue = fieldMap.FieldInfo.GetValue(o);
                    if (clean == null ||
                        !object.Equals(attributeValue, fieldMap.FieldInfo.GetValue(clean)))
                    {
                        fieldNamesToSet.Add(fieldMap.FieldName);
                        command.Parameters.Add(new SqlParameter("@" + fieldMap.FieldName, attributeValue));
                        _log.Debug("Update(): Fieldname: " + fieldMap.FieldName + ", Value: " + attributeValue);
                    }
                }

                if (fieldNamesToSet.Count == 0)
                {
                    return false;  // No changes persisted
                }

                // Add the guidfield parameter
                SqlParameter guidParam = new SqlParameter("@guidfield", SqlDbType.UniqueIdentifier, 40);
                guidParam.Value = guidvalue;
                command.Parameters.Add(guidParam);

                // Set the sql for the command to run
                command.CommandText = CreateUpdateStatement(o, fieldNamesToSet);
                context.OpenConnection();
                ExecuteNonQuery(command);
                result = true;
            }
            catch (SqlException e)
            {
                LogSqlException("Exception caught in Update(TimeSheetBase, TimeSheetContext).", e);
            }
            catch (Exception)
            {
                LogSqlCommand(LogLevel.Error, sql, command);
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }

            return result;
        }

        public static int Update(TimeSheetContext contextParam, string updateStatement, params SqlParameter [] sqlParams) 
        {
            TimeSheetContext context = TimeSheetContext.Create(contextParam);

            int rowsUpdated=0;
            SqlCommand command = null;

            try 
            {
                command = context.CreateCommand(updateStatement);
                if (sqlParams!=null) 
                {
                    foreach (SqlParameter param in sqlParams) 
                    {
                        command.Parameters.Add(param);
                    }
                }

                context.OpenConnection();

                rowsUpdated = ExecuteNonQuery(command);
            } 
            catch (Exception e) 
            {
                LogSqlCommand(LogLevel.Error, updateStatement, command);
                if (e is SqlException)
                {
                    LogSqlException("Exception caught in Update(TimeSheetContext, string).", (SqlException) e);
                }
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }

            return rowsUpdated;
        }

        public bool Delete(TimeSheetBase o, TimeSheetContext contextParam) 
        {
            bool result = false;
            TimeSheetContext context = TimeSheetContext.Create(contextParam);
            SqlCommand command = null;
            string deleteStatement = string.Empty;
            try
            {
                deleteStatement = CreateDeleteStatement();
                command = context.CreateCommand(deleteStatement);
                SqlParameter guidParam = new SqlParameter("@guidfield", SqlDbType.UniqueIdentifier, 40);
                guidParam.Value = this.fieldValue(o, this.PrimaryKey);
                command.Parameters.Add(guidParam);

                context.OpenConnection();
                ExecuteNonQuery(command);
                result = true;
            }
            catch (SqlException e)
            {
                LogSqlException("Exception caught in Delete().", e);
            }
            catch (Exception)
            {
                LogSqlCommand(LogLevel.Error, deleteStatement, command);
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }

            return result;
        }

        public int Count(TimeSheetContext contextParam, string whereClause, params SqlParameter[] sqlParams) 
        {
            TimeSheetContext context = TimeSheetContext.Create(contextParam);

            SqlCommand command = null;
            StringBuilder sql = null;

            int count = 0;
            try 
            {
                sql = new StringBuilder();
                sql.Append("SELECT count(*) FROM ").Append(TableName);
                sql.Append(LockingHintClause);
                sql.Append(" WHERE ").Append(whereClause);
                _log.Debug("Count: sql = " + sql);

                command = context.CreateCommand(sql.ToString());
                if (sqlParams!=null) 
                {
                    foreach (SqlParameter param in sqlParams) 
                    {
                        command.Parameters.Add(param);
                    }
                }

                context.OpenConnection();

                object result = ExecuteScalar(command);
                if (result==null || (result is DBNull)) 
                {
                    count = 0;
                } 
                else 
                {
                    count = (int)result;
                }
            } 
            catch (Exception e) 
            {
                if (sql != null) 
                {
                    LogSqlCommand(LogLevel.Error, sql.ToString(), command);
                }
                if (e is SqlException)
                {
                    LogSqlException("Exception caught in Count().", (SqlException) e);
                }                
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }
            return count;
        }

        public decimal Sum(TimeSheetContext contextParam, string whereClause, string sumField, params SqlParameter[] sqlParams) 
        {
            TimeSheetContext context = TimeSheetContext.Create(contextParam);

            SqlCommand command = null;
            StringBuilder sql = null;

            decimal sum = 0m;
            try 
            {
                sql = new StringBuilder();
                sql.Append("SELECT sum(").Append(sumField).Append(") FROM ").Append(TableName);
                sql.Append(LockingHintClause);
                sql.Append(" WHERE ").Append(whereClause);
                _log.Debug("Sum: sql = " + sql);

                command = context.CreateCommand(sql.ToString());
                foreach (SqlParameter sqlParam in sqlParams) 
                {
                    command.Parameters.Add(sqlParam);
                }

                context.OpenConnection();

                object result = ExecuteScalar(command);
                if (result==null || (result is DBNull)) 
                {
                    sum = 0m;
                } 
                else 
                {
                    sum = (decimal)result;
                }
            } 
            catch (Exception e) 
            {
                if (sql != null) 
                {
                    LogSqlCommand(LogLevel.Error, sql.ToString(), command);
                }
                if (e is SqlException)
                {
                    LogSqlException("Exception caught in Sum().", (SqlException) e);
                }
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }
            return sum;
        }

        public int Delete(TimeSheetContext contextParam, string whereClause, params SqlParameter[] sqlParams) 
        {
            TimeSheetContext context = TimeSheetContext.Create(contextParam);

            SqlCommand command = null;
            StringBuilder sql = null;

            int count = 0;
            try 
            {
                sql = new StringBuilder();
                sql.Append("DELETE FROM ").Append(TableName);
                sql.Append(LockingHintClause);
                if (whereClause.Length>0) 
                {
                    sql.Append(" WHERE ").Append(whereClause);
                }
                _log.Debug("Delete: sql = " + sql);

                command = context.CreateCommand(sql.ToString());
                if (sqlParams!=null) 
                {
                    foreach (SqlParameter param in sqlParams) 
                    {
                        command.Parameters.Add(param);
                    }
                }

                context.OpenConnection();
                count = ExecuteNonQuery(command);

            } 
            catch (Exception e) 
            {
                if (sql != null) 
                {
                    LogSqlCommand(LogLevel.Error, sql.ToString(), command);
                }
                if (e is SqlException)
                {
                    LogSqlException("Exception caught in Delete().", (SqlException)e);
                }
            } 
            finally 
            {
                context.CloseConnection(contextParam);
            }
            return count;
        }

        public int DeleteAll(TimeSheetContext context) 
        {
            return Delete(context, string.Empty);
        }

        public IList Find(TimeSheetContext contextParam, string whereClause, string orderByClause, params SqlParameter[] sqlParams)
        {
            TimeSheetContext context = TimeSheetContext.Create(contextParam);

            IDataReader dataReader = null;
            SqlCommand command = null;
            string sql = string.Empty;

            ArrayList list = new ArrayList();

            try
            {
                sql = CreateSelectStatement(whereClause, orderByClause);
                _log.Debug("Find: sql = " + sql);

                command = context.CreateCommand(sql);
                if (sqlParams != null)
                {
                    foreach (SqlParameter param in sqlParams)
                    {
                        command.Parameters.Add(param);
                    }
                }

                context.OpenConnection();

                dataReader = ExecuteReader(command);
                while (dataReader.Read())
                {
                    TimeSheetBase obj = Init();
                    obj.SetAttributes(dataReader);

                    obj.SetLoaded();
                    list.Add(obj);
                }
            }
            catch (Exception e)
            {
                LogSqlCommand(LogLevel.Error, sql, command);
                if (e is SqlException)
                {
                    LogSqlException("Exception caught in Find().", (SqlException) e);
                }
            }
            finally
            {
                dataReader.Close();
                context.CloseConnection(contextParam);
            }

            _log.Debug("Find: returning " + list.Count + " items from '" + sql + "'");
            return list;
        }

        public TimeSheetBase FindSingle(TimeSheetContext context, string whereClause, params SqlParameter[] sqlParams)
        {
            TimeSheetBase obj = null;

            IList matches = Find(context, whereClause, null, sqlParams);
            if (matches.Count > 1)
            {
                throw new ApplicationException("More than one record was matched.");
            }

            if (matches.Count == 1)
            {
                obj = (TimeSheetBase)matches[0];
            }

            return obj;
        }

        public virtual string SerializeHeader() 
        {
            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < AttributeNames.Count; i++) 
            {
                buffer.Append(((TimeSheetFieldMap)FieldMappings[AttributeNames[i]]).FieldName);
                if (i < AttributeNames.Count - 1) 
                {
                    buffer.Append(",");
                }
            }

            return buffer.ToString();
        }

        protected static void LogSqlException(string message, SqlException exception) 
        {
            _log.Error(message, exception);

            for (int i = 0; i < exception.Errors.Count; i++) 
            {
                _log.Error("Error #{0}: Number={1}, State={2}, Class={3}, Message='{4}'",
                           i, exception.Errors[i].Number, exception.Errors[i].State, exception.Errors[i].Class, exception.Errors[i].Message);
            }
        }

		protected static void LogSqlCommand(LogLevel logLevel, string sql, SqlCommand command) 
		{
            LogSqlCommand(_log, logLevel, sql, command);
            if (_traceLogEnabled) 
            {
                LogSqlCommand(_traceLog, logLevel, sql, command);
            }
        }

		private static void LogSqlCommand(Logger logger, LogLevel logLevel, string sql, SqlCommand command) 
		{
            if (sql != null && sql != string.Empty) 
            {
                logger.Log(logLevel, sql, null);
            }
            if (command != null) 
            {
                if (command.Transaction!=null) 
                {
                    logger.Log(logLevel, "SqlTransaction = " + command.Transaction.GetHashCode(), null);
                } 
                else 
                {
                    logger.Log(logLevel, "SqlTransaction = null", null);
                }

                foreach (SqlParameter param in command.Parameters) 
                {
                    logger.Log(logLevel, param.ParameterName + " = '" + param.Value + "'", null);
                }
            }
        }

		protected static void LogCommandExecutionTime(double executeTimeSeconds, IDbCommand command) 
		{
            try 
            {
                string message;
                if (_traceLogEnabled && _traceLog.IsDebugEnabled) 
                {
                    IDbTransaction sqlTrans = command.Transaction;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("\t{0:F2}\t{1}\t{2}", 
                                            executeTimeSeconds * 1000, 
                                            (sqlTrans!=null ? sqlTrans.GetHashCode() : 0), 
                                            command.CommandText);

                    // Add command parameter names and values
                    foreach (IDataParameter param in command.Parameters) 
                    {
                        sb.AppendFormat("{0}\t\t\t\t\t\t{1} = {2}", Environment.NewLine, param.ParameterName, param.Value);
                    }
                    message = sb.ToString();
                    LogSqlCommand(_traceLog, LogLevel.Debug, message, null);
                }

                if (executeTimeSeconds > 6000) 
                {
                    message = string.Format("The following sql statement took {0:F2} seconds to execute:{1}{2}{3}",
                                            executeTimeSeconds, Environment.NewLine, command.CommandText, new StackTrace(1,true));
                    _performanceLog.Warn(message);
                }
            } 
            catch (Exception e) 
            {
                // Don't want no exception thrown from this method, no-how
                Console.Error.WriteLine("Exception caught in LogCommandExecutionTime()." + e);
            }
        }

        private string CreateSelectStatement() 
        {
            return CreateSelectStatement("");
        }

        private string CreateSelectStatement(Guid guidfield)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" WHERE ").Append(PrimaryKey).Append(" = @guidfield");

            string sqlString = sql.ToString();
            _log.Debug("SQL Select Statement: " + sqlString);
            return CreateSelectStatement(sqlString);
        }

        private string CreateSelectStatement(string whereClause, string orderByClause) 
        {
            StringBuilder sql = new StringBuilder();
            if (! string.IsNullOrEmpty(whereClause)) 
            {
                sql.Append(" WHERE ").Append(whereClause);
            }
            if (! string.IsNullOrEmpty(orderByClause)) 
            {
                sql.Append(" ORDER BY ").Append(orderByClause);
            }

            string sqlString = sql.ToString();
            _log.Debug("SQL Select Statement: " + sqlString);
            return CreateSelectStatement(sqlString);
        }

        private string CreateSelectStatement(string whereGroupClause) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ").Append(FieldNamesForSql());
            sql.Append(" FROM ").Append(TableName);
            sql.Append(LockingHintClause);
            sql.Append(whereGroupClause);

            string sqlString = sql.ToString();
            _log.Debug("SQL Select Statement: " + sqlString);
            return sqlString;
        }

        private string FieldNamesForSql() 
        {
            StringBuilder sql = new StringBuilder();
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values) 
            {
                if (sql.Length > 0)
                {
                    sql.Append(", ");
                }
                sql.Append(mapping.FieldName);
            }
            return sql.ToString();
        }

        private string CreateUpdateStatement(TimeSheetBase o, IList fieldNamesToSet) 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ").Append(TableName);
            sql.Append(LockingHintClause);
            sql.Append(" SET ");
            bool first = true;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values) 
            {
                if (mapping.FieldName == PrimaryKey) 
                {
                    continue;
                }
                if (o.CleanObject==null ||
                    fieldNamesToSet.Contains(mapping.FieldName)) 
                {
                    if (first) 
                    {
                        first = false;
                    } 
                    else 
                    {
                        sql.Append(", ");
                    }
                    sql.Append(mapping.FieldName).Append(" = @").Append(mapping.FieldName);
                }
            }

            sql.Append(" WHERE ").Append(PrimaryKey).Append(" = @guidfield");

            string sqlString = sql.ToString();
            _log.Debug("SQL Update Statement: " + sqlString);
            return sqlString;
        }

        private string CreateDeleteStatement() 
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE ").Append(TableName);
            sql.Append(" WHERE ").Append(PrimaryKey).Append(" = @guidfield");

            string sqlString = sql.ToString();
            _log.Debug("SQL Delete Statement: " + sqlString);
            return sqlString;
        }

        protected static string StringList(IEnumerable strings) 
        {
            StringBuilder list = new StringBuilder();
            list.Append("(");
            bool first = true;
            foreach (string s in strings) 
            {
                if (first) 
                {
                    first = false;
                } 
                else 
                {
                    list.Append(", ");
                }
                list.Append("'").Append(s).Append("'");
            }
            list.Append(")");
            return list.ToString();
        }

        protected static SqlParameter [] GetguidSqlParams(IEnumerable objects) 
        {
            ArrayList sqlParams = new ArrayList();
            int index = 0;
            foreach (TimeSheetBase obj in objects) 
            {
                SqlParameter param = new SqlParameter("@guidfield" + index++, SqlDbType.UniqueIdentifier, 40);
                param.Value = obj.GetDataLink().fieldValue(obj, obj.GetDataLink().PrimaryKey);
                sqlParams.Add(param);
            }
            return(SqlParameter[])sqlParams.ToArray(typeof(SqlParameter));
        }

        public bool findField(string fieldName)
        {
            bool result = false;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
            {
                result = mapping.FieldName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase);
                if (result) break;
            }
            return result;
        }

        public object fieldValue(TimeSheetBase obj, string fieldName)
        {
            bool result = false;
            object fieldVal = null;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
            {
                result = mapping.FieldName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase);
                if (result)
                {
                    fieldVal = mapping.FieldInfo.GetValue(obj);
                    break;
                }
            }
            return fieldVal;
        }

        public TimeSheetFieldMap TimeSheetField(TimeSheetBase obj, string fieldName)
        {
            TimeSheetFieldMap result = null;
            foreach (TimeSheetFieldMap mapping in _fieldMappings.Values)
            {
                if(mapping.FieldName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = mapping;
                    break;
                }
            }
            return result;
        }
    }
}
