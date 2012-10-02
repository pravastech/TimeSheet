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

        private String _username;
        private String _projectname;
        private String _taskname;
        private DateTime _taskdate;
        private String _percentage;
        private String _notes;
        private String _codesnippet;


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

        public String username
        {
            get { return _username; }
            set { SetString(ref _username, value); }
        }
        public String projectname
        {
            get { return _projectname; }
            set { SetString(ref _projectname, value); }
        }
        public String taskname
        {
            get { return _taskname; }
            set { SetString(ref _taskname, value); }
        }
        public DateTime taskdate
        {
            get { return _taskdate; }
            set { SetDateTime(ref _taskdate, value); }
        }
        public String percentage
        {
            get { return _percentage; }
            set { SetString(ref _percentage, value); }
        }
        public String notes
        {
            get { return _notes; }
            set { SetString(ref _notes, value); }
        }
        public String codesnippet
        {
            get { return _codesnippet; }
            set { SetString(ref _codesnippet, value); }
        }

    }
    public sealed class timesheettaskDataLink : TimeSheetDataLink
    {
        private static volatile timesheettaskDataLink _instance = new timesheettaskDataLink();

        private timesheettaskDataLink()
            : base(typeof(timesheettask), "timesheettask")
        {
            AddFieldMapping("_username", "username", false, false);
            AddFieldMapping("_projectname", "projectname", false, false);
            AddFieldMapping("_taskname", "taskname", false, false);
            AddFieldMapping("_taskdate", "taskdate", false, false);
            AddFieldMapping("_percentage", "percentage", false, false);
            AddFieldMapping("_notes", "notes", false, false);
            AddFieldMapping("_codesnippet", "codesnippet", false, false);

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