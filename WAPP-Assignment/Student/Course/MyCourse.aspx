﻿<%@ Page Title="My Courses" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="MyCourse.aspx.cs" Inherits="WAPP_Assignment.MyCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="CourseContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="CoursePanel" runat="server">
        <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
  <script>
    $("a.unenroll-link").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })
  </script>
</asp:Content>
