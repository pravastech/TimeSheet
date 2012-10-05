using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class timesheettask : TimeSheetBase
    {
        private static Hashtable _cache = null;

        private String _UserName;
        private Guid _guidfield;
        private String _Projectname;
        private String _Taskname;
        private DateTime _Taskdate;
        private String _Percentage;
        private String _Notes;
        private String _CodeSnippet;


        public timesheettask()
            : base()
        {

        }

        public timesheettask(CurrentUser User)
            : base()
        {
            this.User = User;
        }

        public static timesheettaskDataLink timesheettaskDataLink
        {
            get
            {
                return timesheettaskDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return timesheettaskDataLink;
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
        public Guid guidfield
        {
            get { return _guidfield; }
            set { SetGuid(ref _guidfield, value); }
        }
        public String Projectname
        {
            get { return _Projectname; }
            set { SetString(ref _Projectname, value); }
        }
        public String Taskname
        {
            get { return _Taskname; }
            set { SetString(ref _Taskname, value); }
        }
        public DateTime Taskdate
        {
            get { return _Taskdate; }
            set { SetDateTime(ref _Taskdate, value); }
        }
        public String Percentage
        {
            get { return _Percentage; }
            set { SetString(ref _Percentage, value); }
        }
        public String Notes
        {
            get { return _Notes; }
            set { SetString(ref _Notes, value); }
        }
        public String CodeSnippet
        {
            get { return _CodeSnippet; }
            set { SetString(ref _CodeSnippet, value); }
        }

    }
    public sealed class timesheettaskDataLink : TimeSheetDataLink
    {
        private static volatile timesheettaskDataLink _instance = new timesheettaskDataLink();

        private timesheettaskDataLink()
            : base(typeof(timesheettask), "timesheettask")
        {
            AddFieldMapping("_UserName", "UserName", false, true);
            AddFieldMapping("_guidfield", "guidfield", false, true);
            AddFieldMapping("_Projectname", "Projectname", false, true);
            AddFieldMapping("_Taskname", "Taskname", false, false);
            AddFieldMapping("_Taskdate", "Taskdate", false, false);
            AddFieldMapping("_Percentage", "Percentage", false, true);
            AddFieldMapping("_Notes", "Notes", false, false);
            AddFieldMapping("_CodeSnippet", "CodeSnippet", false, false);

        }

        public static timesheettaskDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(timesheettaskDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new timesheettaskDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new timesheettask();
        }
    }

}