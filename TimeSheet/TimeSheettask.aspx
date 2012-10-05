<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheettask.aspx.cs" Inherits="TimeSheet.timesheettask" %>

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
        return "TimeSheetTaskUIForm";
    }

    GridUtil.Title = function () {
        return "timesheettask";
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

    <div class="form_default" style="display:none;" id="TimeSheetTaskUIForm">
        <form id="form" action="" method="post">
            <asp:Literal ID="ltrHiddens" runat="server"></asp:Literal>
            <fieldset>
                 <p><label for="Username">UserName </label><input type="text" name="username" id="username" class="sf" /></p>
                 <p><label for="ProjectName">ProjectName</label><input type="text" name="projectname" id="projectname" /></p>
                <p><label for="TaskName">TaskName</label><input type="text" name="taskname" id="taskname" /></p>
                <p><label for="TaskDate">TaskDate</label><input type="date" name="taskdate" id="taskdate" /></p>
                <p><label for="Percentage">Percentage</label><input type="text"  name="percentage" id="percentage" /></p>
                 <p><label for="Notes">Notes</label><input type="text" name="notes" id="notes" class="sf" /></p>
                 <p><label for="CodeSnippet">CodeSnippet</label><input type="text" name="codesnippet" id="codesnippet" class="sf" /></p>
                <p>
                    <button type="button" onclick="GridUtil.saveRow();">Submit</button>
                </p>


                </fieldset>
         </form>
     </div>
</body>
</html>
