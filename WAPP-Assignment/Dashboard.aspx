<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WAPP_Assignment.Dashboard" %>

<asp:Content ID="DashContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h1>My First Bootstrap Page</h1>
    <p>Resize this responsive page to see the effect!</p>
  </div>
  <form id="form1" runat="server">
  </form>
  <br />
  <br />
  <input type="hidden" id="NavLocation" value="Dashboard" disabled="disabled" />
</asp:Content>
