<%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WAPP_Assignment.Home" %> 

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="HomeContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Home Page</h1>
  <asp:PlaceHolder ID="NavLocPH" runat="server"></asp:PlaceHolder>
</asp:Content>
