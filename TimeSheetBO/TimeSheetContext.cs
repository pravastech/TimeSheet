using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data.SqlClient;

namespace TimeSheetBO
{
    public class TimeSheetContext
    {
        private static string _connectionString;
        private SqlConnection _sqlCon;
        private Users _user;

        public Users User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string UserName
        {
            get
            {
                if (_user == null)
                {
                    _user = new Users();
                    _user.UserName = "TimeSheet";
                }
                return _user.UserName;
            }
        }

        public TimeSheetContext()
        {
            _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            _sqlCon = new SqlConnection(_connectionString);           
        }

        public TimeSheetContext(string username)
        {
            this.User.UserName = username;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString"]);
            csb.ApplicationName = (string.IsNullOrEmpty(this.UserName) ? "TimeSheet" : this.UserName);
            _sqlCon = new SqlConnection(csb.ConnectionString);
        }

        public TimeSheetContext(string username, string conStr)
        {
            this.User.UserName = username;
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(conStr);
            csb.ApplicationName = (string.IsNullOrEmpty(this.UserName) ? "TimeSheet" : this.UserName);
            _sqlCon = new SqlConnection(conStr);
        }

        public TimeSheetContext(Users user)
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(ConfigurationManager.AppSettings["ConnectionString"]);
            csb.ApplicationName = (string.IsNullOrEmpty(this.UserName) ? "TimeSheet" : user.UserName);
            _sqlCon = new SqlConnection(csb.ConnectionString);
        }

        public SqlCommand CreateCommand(string sql)
        {
            SqlCommand dbCmd = new SqlCommand(sql);
            dbCmd.Connection = _sqlCon;

            return dbCmd;
        }

        public void OpenConnection()
        {
            if (_sqlCon.State == System.Data.ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
        }

        public static TimeSheetContext Create(TimeSheetContext context)
        {
            if (context == null)
            {
                return new TimeSheetContext();
            }
            return context;
        }

        public void CloseConnection(TimeSheetContext context)
        {
            try
            {
                _sqlCon.Close();
            }
            catch
            {
                //Catch if
            }
        }
    }
}
