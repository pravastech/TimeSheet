using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetBO;
using System.Data.SqlClient;
using System.Web;

namespace TimeSheet
{
     public class manager_report
    {
         private List<managerreportlist> managerreportlist = new List<managerreportlist>();

        public manager_report(string bdate, string edate, string user)
        {
            string tsql = "Select taskdate as 'Task Date',projectname as 'Project',taskname as 'Task',Notes as 'Status' from timesheettask Where taskdate between '" + bdate.Replace("'", "''") + "' and '" + edate.Replace("'", "''") + "' and UserName = '" + user + "' order by 1, 2";
            //List<SqlParameter> dbparm = new List<SqlParameter>();

            //dbparm.Add(new SqlParameter("@beginFromDate", bdate));
            //dbparm.Add(new SqlParameter("@beginToDate", edate));
            MakeManagerReport(tsql);
        }

        private void MakeManagerReport(string sql)
        {
            TimeSheetContext context = new TimeSheetContext();
            SqlCommand dbCmd = context.CreateCommand(sql);
            context.OpenConnection();
            SqlDataReader reader = dbCmd.ExecuteReader();
            
            while (reader.Read())
            {
                managerreportlist fdep = new managerreportlist();
                
                fdep.taskDate = reader.GetDateTime(0);
                fdep.project = reader.GetString(1);
                fdep.task = reader.GetString(2);
                fdep.status = reader.GetString(3);

                managerreportlist.Add(fdep);
            }
        }

        public string ToTable()
        {
            StringBuilder sbTable = new StringBuilder();
            sbTable.Append(@"<table cellpadding=""0"" cellspacing=""0"" border=""0""  id=""tblManReport"">");
            sbTable.Append(@"<thead><tr>
<th class=""head1"">Status</th>
<th class=""head0"">taskDate</th>
<th class=""head1"">task</th>
<th class=""head0"">project</th>
</tr></thead>");
            sbTable.Append("<tbody>");
            managerreportlist.ForEach(x =>
            {
                sbTable.Append(x.ToTableRow());
            });
            sbTable.AppendLine("</body></table>");

            return sbTable.ToString();
        }

        public string ToFile()
        {
            StringBuilder sbFile = new StringBuilder();
            managerreportlist.ForEach(x =>
            {
                sbFile.AppendLine(x.ToString());
            });

            return sbFile.ToString();
        }

    }

     public class managerreportlist
    {
        public string status { get; set; }
        public DateTime taskDate { get; set; }
        public string task { get; set; }
        public string project { get; set; }

        public string ToTableRow()
        {
            return String.Format(@"<tr>
<td>{0}</td><td>{1}</td><td>{2}</td>
<td>{3}</td></tr>", HttpUtility.HtmlEncode(status), HttpUtility.HtmlEncode(taskDate.ToString("MM/dd/yyyy")), HttpUtility.HtmlEncode(task),
                  HttpUtility.HtmlEncode(project));
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", status, taskDate.ToString("MM/dd/yyyy"), task,project);
        }
    }
}
