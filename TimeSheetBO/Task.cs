using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using TimeSheetUtil;

namespace TimeSheetBO
{
    public class Task : TimeSheetBase
    {
        private static Hashtable _cache = null;

        private String _ProjectName;
        private String _TaskName;
        private DateTime _BeginDate;
        private DateTime _EndDate;
        private String _DevUserName;
        private String _QAUserName;
        private String _ApprovedUserName;
        private Guid _guidfield;
        private DateTime _lastChange;
        private String _lastChangeUser;


        public Task()
            : base()
        {

        }

        public Task(CurrentUser User)
            : base()
        {
            this.User = User;
        }

        public static TaskDataLink TaskDataLink
        {
            get
            {
                return TaskDataLink.Instance;
            }
        }

        public override TimeSheetDataLink GetDataLink()
        {
            return TaskDataLink;
        }

        public static Hashtable GetCache1()
        {
            return _cache;
        }

        public String ProjectName
        {
            get { return _ProjectName; }
            set { SetString(ref _ProjectName, value); }
        }
        public String TaskName
        {
            get { return _TaskName; }
            set { SetString(ref _TaskName, value); }
        }
        public DateTime BeginDate
        {
            get { return _BeginDate; }
            set { SetDateTime(ref _BeginDate, value); }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { SetDateTime(ref _EndDate, value); }
        }
        public String DevUserName
        {
            get { return _DevUserName; }
            set { SetString(ref _DevUserName, value); }
        }
        public String QAUserName
        {
            get { return _QAUserName; }
            set { SetString(ref _QAUserName, value); }
        }
        public String ApprovedUserName
        {
            get { return _ApprovedUserName; }
            set { SetString(ref _ApprovedUserName, value); }
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

    }
    public sealed class TaskDataLink : TimeSheetDataLink
    {
        private static volatile TaskDataLink _instance = new TaskDataLink();

        private TaskDataLink()
            : base(typeof(Task), "Task")
        {
            AddFieldMapping("_ProjectName", "ProjectName", false, true);
            AddFieldMapping("_TaskName", "TaskName", false, true);
            AddFieldMapping("_BeginDate", "BeginDate", false, false);
            AddFieldMapping("_EndDate", "EndDate", false, false);
            AddFieldMapping("_DevUserName", "DevUserName", false, false);
            AddFieldMapping("_QAUserName", "QAUserName", false, false);
            AddFieldMapping("_ApprovedUserName", "ApprovedUserName", false, false);
            AddFieldMapping("_guidfield", "guidfield", false, true);
            AddFieldMapping("_lastChange", "lastChange", false, false);
            AddFieldMapping("_lastChangeUser", "lastChangeUser", false, false);

        }

        public static TaskDataLink Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(TaskDataLink))
                    {
                        if (_instance == null)
                        {
                            _instance = new TaskDataLink();
                        }
                    }
                }

                return _instance;
            }
        }

        public override TimeSheetBase Init()
        {
            return new Task();
        }
    }

}