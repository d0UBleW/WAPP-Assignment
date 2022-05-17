<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="WAPP_Assignment.Student.Feedback" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
    <br />
    <asp:TextBox ID="ContentTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Button ID="SubmitBtn" runat="server" Text="Submit" />
  </form>
</asp:Content>
