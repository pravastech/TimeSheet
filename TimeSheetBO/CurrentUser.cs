using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Reflection;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TimeSheetBO
{
    public class CurrentUser
    {
        private bool _loggedIn = true;
        private Users _Users;
        private String userName = "";
        private List<string> _determinedAllowedStores;
        private TimeSheetContext _userContext;
        private ApplicationException _error;
        private List<ApplicationException> _errors = new List<ApplicationException>();
        private bool _loggedInWithTemp = false;

        public bool IsLoggedInWithTemp
        {
            get { return _loggedInWithTemp; }
            private set { _loggedInWithTemp = value; }
        }

        public TimeSheetContext userContext
        {
            get
            {
                if (_userContext == null)
                {
                    _userContext = new TimeSheetContext(this.Users);
                }
                return _userContext;
            }
            set
            {
                _userContext = value;
            }
        }

        public Users Users
        {
            get
            {
                return _Users;
            }
        }

        public bool loggedIn
        {
            get { return _loggedIn; }
            private set { _loggedIn = value; }
        }

        public CurrentUser()
        {
            _Users = new Users();
        }

        public CurrentUser(String UserName)
        {
            this.userName = UserName;
            this.Load();
        }

        public void Load()
        {
            _Users = new Users();
            _Users.LoadSingle(_Users, " where username=@username", "username", this.userName);
        }

        public bool IsTempPWExpired
        {
            get
            {
                return (this.Users.tempPWExpiration < DateTime.Now);
            }
        }

        public bool SetPassword(String password)
        {
            
            this.Users.Password = hashedPassword(password);
            Boolean result = this.Users.Save();
            this.IsLoggedInWithTemp = !result;

            return result;
        }

        public Boolean IsLoginValid(string password)
        {
            Boolean loginResult = false;
            if (String.IsNullOrEmpty(password))
            {
                _error = new ApplicationException("The username and/or password is incorrect.");
            }
            else if (this.Users.disabledLogin)
            {
                _error = new ApplicationException("Your account has been disabled.");
            }
            else if (!this.IsTempPWExpired)
            {
                if (this.Users.tempPW.Equals(password))
                {
                    /* Logged in with Temp - Should Change Password */
                    this.Users.User = this;
                    this.IsLoggedInWithTemp = true;
                    this.Users.tempPW = "";
                    this.Users.tempPWExpiration = Convert.ToDateTime("01/01/1900");
                    this.Users.Save();

                    loginResult = true;
                }
            }
            
            if ((this.Users.Password??"").Equals(this.hashedPassword(password), StringComparison.InvariantCulture))
            {
                loginResult = true;
            }
            else
            {
                _error = new ApplicationException("The username and/or password is incorrect.");
            }

            return loginResult;
        }

        public void SleepWithMax(Int32 sleepMilliseconds, Int32 maxMillisecondstosleep)
        {
            System.Threading.Thread.Sleep(Math.Min(sleepMilliseconds, maxMillisecondstosleep));
        }

        public string hashedPassword(string password)
        {
            MD5 md = MD5.Create();
            string shash = this.userName + password;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] result = md.ComputeHash(encoding.GetBytes(shash));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
        }


        public TimeSheetBase protoTimeSheetBase(string objectName)
        {
            Type objType = Type.GetType("TimeSheetBO." + objectName);
            if (null == objType)
            {
                throw new ApplicationException("The object Name '" + objectName + "' is not valid. Please check object name.");
            }
            TimeSheetBase tobj = (TimeSheetBase)Activator.CreateInstance(objType, new object[] { this });
            return tobj;
        }

       
    }
}