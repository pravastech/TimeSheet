using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class timesheet : TimeSheetBase
    {
        private static Hashtable _cache = null;
        private CurrentUser User;
        private String _usernamename;
        private String _projectname;
        private String _tasknamename;
        private DateTime _date;
        private String _percentage;
        private String _notes;
        private String _codesnippet;


        public timesheet()
            : base()
        {

        }

        public timesheet(CurrentUser User)
            : base()
        {
            this.User = User;
        }

        public static timesheetDataLink timesheetDataLink
        {
            get
            {
                return timesheetDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return timesheetDataLink;
        }

        public static Hashtable GetCache1()
        {
            return _cache;
        }

        public String usernamename
        {
            get { return _usernamename; }
            set { SetString(ref _usernamename, value); }
        }
        public String projectname
        {
            get { return _projectname; }
            set { SetString(ref _projectname, value); }
        }
        public String tasknamename
        {
            get { return _tasknamename; }
            set { SetString(ref _tasknamename, value); }
        }
        public DateTime date
        {
            get { return _date; }
            set { SetDateTime(ref _date, value); }
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
    public sealed class timesheetDataLink : TimeSheetDataLink
    {
        private static volatile timesheetDataLink _instance = new timesheetDataLink();

        private timesheetDataLink()
            : base(typeof(timesheet), "timesheet")
        {
            AddFieldMapping("_usernamename", "usernamename", false, false);
            AddFieldMapping("_projectname", "projectname", false, false);
            AddFieldMapping("_tasknamename", "tasknamename", false, false);
            AddFieldMapping("_date", "date", false, false);
            AddFieldMapping("_percentage", "percentage", false, false);
            AddFieldMapping("_notes", "notes", false, false);
            AddFieldMapping("_codesnippet", "codesnippet", false, false);

        }

        public static timesheetDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(timesheetDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new timesheetDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new timesheet();
        }
    }

}