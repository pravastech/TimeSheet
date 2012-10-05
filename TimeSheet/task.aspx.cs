using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheetBO;

namespace TimeSheet
{
    public partial class task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region old code for ref
            //            namespace Pos360
//{
//    public partial class tasksTest : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            string submitKey = (Request.Form["submitKey"] ?? "").ToString();
//            if (!string.IsNullOrEmpty(submitKey))
//            {
//                string allSubmitKeys = (Session["allSubmitKeys"] ?? "").ToString();
//                if (!allSubmitKeys.Split(',').Contains(submitKey))
//                {
//                    Session["allSubmitKeys"] = allSubmitKeys + "," + submitKey;
//                    CurrentUser user = new CurrentUser("POSProcesor");
//                    PosBO.tasks taskObj = new PosBO.tasks(user);
//                    taskObj.LoadSingle(taskObj, " where rolename=@rolename", "rolename", Request.Form["rolename"] ?? "");
//                    taskObj.roleName = Request.Form["rolename"];
//                    taskObj.allowAdd = (Request.Form["allowadd"] ?? "").Equals("on");
//                    taskObj.allowDelete = (Request.Form["allowdelete"] ?? "").Equals("on");
//                    taskObj.allowEdit = (Request.Form["allowedit"] ?? "").Equals("on");
//                    taskObj.allowView = (Request.Form["allowview"] ?? "").Equals("on");
//                    if (taskObj.Save())
//                    {
//                        ltrMessage.Text = "Store saved successfully.";
//                    }
//                    else
//                    {
//                        ltrMessage.Text = "Unable to save store information.";
//                    }
//                }
//            }
            
//        }
//        protected void Page_LoadComplete(object sender, EventArgs e)
//        {
//            CurrentUser user = new CurrentUser("POSProcesor");
//            var tasksGrid = POSGridUtility.tasksGrid(user);

//            var tasks = new tasks(user).Load("", "", "").Cast<tasks>().ToList();
//            tasksGrid.Rows.AddRange(tasks);

//            ltrGridUI.Text = tasksGrid.gridTable.ToHTML();
//            ltrAddNew.Text = "<button type=\"button\" onclick=\"GridUtil.newRow();\">Add New</button>";
//            ltrGridScript.Text = JSUtil.encloseInJavascriptTag("gridData = " + tasksGrid.gridJS() + ";\ncolumnJS=[" + tasksGrid.gridTable.columnJS() + "]");
//            ltrHiddens.Text = tasksGrid.gridTable.hiddenVars;
              
//        }
//    }
            //}
            #endregion
            string submitKey = (Request.Form["submitKey"] ?? "").ToString();
            if (!string.IsNullOrEmpty(submitKey))
            {
                string allSubmitKeys = (Session["allSubmitKeys"] ?? "").ToString();
                if (!allSubmitKeys.Split(',').Contains(submitKey))
                {
                    Session["allSubmitKeys"] = allSubmitKeys + "," + submitKey;
                    CurrentUser user = new CurrentUser("TimeSheetAdmin");
                    TimeSheetBO.Task taskObj = new TimeSheetBO.Task(user);
                    Guid guidfield;
                    Guid.TryParse(Request.Form["guidfield"], out guidfield);
                    string req = Request.Form["guidfield"];
                    if (!string.IsNullOrEmpty(req))
                    {
                        taskObj.LoadSingle(taskObj, " WHERE guidfield = @guidfield", "guidfield", guidfield);
                    }
                   // taskObj.LoadSingle(taskObj, " WHERE guidfield = @guidfield", "guidfield", guidfield);
                    taskObj.ProjectName = Request.Form["projectname"];
                    taskObj.TaskName = Request.Form["taskname"];
                   // taskObj.begindate = Request.Form["begindate"];
                    DateTime bdate;
                    DateTime.TryParse(Request.Form["begindate"], out bdate);
                    taskObj.BeginDate = bdate;
                    string strbeginTime = Request.Form["begindate"];
                    DateTime beginDate =  new DateTime();
                    //beginDate=DateTime.ParseExact(strbeginTime,"MM/dd/yyyy",null);
                    //taskObj.begindate = beginDate;
                   //taskObj.begindate = DateTime.Now["begindate"];
                    //string strEndTime = Request.Form["enddate"];
                    //DateTime endDate = new DateTime();
                    //endDate = DateTime.ParseExact(strEndTime, "mm/dd/yyyy", null);
                   // taskObj.enddate = Request.Form["enddate"];
                    DateTime edate;
                    DateTime.TryParse(Request.Form["enddate"], out edate);
                    taskObj.EndDate = edate;
                    string strenddate = Request.Form["enddate"];
                    DateTime enddate = new DateTime();
                    taskObj.DevUserName = Request.Form["devuserName"];
                    taskObj.QAUserName = Request.Form["qauserName"];
                    taskObj.ApprovedUserName = Request.Form["approveduserName"];
                    //if (strbeginTime.Length == null)
                    //{
                        if (taskObj.Save())
                        {

                            ltrMessage.Text = "Store saved successfully.";
                        }
                        else
                        {
                            ltrMessage.Text = "Unable to save store information.";
                        }

                    //}
                    //else 
                    //{
                    //    taskObj.Update();
                    //}
                }
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            CurrentUser user = new CurrentUser("TimeSheetAdmin");
            var taskGrid = TimeSheetGridUtility.taskGrid(user);
            taskGrid.allowDelete = true;

            //var task = new task(user).load("", "", "").Cast<task>().ToList();
            var task = new TimeSheetBO.Task(user).Load("", "", "").Cast<TimeSheetBO.Task>().ToList();
            taskGrid.Rows.AddRange(task);

            ltrGridUI.Text = taskGrid.gridTable.ToHTML();
            ltrAddNew.Text = "<button type=\"button\" onclick=\"GridUtil.newRow();\">Add New</button>";
            ltrGridScript.Text = JSUtil.encloseInJavascriptTag("gridData = " + taskGrid.gridJS() + ";\ncolumnJS=[" + taskGrid.gridTable.columnJS() + "]");
            ltrHiddens.Text = taskGrid.gridTable.hiddenVars;

            var lstUsers = new Users(user).Load("", "", "").Cast<Users>().ToList();
            this.ltrdevUsers.Text = "<option value=\"\"></option>" + 
            String.Join("", lstUsers.Select(x=>x.UserName).Distinct().Select(uname =>
            {
                return "<option value=\"" + HttpUtility.HtmlEncode(uname) + "\">" + HttpUtility.HtmlEncode(uname) + "</option>";
            }).ToArray());
            ltrqausers.Text = ltrdevUsers.Text;
            ltrapprovedusers.Text = ltrdevUsers.Text;
        }

        } 
    }