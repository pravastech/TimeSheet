<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TimeSheet.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div>
        <br />
        <div>
            <h1 style="width: 320px; font-size: x-large;">
                    manager report
               </h1>
        </div>

       <%-- <div>
            <ul>
                <li style="width: 140px; margin-left: 0px"><a id="user" runat="server" href="~/">choose user</a></li>
            </ul>
        </div>
        <div>
            <ul style="width: 137px">
                <li style="width:140px"><a id="fromdate" runat="server" href="~/">fromdate</a></li>
            </ul>
        </div>--%>
        <div style="height: 208px; width: 194px">
            <div>
            <ul>
                <li style="width: 140px; margin-left: 0px"><a id="user" runat="server" href="~/">choose user</a></li>
                    <%--<asp:Literal ID="ltrHiddens" runat="server"></asp:Literal>--%>
                <p><label for="devusername">choose user</label><select  name="chooseuser" id="chooseuser">
<asp:Literal ID="ltrchooseuser" runat="server"></asp:Literal>
</select></p>
            </ul>
        </div>
        <div>
            <ul style="width: 137px">
                <li style="width:140px"><a id="fromdate" runat="server" href="~/">fromdate</a></li>
            </ul>
        </div>
            <div>
                <ul style="width: 137px">
                <li style="width:140px"><a id="todate" runat="server" href="~/">todate</a></li>
            </ul>
            </div>
            <div>
                <ul style="width: 137px">
                <li style="width:140px"><a id="refresh" runat="server" href="~/">refresh</a>&nbsp;
                    <asp:Button ID="Btnsubmit" runat="server" Text="submit" />
                    </li>
            </ul>
            </div>
        </div>

        <br />
        <div>
             <div>
                 <table>
        <tr><td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A1" runat="server" >SNO</a>
            </ul></div></td>
            <td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A2" runat="server" >DATE</a>
            </ul></div></td>
            <td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A3" runat="server" >Project</a>
            </ul></div></td>
            <td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A4" runat="server" >Task</a>
            </ul></div></td>
            <td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A5" runat="server" >HOURS</a>
            </ul></div></td>
            <td style="vertical-align:top;width:50%;"><div class="divBox">
            <ul style="width: 137px; height: 16px;">
                <a id="A6" runat="server" >Status</a>
            </ul></div></td>

                 </table>
               
            </div>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
