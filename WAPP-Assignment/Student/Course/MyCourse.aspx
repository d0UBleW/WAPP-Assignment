<%@ Page Title="My Courses" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="MyCourse.aspx.cs" Inherits="WAPP_Assignment.MyCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/searchCourse.js" defer></script>
  <link rel="stylesheet" href="/Content/course.css" />
</asp:Content>

<asp:Content ID="CourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Panel ID="CoursePanel" runat="server" CssClass="container">
    <h1 class="border-bottom mb-3">My Courses</h1>
    <asp:Panel ID="GridPanel" runat="server" CssClass="row g-4">
      <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
  </asp:Panel>
</asp:Content>
