﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheetBO;

namespace TimeSheet
{
    public partial class WebForm3 : System.Web.UI.Page
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
                TimeSheetBO.Task taskObj = new TimeSheetBO.Task(user);
                Guid guidfield;
                Guid.TryParse(Request.Form["guidfield"], out guidfield);
                taskObj.LoadSingle(taskObj, " WHERE guidfield = @guidfield", "guidfield", guidfield);
                //taskObj.ProjectName = Request.Form["projectname"];
                //taskObj.TaskName = Request.Form["taskname"];
                // taskObj.begindate = Request.Form["begindate"];
                DateTime bdate;
                DateTime.TryParse(Request.Form["beginFromDate"], out bdate);
                taskObj.BeginDate = bdate;
                string strbeginTime = Request.Form["beginFromDate"];
                DateTime beginDate = new DateTime();
                //beginDate=DateTime.ParseExact(strbeginTime,"MM/dd/yyyy",null);
                //taskObj.begindate = beginDate;
                //taskObj.begindate = DateTime.Now["begindate"];
                //string strEndTime = Request.Form["enddate"];
                //DateTime endDate = new DateTime();
                //endDate = DateTime.ParseExact(strEndTime, "mm/dd/yyyy", null);
                // taskObj.enddate = Request.Form["enddate"];
                DateTime edate;
                DateTime.TryParse(Request.Form["beginToDate"], out edate);
                taskObj.EndDate = edate;
                string strenddate = Request.Form["beginToDate"];
                DateTime enddate = new DateTime();
                taskObj.DevUserName = Request.Form["devuserName"];
                //taskObj.QAUserName = Request.Form["qauserName"];
                //taskObj.ApprovedUserName = Request.Form["approveduserName"];
                //if (strbeginTime.Length == null)
                //{
                if (taskObj.Save())
                {

                    //ltrMessage.Text = "Store saved successfully.";
                }
                else
                {
                    //ltrMessage.Text = "Unable to save store information.";
                }

                //}
                //else 
                //{
                //    taskObj.Update();
                //}
            }
                }
        }
        protected void page_complete(object sender, EventArgs e)
        {
            CurrentUser user = new CurrentUser("TimeSheetAdmin");
            var lstChooseUsers = new Users(user).Load("", "", "").Cast<Users>().ToList();
            this.lstChooseUsers.Text = "<option value=\"\"></option>" +
            String.Join("", lstChooseUsers.Select(x => x.UserName).Distinct().Select(uname =>
            {
                return "<option value=\"" + HttpUtility.HtmlEncode(uname) + "\">" + HttpUtility.HtmlEncode(uname) + "</option>";
            }).ToArray());
        }
    }
}