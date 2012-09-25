<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TimeSheetObjects._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        TimeSheet 360 Object Maker!
    </h2>
    <div>
        <asp:HiddenField ID="dataSource" runat="server" />
        <br />
        <asp:Label ID="lblDataSource" runat="server"></asp:Label><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Choose Table"></asp:Label><br />
        <asp:DropDownList ID="cmbTable" runat="server">
        </asp:DropDownList>&nbsp;
        <br />
        <br />
        <asp:Button ID="cmdMakeObject" runat="server" Text="Make Object" 
            onclick="cmdMakeObject_Click" />
        <asp:GridView ID="dbfields" runat="server">
        </asp:GridView>
        &nbsp;<asp:TextBox ID="txtResults" runat="server" Height="320px" TextMode="MultiLine"
            Width="792px"></asp:TextBox>
    </div>
</asp:Content>
