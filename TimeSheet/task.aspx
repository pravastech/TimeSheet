<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="task.aspx.cs" Inherits="TimeSheet.task" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <script type="text/javascript" src="/js/jquery.js?20120922"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.13.custom.min.js?20120922"></script>
    <script type="text/javascript" src="/js/General.js?20120922"></script>
    <script type="text/javascript" src="/js/GridUtil.js?20121001"></script>
    
<script type="text/javascript">
    var gridData = [];

    $().ready(function () {
        /* Later Set the DynTable */
    });

    GridUtil.UIFormName = function () {
        return "taskUIForm";
    }

    GridUtil.Title = function () {
        return "task";
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

    <div class="form_default" style="display:none;" id="taskUIForm">
        <form id="form" action="" method="post">
            <asp:Literal ID="ltrHiddens" runat="server"></asp:Literal>
            <fieldset>
                 <p><label for="projectname">projectname </label><input type="text" name="projectname" id="projectname" class="sf" /></p>
                 <p><label for="taskname">taskname</label><input type="text" name="taskname" id="taskname" /></p>
                <p><label for="begindate">begindate</label><input type="date" name="begindate" id="begindate" /></p>
                <p><label for="enddate">enddate</label><input type="date" name="enddate" id="enddate" /></p>
                <p><label for="devusername">devusername</label><select  name="devusername" id="devusername">
<asp:Literal ID="ltrdevUsers" runat="server"></asp:Literal>
</select></p>
                    <%--<input type="se" list="browsers"/><datalist id="browsers">
 <option value="Internet Explorer"></option>
 <option value="Firefox"></option>
 <option value="Chrome"></option>
 <option value="Opera"></option>
 <option value="Safari"></option>
</datalist></p>--%>
                    <%--<input type="text" name="devusername" id="devusername" /></p>--%>
                 <p><label for="qausername">qausername </label><select  name="qausername" id="qausername" class="sf"/>
  <asp:Literal ID="ltrqausers" runat="server"></asp:Literal>

</select></p>
                     <%--<input type="text" name="qausername" id="qausername" class="sf" /></p>--%>
                 <p><label for="approvedusername">approvedusername </label><select  name="approvedusername" id="approvedusername" class="sf"/>
  <asp:Literal ID="ltrapprovedusers" runat="server"></asp:Literal>

</select></p>
                     <%--<input type="text" name="approvedusername" id="approvedusername" class="sf" />  </p>--%>
                <p>
                    <button type="button" onclick="GridUtil.saveRow();">Submit</button>
                </p>

                </fieldset>
         </form>
     </div>
</body>
</html>
