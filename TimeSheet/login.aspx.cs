using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeSheetBO;

namespace TimeSheet
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        
        
        
        {
            if (!IsPostBack)
            {
                String sessionEnd = Request.QueryString["sessionEnd"];
                if (!String.IsNullOrEmpty(sessionEnd) && sessionEnd.Equals("1"))
                {
                    Session.Clear();
                    ltrSessionEndMsg.Text = Message("Your current session is ended. Please login again.", MsgType.Alert);
                }
                String logout = Request.QueryString["logout"];
                if (!String.IsNullOrEmpty(logout) && logout.Equals("1"))
                {
                    Session.RemoveAll();
                    ltrSessionEndMsg.Text = Message("You have been logged out.", MsgType.Information);
                }
            }
            else
            {
                String username = Request.Form["username"] ?? "";
                String password = Request.Form["password"] ?? "";

                CurrentUser User = new CurrentUser(username);
                if (User.IsLoginValid(password))
                {
                    HttpContext.Current.Session["UserInSession"] = User;
                    if (User.IsLoggedInWithTemp)
                    {
                        /* Should Change Password and fill in security questions */
                        Response.Redirect("User.aspx");
                    }
                    else
                    {
                        Response.Redirect("task.aspx");
                    }
                }
                else
                {
                    ltrSessionEndMsg.Text = Message("Invalid Username or Password.", MsgType.Error);
                }
            }
        }
        public static string Message(string msg, MsgType type)
        {
            String result = "";
            if (type.Equals(MsgType.Success))
            {
                result = @"<div class=""notification msgsuccess"">
            	           <a class=""close""></a>
            	           <p>" + msg + "</p></div>";
            }
            else if (type.Equals(MsgType.Information))
            {
                result = @"<div class=""notification msginfo"">
            	           <a class=""close""></a>
            	           <p>" + msg + "</p></div>";
            }
            else if (type.Equals(MsgType.Alert))
            {
                result = @"<div class=""notification msgalert"">
            	           <a class=""close""></a>
            	           <p>" + msg + "</p></div>";
            }
            else if (type.Equals(MsgType.Error))
            {
                result = @"<div class=""notification msgerror"">
            	           <a class=""close""></a>
            	           <p>" + msg + "</p></div>";
            }
            return result;
        }
        public enum MsgType { Success, Information, Alert, Error }; 
    }
}