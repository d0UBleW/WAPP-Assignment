<%@ Page Title="Edit Exam Menu" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="EditExamMenu.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.EditExamMenu" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Exam Menu</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" CssClass="btn btn-primary btn-md mb-3" />
      <div class="list-group list-group-flush text-truncate">
        <asp:PlaceHolder ID="ExamPlaceholder" runat="server"></asp:PlaceHolder>
      </div>
    </div>
  </form>
</asp:Content>
