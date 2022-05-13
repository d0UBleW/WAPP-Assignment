 <%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WAPP_Assignment.Home" %> 

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server">
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <h1>Home Page</h1>
    <input id="HomeNavHidden" name="HomeNavHidden" type="hidden" value="Home" disabled="disabled" />
</asp:Content>
