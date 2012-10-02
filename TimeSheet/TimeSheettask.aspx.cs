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
                    TimeSheetObj.LoadSingle(TimeSheetObj, " WHERE guidfield = @guidfield", "guidfield", guidfield);
                    TimeSheetObj.username = Request.Form["username"];
                    TimeSheetObj.projectname = Request.Form["projectname"];
                    TimeSheetObj.taskname = Request.Form["taskname"];
                    DateTime tdate;
                    DateTime.TryParse(Request.Form["taskdate"],out tdate);
                    TimeSheetObj.taskdate =  tdate;
                    string strbegintime = Request.Form["taskdate"];
                    DateTime taskdate = new DateTime();
                    TimeSheetObj.percentage = Request.Form["Percentage"];
                    TimeSheetObj.notes = Request.Form["Notes"];
                    TimeSheetObj.codesnippet = Request.Form["CodeSnippet"];
                    if (TimeSheetObj.Save())
                    {

                        ltrMessage.Text = "Store saved successfully.";
                    }
                    else
                    {
                        ltrMessage.Text = "Unable to save store information.";
                    }
                }
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            CurrentUser user = new CurrentUser("TimeSheetAdmin"); /* This will change later only test purposes it is here, later it will become login user */
            var TimeSheetTaskGrid = TimeSheetGridUtility.TimeSheetTaskGrid(user);

            //var UserFields = new UserFields(user).load("", "", "").Cast<UserFields>().ToList();
            var Users = new Users(user).Load("", "", user).Cast<Users>().ToList();
            TimeSheetTaskGrid.Rows.AddRange(Users);

            ltrGridUI.Text = TimeSheetTaskGrid.gridTable.ToHTML();
            ltrAddNew.Text = "<button type=\"button\" onclick=\"GridUtil.newRow();\">Add New</button>";
            ltrGridScript.Text = JSUtil.encloseInJavascriptTag("gridData = " + TimeSheetTaskGrid.gridJS() + ";\ncolumnJS=[" + TimeSheetTaskGrid.gridTable.columnJS() + "]");
            ltrHiddens.Text = TimeSheetTaskGrid.gridTable.hiddenVars;

        }
    }
}