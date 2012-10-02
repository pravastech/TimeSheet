﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheetBO;

namespace TimeSheet
{
    public partial class User : System.Web.UI.Page
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
                    TimeSheetBO.Users TimeSheetObj = new TimeSheetBO.Users(user);
                    TimeSheetObj.LoadSingle(TimeSheetObj, " where rolename=@rolename", "rolename", Request.Form["rolename"] ?? "");
                    TimeSheetObj.UserName = Request.Form["UserName"];
                    TimeSheetObj.FullName = Request.Form["FullName"];
                    TimeSheetObj.emailAddress = Request.Form["EmailAddress"];
                    TimeSheetObj.Address1 = Request.Form["Address1"];
                    TimeSheetObj.Address2 = Request.Form["Address2"];
                    TimeSheetObj.City = Request.Form["City"];
                    TimeSheetObj.State = Request.Form["State"];
                    TimeSheetObj.Role = Request.Form["Role"];
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
            var UserFieldsGrid = TimeSheetGridUtility.UserFieldsGrid(user);

            //var UserFields = new UserFields(user).load("", "", "").Cast<UserFields>().ToList();
            var Users = new Users(user).Load("", "", user).Cast<Users>().ToList();
            UserFieldsGrid.Rows.AddRange(Users);

            ltrGridUI.Text = UserFieldsGrid.gridTable.ToHTML();
            ltrAddNew.Text = "<button type=\"button\" onclick=\"GridUtil.newRow();\">Add New</button>";
            ltrGridScript.Text = JSUtil.encloseInJavascriptTag("gridData = " + UserFieldsGrid.gridJS() + ";\ncolumnJS=[" + UserFieldsGrid.gridTable.columnJS() + "]");
            ltrHiddens.Text = UserFieldsGrid.gridTable.hiddenVars;

        }
    }
}