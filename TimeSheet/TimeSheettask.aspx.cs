using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheetBO;

namespace TimeSheet
{
    public partial class timesheettask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string submitKey = (Request.Form["submitKey"] ?? "").ToString();
            if (!string.IsNullOrEmpty(submitKey))
            {
                string allSubmitKeys = (Session["allSubmitKeys"] ?? "").ToString();
                if (!allSubmitKeys.Split(',').Contains(submitKey))
                {
                    Session["allSubmitKeys"] = allSubmitKeys + "," + submitKey;
                    CurrentUser user = new CurrentUser("TimeSheetAdmin");
                    TimeSheetBO.timesheettask TimeSheetObj = new TimeSheetBO.timesheettask(user);
                    Guid guidfield;
                    Guid.TryParse(Request.Form["guidfield"], out guidfield); 
                  string req=  Request.Form["guidfield"];
                    if (!string.IsNullOrEmpty(req)) 
                    {
                    TimeSheetObj.LoadSingle(TimeSheetObj, " WHERE guidfield = @guidfield", "guidfield", guidfield);
                    }
                    TimeSheetObj.UserName = Request.Form["username"];
                    TimeSheetObj.Projectname = Request.Form["projectname"];
                    TimeSheetObj.Taskname = Request.Form["taskname"];
                    DateTime tdate;
                    DateTime.TryParse(Request.Form["taskDate"],out tdate);
                    DateTime taskdate = new DateTime();
                    TimeSheetObj.Taskdate =  tdate;
                    string strbegintime = Request.Form["taskdate"];
                    TimeSheetObj.Percentage = Request.Form["percentage"];
                    TimeSheetObj.Notes = Request.Form["notes"];
                    TimeSheetObj.CodeSnippet = Request.Form["codesnippet"];
                    //if (TimeSheetObj.username.Length == null)
                    //   {
                      
                    if (TimeSheetObj.Save())
                    {

                        ltrMessage.Text = "TimeSheet saved successfully.";
                    }
                    else
                    {
                        ltrMessage.Text = "Unable to save TimeSheet information.";
                    }
                }
                    //else 
                    //{
                    //    TimeSheetObj.Update();
                    //}
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            CurrentUser user = new CurrentUser("TimeSheetAdmin"); 
            var TimeSheetTaskGrid = TimeSheetGridUtility.TimeSheetTaskGrid(user);
            TimeSheetTaskGrid.allowDelete = true;
            //var UserFields = new UserFields(user).load("", "", "").Cast<UserFields>().ToList();
            var timesheet = new TimeSheetBO.timesheettask(user).Load("", "", "").Cast<TimeSheetBO.timesheettask>().ToList();
            TimeSheetTaskGrid.Rows.AddRange(timesheet);

            ltrGridUI.Text = TimeSheetTaskGrid.gridTable.ToHTML();
            ltrAddNew.Text = "<button type=\"button\" onclick=\"GridUtil.newRow();\">Add New</button>";
            ltrGridScript.Text = JSUtil.encloseInJavascriptTag("gridData = " + TimeSheetTaskGrid.gridJS() + ";\ncolumnJS=[" + TimeSheetTaskGrid.gridTable.columnJS() + "]");
            ltrHiddens.Text = TimeSheetTaskGrid.gridTable.hiddenVars;

        }
    }
}