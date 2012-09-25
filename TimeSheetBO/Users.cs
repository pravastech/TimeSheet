using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class Users : TimeSheetBase
    {
        private static Hashtable _cache = null;
        private String _UserName;
        private String _Password;
        private Guid _guidfield;
        private DateTime _lastChange;
        private String _lastChangeUser;
        private String _emailAddress;
        private String _phone;
        private String _FullName;
        private String _Title;
        private String _AllowedStores;
        private DateTime _LastPasswordChange;
        private String _tempPW;
        private DateTime _tempPWExpiration;
        private String _SecurityQ1;
        private String _SecurityA1;
        private String _SecurityQ2;
        private String _SecurityA2;
        private String _SecurityQ3;
        private String _SecurityA3;
        private String _Role;
        private Boolean _VerfNextLogin;
        private Boolean _disabledLogin;
        private String _department;
        private String _usergroup;
        private Boolean _allowGoogleCalendar;
        private String _googleToken;
        private String _googleTokenSecret;


        public Users()
            : base()
        {

        }

        public Users(CurrentUser User)
            : base()
        {
            
        }

        public static UsersDataLink UsersDataLink
        {
            get
            {
                return UsersDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return UsersDataLink;
        }

        public static Hashtable GetCache1()
        {
            return _cache;
        }

        public String UserName
        {
            get { return _UserName; }
            set { SetString(ref _UserName, value); }
        }
        public String Password
        {
            get { return _Password; }
            set { SetString(ref _Password, value); }
        }
        public Guid guidfield
        {
            get { return _guidfield; }
            set { SetGuid(ref _guidfield, value); }
        }
        public DateTime lastChange
        {
            get { return _lastChange; }
            set { SetDateTime(ref _lastChange, value); }
        }
        public String lastChangeUser
        {
            get { return _lastChangeUser; }
            set { SetString(ref _lastChangeUser, value); }
        }
        public String emailAddress
        {
            get { return _emailAddress; }
            set { SetString(ref _emailAddress, value); }
        }
        public String phone
        {
            get { return _phone; }
            set { SetString(ref _phone, value); }
        }
        public String FullName
        {
            get { return _FullName; }
            set { SetString(ref _FullName, value); }
        }
        public String Title
        {
            get { return _Title; }
            set { SetString(ref _Title, value); }
        }
        public String AllowedStores
        {
            get { return _AllowedStores; }
            set { SetString(ref _AllowedStores, value); }
        }
        public DateTime LastPasswordChange
        {
            get { return _LastPasswordChange; }
            set { SetDateTime(ref _LastPasswordChange, value); }
        }
        public String tempPW
        {
            get { return _tempPW; }
            set { SetString(ref _tempPW, value); }
        }
        public DateTime tempPWExpiration
        {
            get { return _tempPWExpiration; }
            set { SetDateTime(ref _tempPWExpiration, value); }
        }
        public String SecurityQ1
        {
            get { return _SecurityQ1; }
            set { SetString(ref _SecurityQ1, value); }
        }
        public String SecurityA1
        {
            get { return _SecurityA1; }
            set { SetString(ref _SecurityA1, value); }
        }
        public String SecurityQ2
        {
            get { return _SecurityQ2; }
            set { SetString(ref _SecurityQ2, value); }
        }
        public String SecurityA2
        {
            get { return _SecurityA2; }
            set { SetString(ref _SecurityA2, value); }
        }
        public String SecurityQ3
        {
            get { return _SecurityQ3; }
            set { SetString(ref _SecurityQ3, value); }
        }
        public String SecurityA3
        {
            get { return _SecurityA3; }
            set { SetString(ref _SecurityA3, value); }
        }
        public String Role
        {
            get { return _Role; }
            set { SetString(ref _Role, value); }
        }
        public Boolean VerfNextLogin
        {
            get { return _VerfNextLogin; }
            set { SetBoolean(ref _VerfNextLogin, value); }
        }
        public Boolean disabledLogin
        {
            get { return _disabledLogin; }
            set { SetBoolean(ref _disabledLogin, value); }
        }
        public String department
        {
            get { return _department; }
            set { SetString(ref _department, value); }
        }
        public String usergroup
        {
            get { return _usergroup; }
            set { SetString(ref _usergroup, value); }
        }
        public Boolean allowGoogleCalendar
        {
            get { return _allowGoogleCalendar; }
            set { SetBoolean(ref _allowGoogleCalendar, value); }
        }
        public String googleToken
        {
            get { return _googleToken; }
            set { SetString(ref _googleToken, value); }
        }
        public String googleTokenSecret
        {
            get { return _googleTokenSecret; }
            set { SetString(ref _googleTokenSecret, value); }
        }

    }
    public sealed class UsersDataLink : TimeSheetDataLink
    {
        private static volatile UsersDataLink _instance = new UsersDataLink();

        private UsersDataLink()
            : base(typeof(Users), "Users")
        {
            AddFieldMapping("_UserName", "UserName", false, true);
            AddFieldMapping("_Password", "Password", false, false);
            AddFieldMapping("_guidfield", "guidfield", false, true);
            AddFieldMapping("_lastChange", "lastChange", false, false);
            AddFieldMapping("_lastChangeUser", "lastChangeUser", false, false);
            AddFieldMapping("_emailAddress", "emailAddress", false, true);
            AddFieldMapping("_phone", "phone", false, false);
            AddFieldMapping("_FullName", "FullName", false, false);
            AddFieldMapping("_Title", "Title", false, false);
            AddFieldMapping("_AllowedStores", "AllowedStores", false, true);
            AddFieldMapping("_LastPasswordChange", "LastPasswordChange", false, false);
            AddFieldMapping("_tempPW", "tempPW", false, false);
            AddFieldMapping("_tempPWExpiration", "tempPWExpiration", false, false);
            AddFieldMapping("_SecurityQ1", "SecurityQ1", false, false);
            AddFieldMapping("_SecurityA1", "SecurityA1", false, false);
            AddFieldMapping("_SecurityQ2", "SecurityQ2", false, false);
            AddFieldMapping("_SecurityA2", "SecurityA2", false, false);
            AddFieldMapping("_SecurityQ3", "SecurityQ3", false, false);
            AddFieldMapping("_SecurityA3", "SecurityA3", false, false);
            AddFieldMapping("_Role", "Role", false, true);
            AddFieldMapping("_VerfNextLogin", "VerfNextLogin", false, true);
            AddFieldMapping("_disabledLogin", "disabledLogin", false, true);
            AddFieldMapping("_department", "department", false, false);
            AddFieldMapping("_usergroup", "usergroup", false, false);
            AddFieldMapping("_allowGoogleCalendar", "allowGoogleCalendar", false, false);
            AddFieldMapping("_googleToken", "googleToken", false, false);
            AddFieldMapping("_googleTokenSecret", "googleTokenSecret", false, false);

        }

        public static UsersDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(UsersDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new Users();
        }
    }

}