<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WAPP_Assignment.Dashboard" %>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Dashboard</li>
</asp:Content>

<asp:Content ID="DashContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h1>My First Bootstrap Page</h1>
    <p>Resize this responsive page to see the effect!</p>
  </div>
  <form id="form1" runat="server">
  </form>
  <br />
  <br />
  <input type="hidden" id="NavLocation" value="dashboard" disabled="disabled" />
</asp:Content>
