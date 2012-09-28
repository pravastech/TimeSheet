using System;
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
                    TimeSheetBO.Users userRoleObj = new TimeSheetBO.Users(user);
                    userRoleObj.LoadSingle(userRoleObj, " where rolename=@rolename", "rolename", Request.Form["rolename"] ?? "");
                    userRoleObj.UserName = Request.Form["UserName"];
                    userRoleObj.FullName = Request.Form["FullName"];
                    userRoleObj.emailAddress = Request.Form["EmailAddress"];
                    userRoleObj.Address1 = Request.Form["Address1"];
                    userRoleObj.Address2 = Request.Form["Address2"];
                    userRoleObj.City = Request.Form["City"];
                    userRoleObj.State = Request.Form["State"];
                    userRoleObj.Role = Request.Form["Role"];
                    if (userRoleObj.Save())
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