<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TimeSheet.User" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/jquery.js?20120922"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.13.custom.min.js?20120922"></script>
    <script type="text/javascript" src="/js/General.js?20120922"></script>
    <script type="text/javascript" src="/js/GridUtil.js?20120922"></script>
    
<script type="text/javascript">
    var gridData = [];

    $().ready(function () {
        /* Later Set the DynTable */
    });

    GridUtil.UIFormName = function () {
        return "UserFieldsUIForm";
    }

    GridUtil.Title = function () {
        return "UserFields";
    }
</script>

        <asp:Literal ID="ltrGridScript" runat="server"></asp:Literal>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>
    <asp:Literal ID="ltrGridUI" runat="server"></asp:Literal>
     <asp:Literal ID="ltrAddNew" runat="server"></asp:Literal>

    </div>
    </form>

    <div class="form_default" style="display:none;" id="UserFieldsUIForm">
        <form id="form" action="" method="post">
            <asp:Literal ID="ltrHiddens" runat="server"></asp:Literal>
            <fieldset>
                 <p><label for="UserName">UserName </label><input type="text" name="UserName" id="UserName" class="sf" /></p>
                 <p><label for="FullName">FullName</label><input type="text"< name="FullName" id="FullName" /></p>
                <p><label for="Password">Password</label><input type="text"< name="Password" id="Password" /></p>
                <p><label for="EmailAddress">EmailAddress</label><input type="text"< name="EmailAddress" id="EmailAddress" /></p>
                <p><label for="Address1">Address1</label><input type="text"< name="Address1" id="Address1" /></p>
                 <p><label for="Address2">Address2 </label><input type="text" name="Address2" id="Address2" class="sf" /></p>
                 <p><label for="City">City </label><input type="text" name="City" id="City" class="sf" /></p>
                 <p><label for="State">State </label><input type="text" name="State" id="State" class="sf" /></p>
                 <p><label for="Zip">Zip </label><input type="text" name="Zip" id="Zip" class="sf" /></p>
                 <p><label for="Phone">Phone </label><input type="text" name="Phone" id="Phone" class="sf" /></p>
                 <p><label for="Role">Role </label><input type="text" name="Role" id="Role" class="sf" /></p>
                <p>
                    <button type="button" onclick="GridUtil.saveRow();">Submit</button>
                </p>

                </fieldset>
         </form>
     </div>
</body>
</html>