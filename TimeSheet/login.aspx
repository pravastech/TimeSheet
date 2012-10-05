<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TimeSheet.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery.js?20120922"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.13.custom.min.js?20120922"></script>
    <script type="text/javascript" src="/js/General.js?20120922"></script>
    <script type="text/javascript" src="/js/GridUtil.js?20121001"></script>
</head>
<body>
    <div style="width: 570px; margin: 10px auto; padding: 7px 10px; font-size: 11px; color: #ccc; -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; color:#000;">
        <asp:Literal ID="ltrSessionEndMsg" runat="server"></asp:Literal>
    </div>    
    <form id="loginform" method="post" runat="server">
<div class="loginbox">    
	<div class="loginbox_inner">
    	<div class="loginbox_content">
            <input type="text" name="username" class="username" />
            <input type="password" name="password" class="password" />
            <button name="submit" class="submit">Login</button>
        </div><!--loginbox_content-->
    </div><!--loginbox_inner-->
</div><!--loginbox-->

<div class="loginoption">
	<a href="" class="cant">Can't access your account?</a>
    <input type="checkbox" name="remember" /> Remember me on this computer.
</div><!--loginoption-->

</form>
    
</body>
</html>
