<%@ Page Title="Edit Chapter Menu" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="EditChapMenu.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Chapter.ListChapter" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Chapter</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Button ID="AddChapBtn" runat="server" Text="Add Chapter" OnClick="AddChapBtn_Click" CssClass="btn btn-primary btn-md mb-3" />
      <div class="list-group list-group-flush text-truncate">
        <asp:PlaceHolder ID="ChapterPlaceholder" runat="server"></asp:PlaceHolder>
      </div>
    </div>
  </form>
</asp:Content>
