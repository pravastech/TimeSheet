<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManagerReport.aspx.cs" Inherits="TimeSheet.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function saveRow() {
            $('#downloadFile').val('1');
            $('form:last').submit();
        }
</script>
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 53px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <div>
            <h1 style="width: 320px; font-size: x-large;">manager report
            </h1>
        </div>
        <div style="height: 194px; width: 398px; background-color: #808080;" id="user" class="user">
            <div>
                <div class="form_default" style="display: none;" id="taskUIForm">
                    <asp:Literal ID="ltrMessage" runat="server"></asp:Literal>

                </div>
                <div style="background-color: #C0C0C0">
                    <table>
                        <tr>
                            <td>
                                <ul>
                                    <%--<li style="width: 140px; margin-left: 0px"><a id="user1" runat="server" href="~/">choose user</a></li>--%>
                                    <p>
                                        <label for="devusername">choose user</label>
                                    </p>
                                </ul>
                            </td>
                            <td style="width: 300px;" align="center">
                                <select name="chooseuser" id="Select1" size="1" style="background-color: #FFFFFF; width: 100px;">
                                    <asp:Literal ID="ltrChooseUsers" runat="server"></asp:Literal>
                                </select>
                            </td>
                        </tr>

                        <tr>
                            <td style="vertical-align: top; width: 50%;">
                                <div class="divbox">
                                    <ul style="width: 137px">
                                        <p>
                                            <label for="fromdate">From Date</label><input type="date" name="begindate" id="beginFromDate" />
                                        </p>
                                    </ul>
                                </div>
                            </td>
                            <td style="vertical-align: top; width: 50%;">
                                <div class="divbox">
                                    <ul style="width: 137px">
                                        <p>
                                            <label for="todate">To Date</label><input type="date" name="enddate" id="beginToDate" />
                                        </p>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <ul style="width: 137px">
                        <li style="width: 140px">
                            <button type="button" onclick="saveRow();" style="text-align: right" id="btnrefresh">refresh</button>
                        </li>
                    </ul>
                </div>
            </div>
            <br />

            <div style="grid-column-align: center">

                <table style="background-color: #800000">
                    <tr>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A1" runat="server">SNO</a>
                                </ul>
                            </div>
                        </td>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A2" runat="server">DATE</a>
                                </ul>
                            </div>
                        </td>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A3" runat="server">Project</a>
                                </ul>
                            </div>
                        </td>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A4" runat="server">Task</a>
                                </ul>
                            </div>
                        </td>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A5" runat="server">HOURS</a>
                                </ul>
                            </div>
                        </td>
                        <td style="vertical-align: top;" class="auto-style1">
                            <div class="divBox">
                                <ul style="width: 137px; height: 16px;">
                                    <a id="A6" runat="server">Status</a>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <br />
        </div>
    </div>
</asp:Content>
