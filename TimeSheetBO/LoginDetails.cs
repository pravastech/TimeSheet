using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class LoginDetails : TimeSheetBase
    {
        private static Hashtable _cache = null;

        private String _Username;
        private String _Password;


        public LoginDetails()
            : base()
        {

        }

        public LoginDetails(CurrentUser User)
            : base()
        {
            this.User = User;
        }

        public static LoginDetailsDataLink LoginDetailsDataLink
        {
            get
            {
                return LoginDetailsDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return LoginDetailsDataLink;
        }

        public static Hashtable GetCache1()
        {
            return _cache;
        }

        public String Username
        {
            get { return _Username; }
            set { SetString(ref _Username, value); }
        }
        public String Password
        {
            get { return _Password; }
            set { SetString(ref _Password, value); }
        }

    }
    public sealed class LoginDetailsDataLink : TimeSheetDataLink
    {
        private static volatile LoginDetailsDataLink _instance = new LoginDetailsDataLink();

        private LoginDetailsDataLink()
            : base(typeof(LoginDetails), "LoginDetails")
        {
            AddFieldMapping("_Username", "Username", false, false);
            AddFieldMapping("_Password", "Password", false, false);

        }

        public static LoginDetailsDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(LoginDetailsDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new LoginDetailsDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new LoginDetails();
        }
    }

}
