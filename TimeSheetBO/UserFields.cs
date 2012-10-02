using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class UserFields : TimeSheetBase
    {
        private static Hashtable _cache = null;
        private String _UserName;
        private String _FullName;
        private String _EmailAddress;
        private String _Address1;
        private String _Address2;
        private String _City;
        private String _State;
        private String _Zip;
        private String _Phone;
        private String _Role;
        private Guid _guidfield;
        


        public UserFields()
            : base()
        {

        }

        public UserFields(CurrentUser User)
            : base()
        {
            base.User = User;
        }

        public static UserFieldsDataLink UserFieldsDataLink
        {
            get
            {
                return UserFieldsDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return UserFieldsDataLink;
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
        public String FullName
        {
            get { return _FullName; }
            set { SetString(ref _FullName, value); }
        }
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { SetString(ref _EmailAddress, value); }
        }
        public String Address1
        {
            get { return _Address1; }
            set { SetString(ref _Address1, value); }
        }
        public String Address2
        {
            get { return _Address2; }
            set { SetString(ref _Address2, value); }
        }
        public String City
        {
            get { return _City; }
            set { SetString(ref _City, value); }
        }
        public String State
        {
            get { return _State; }
            set { SetString(ref _State, value); }
        }
        public String Zip
        {
            get { return _Zip; }
            set { SetString(ref _Zip, value); }
        }
        public String Phone
        {
            get { return _Phone; }
            set { SetString(ref _Phone, value); }
        }
        public String Role
        {
            get { return _Role; }
            set { SetString(ref _Role, value); }
        }
        public Guid guidfield
        {
            get { return _guidfield; }
            set { SetGuid(ref _guidfield, value); }
        }
        //public DateTime lastChange
        //{
        //    get { return _lastChange; }
        //    set { SetDateTime(ref _lastChange, value); }
        //}
        //public String lastChangeUser
        //{
        //    get { return _lastChangeUser; }
        //    set { SetString(ref _lastChangeUser, value); }
        //}

    }
    public sealed class UserFieldsDataLink : TimeSheetDataLink
    {
        private static volatile UserFieldsDataLink _instance = new UserFieldsDataLink();

        private UserFieldsDataLink()
            : base(typeof(UserFields), "UserFields")
        {
            AddFieldMapping("_UserName", "UserName", false, false);
            AddFieldMapping("_FullName", "FullName", false, false);
            AddFieldMapping("_EmailAddress", "EmailAddress", false, true);
            AddFieldMapping("_Address1", "Address1", false, true);
            AddFieldMapping("_Address2", "Address2", false, true);
            AddFieldMapping("_City", "City", false, true);
            AddFieldMapping("_State", "State", false, true);
            AddFieldMapping("_Zip", "Zip", false, true);
            AddFieldMapping("_Phone", "Phone", false, true);
            AddFieldMapping("_Role", "Role", false, true);
            AddFieldMapping("_guidfield", "guidfield", false, true);
            //AddFieldMapping("_lastChange", "lastChange", false, false);
            //AddFieldMapping("_lastChangeUser", "lastChangeUser", false, false);

        }

        public static UserFieldsDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(UserFieldsDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new UserFieldsDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new UserFields();
        }
    }

}