using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSheetBO;
using System.Data.SqlClient;

namespace TimeSheet
{
     public class manager_report
    {
        private List<mgrreport> managerreportlist = new List<mgrreport>();

        public manager_report(string bdate, string edate)
        {
            string tsql = "Select taskdate,project,task,status from timesheettask Where taskdate between '" + bdate.Replace("'","''") + "' and '" + edate.Replace("'","''") + "' order by 1, 2";
            MakeDepositFile(tsql);
        }

        private void MakeDepositFile(string sql)
        {
            TimeSheetContext context = new TimeSheetContext();
            SqlCommand dbCmd = context.CreateCommand(sql);
            context.OpenConnection();
            SqlDataReader reader = dbCmd.ExecuteReader();
            
            while (reader.Read())
            {
                managerreportlist fdep = new managerreportlist();
                fdep.status = reader.GetString(0);
                fdep.taskDate = reader.GetDateTime(1);
                fdep.task = reader.GetString(2);
                fdep.project = reader.GetString(2);
                fdep.Amount = reader.GetDecimal(3);

                managerreportlist.Add(fdep);
            }
        }

        public string ToTable()
        {
            StringBuilder sbTable = new StringBuilder();
            sbTable.Append(@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" class=""dyntable"" id=""tblFinDeposit"">");
            sbTable.Append(@"<thead><tr>
<th class=""head1"">Status</th>
<th class=""head0"">taskDate</th>
<th class=""head1"">task</th>
<th class=""head1"">project</th>
<th class=""head0"">Amount</th>
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
        public decimal Amount { get; set; }

        public string ToTableRow()
        {
            return String.Format(@"<tr>
<td>{0}</td><td>{1}</td><td>{2}</td>
<td>{3}</td></tr>", HttpUtility.HtmlEncode(status), HttpUtility.HtmlEncode(taskDate.ToString("MM/dd/yyyy")), HttpUtility.HtmlEncode(task),
                  HttpUtility.HtmlEncode(project),HttpUtility.HtmlEncode(Amount.ToString("0.00")));
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", status, taskDate.ToString("MM/dd/yyyy"), task,project, Amount.ToString("0.00"));
        }
    }
}
