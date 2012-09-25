using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Collections.Generic;

using TimeSheetUtil;

namespace TimeSheetBO
{
    
    [Serializable]
    public abstract class TimeSheetBase
    {
        private bool _IsLoaded;
        private bool _IsDirty;
        private bool _IsNew;
        private bool _IsDeleted;
        private TimeSheetBase _cleanObject = null;
        private static Logger _log = new Logger("TimeSheetBase.log");
        private CurrentUser _user;

        public CurrentUser User
        {
            get { return _user; }
            set { _user = value; }
        }

        public TimeSheetBase Clone()
        {
            return (TimeSheetBase)MemberwiseClone();
        }

        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; }
        }

        public bool IsDirty
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; }
        }

        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }

        public bool IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        protected TimeSheetBase()
        {
            _IsNew = true;
        }

        public TimeSheetBase(CurrentUser User)
        {
            _user = User;
        }

        internal TimeSheetBase CleanObject
        {
            get
            {
                return _cleanObject;
            }
        }

        internal void SetLoaded()
        {
            _IsLoaded = true;
            _IsDirty = false;
            _IsNew = false;
            _IsDeleted = false;

            _cleanObject = null;
        }

        protected void SetDirty()
        {
            _log.Debug("SetDirty: Setting dirty flag");
            _IsDirty = true;
            if (_cleanObject == null)
            {
                // Save a copy of the clean object for later updating
                _cleanObject = (TimeSheetBase)this.MemberwiseClone();
            }
        }

        protected void Set(ref object instanceVar, object newValue)
        {
            if (!object.Equals(instanceVar, newValue))
            {
                _log.Debug("Set: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetString(ref string instanceVar, string newValue)
        {
            if (!object.Equals(instanceVar, newValue))
            {
                _log.Debug("SetString: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetString(ref string instanceVar, char newValue)
        {
            SetString(ref instanceVar, new string(new char[] { newValue }));
        }

        protected void SetInt(ref int instanceVar, int newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetInt: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetInt32(ref int instanceVar, int newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetInt: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetLong(ref long instanceVar, long newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetLong: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetChar(ref char instanceVar, char newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetChar: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetGuid(ref Guid instanceVar, Guid newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetGuid: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetBool(ref bool instanceVar, bool newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetBool: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetBoolean(ref bool instanceVar, bool newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetBoolean: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }
        
        protected void SetDecimal(ref decimal instanceVar, decimal newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetDecimal: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        protected void SetDateTime(ref DateTime instanceVar, DateTime newValue)
        {
            if (instanceVar != newValue)
            {
                _log.Debug("SetDateTime: instanceVar={0}, newValue={1}", instanceVar, newValue);
                SetDirty();
                instanceVar = newValue;
            }
        }

        public override string ToString()
        {
            return GetType().Name + " #";
        }

        public abstract TimeSheetDataLink GetDataLink();
        
        public void Load()
        {
            //TODO: Find The Methods applied and call
        }

        public void LoadSingle(TimeSheetBase obj, string where, params object[] args)
        {            
            GetDataLink().LoadSingle(obj, (this.User == null) ? null : this.User.userContext, where, args);
        }

        public void Load(TimeSheetContext context)
        {
            if (_IsLoaded)
            {
                return;
            }

            Refresh(context);
        }

        public List<TimeSheetBase> Load(string where, string orderby, params object[] args)
        {
            return GetDataLink().Load(this, (this.User == null) ? null : this.User.userContext, where, orderby, args);
        }

        public void Refresh()
        {
            Refresh(null);
        }

        public void Refresh(TimeSheetContext context)
        {
            _log.Debug("Refresh: loading " + GetType().FullName);
            GetDataLink().LoadSingle(this, context, "");
            SetLoaded();
        }

        public bool Save()
        {
            return Save(this.User.userContext);
        }

        public bool Save(TimeSheetContext context)
        {
			bool result = false;
            if (_IsNew)
            {
                result = Insert(context);
            }
            else
            {
                result = Update(context);
            }
			return result;
        }

        public bool Insert()
        {
            return Insert(this.User.userContext);
        }

        public virtual bool Insert(TimeSheetContext context)
        {
			bool result = false;
            result = GetDataLink().Insert(this, context);			

            _IsLoaded = true;
            _IsDirty = false;
            _IsNew = false;
            _IsDeleted = false;

            _cleanObject = null;
			
			return result;
        }

        public bool Update()
        {
            return Update(this.User.userContext);
        }

        public bool Update(TimeSheetContext context)
        {
            if (!_IsDirty)
            {
                return false;
            }

            bool result = GetDataLink().Update(this, context);
            _IsLoaded = true;
            _IsDirty = false;
            _IsNew = false;
            _IsDeleted = false;

            _cleanObject = null;
            return result;
        }

        public bool Delete()
        {
            return Delete(this.User.userContext);
        }

        public bool Delete(TimeSheetContext context)
        {
			bool result = false;
            if (!_IsDirty)
            {
                return false;
            }
            result = GetDataLink().Delete(this, context);

            _IsLoaded = false;
            _IsDirty = false;
            _IsNew = false;
            _IsDeleted = true;
			
			return result;
        }

        internal void SetAttributes(IDataReader reader)
        {
            // allow access to private fields
            ReflectionPermission perm = new ReflectionPermission(ReflectionPermissionFlag.NoFlags);
            perm.Demand();
            perm.Assert();

            try
            {
                IDictionary TimeSheetFieldMappings = GetDataLink().FieldMappings;

                _log.Debug("SetAttributes: starting for loop (" + this.GetType().FullName + ")");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    try
                    {
                        // Get the field mapping entry corresponding to the database column name
                        TimeSheetFieldMap TimeSheetFieldMap = null;
                        foreach (TimeSheetFieldMap TimeSheetFieldMap2 in TimeSheetFieldMappings.Values)
                        {
                            if (TimeSheetFieldMap2.FieldName == reader.GetName(i))
                            {
                                TimeSheetFieldMap = TimeSheetFieldMap2;
                                break;
                            }
                        }

                        if (TimeSheetFieldMap == null)
                        {
                            //throw new ApplicationException(string.Format("Could not locate {0} in field mappings.", reader.GetName(i)));
                        }

                        // Read the object from the database.  This may be a plain value
                        // Enum stored as an integer in the database
                        object value;
                        if (!reader.IsDBNull(i))
                        {
                            if (TimeSheetFieldMap.IsEnum)
                            {
                                // the database field holds the int value for an enum
                                _log.Debug("SetAttributes: Setting enum value for " + TimeSheetFieldMap.AttributeName);
                                object enumValue = GetObjectFromDatabase(reader, i);
                                value = Enum.ToObject(TimeSheetFieldMap.EnumType, enumValue);
                            }
                            else
                            {
                                // the database field holds the attribute value
                                _log.Debug("SetAttributes: Setting direct value for " + TimeSheetFieldMap.AttributeName);
                                value = GetObjectFromDatabase(reader, i);
                            }
                        }
                        else
                        {
                            value = null;
                        }

                        _log.Debug("SetAttributes: now setting field " + TimeSheetFieldMap.FieldInfo + " to " + value);
                        // set the instance's field to the value
                        try
                        {
                            TimeSheetFieldMap.FieldInfo.SetValue(this, value);
                        }
                        catch
                        {
                            _log.Debug("Exception caught in FieldInfo.SetValue setting field {0} to {1}.", TimeSheetFieldMap.FieldInfo, value);
                            //throw;
                        }
                        _log.Debug("SetAttributes: Setting " + TimeSheetFieldMap.FieldName + " to " + value);
                    }
                    catch (Exception e)
                    {
                        _log.Debug("Exception caught reading {0}, column {1}, column name '{2}'.", e, this.GetType().Name, i, reader.GetName(i));
                        //throw;
                    }
                }
            }
            finally
            {
                CodeAccessPermission.RevertAssert();
            }
            _log.Debug("SetAttributes: done (" + this.GetType().FullName + ")");
        }

        /// <summary>
        /// Serialize the object as CSV to a buffer which can be written to a file
        /// </summary>
        /// <returns>The serialized object</returns>

        public virtual string Serialize()
        {
            StringBuilder buffer = new StringBuilder();

            TimeSheetDataLink broker = this.GetDataLink();
            IDictionary mappings = broker.FieldMappings;
            IList attributeNames = broker.AttributeNames;

            for (int i = 0; i < attributeNames.Count; i++)
            {
                object key = attributeNames[i];
                TimeSheetFieldMap map = (TimeSheetFieldMap)mappings[key];

                object value = map.FieldInfo.GetValue(this);

                if (value != null &&
                    (value.GetType() == typeof(string) ||
                     value.GetType() == typeof(DateTime) ))
                {
                    buffer.Append("\"").Append(value).Append("\"");
                }
                else
                {
                    buffer.Append(value);
                }

                if (i < attributeNames.Count - 1)
                {
                    buffer.Append(",");
                }
            }

            return buffer.ToString();
        }

        private Object GetObjectFromDatabase(IDataReader reader, int columnNum)
        {
            object value = null;

            if (reader.GetName(columnNum) == GetDataLink().PrimaryKey)
            {
                // it's the object's guidfield
                value = reader.GetGuid(columnNum);
            }
            else
            {
                switch (reader.GetDataTypeName(columnNum).ToLower())
                {
                    case ("nchar"):
                    case ("char"):
                        // fixed length char columns need to be trimmed
                        value = reader.GetString(columnNum).Trim();
                        break;
                    case ("nvarchar"):
                    case ("varchar"):
                    case ("text"):
                    case ("ntext"):
                        value = reader.GetString(columnNum);
                        break;
                    case "tinyint":
                        value = reader.GetByte(columnNum);
                        break;
                    case "smallint":
                        value = reader.GetInt16(columnNum);
                        break;
                    case "int":
                        value = reader.GetInt32(columnNum);
                        break;
                    case "bit":
                        value = reader.GetBoolean(columnNum);
                        break;
                    case "smalldatetime":
                    case "datetime":
                        value = reader.GetDateTime(columnNum);
                        break;
                    case "money":
                        value = reader.GetDecimal(columnNum);
                        break;
                    case "bigint":
                        value = reader.GetInt64(columnNum);
                        break;
                    case "uniqueidentifier":
                        value = reader.GetGuid(columnNum);
                        break;
                    case "decimal":
                        value = reader.GetDecimal(columnNum);
                        break;
                    default:
                        throw new ApplicationException(string.Format("Unknown conversion for {0} data type.", reader.GetDataTypeName(columnNum)));
                }
            }

            return value;
        }
    }
}
