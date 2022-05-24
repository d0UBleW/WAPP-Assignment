<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="WAPP_Assignment.Admin.AddExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/ListCourse.aspx">All Courses</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="EditLink" runat="server" Text="Edit Course"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Add Exam</li>
</asp:Content>

<asp:Content ID="AddExamContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div>
      <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
      <asp:TextBox ID="TitleTxtBox" runat="server"></asp:TextBox>
      <asp:CheckBox ID="RetakeChkBox" runat="server" Text="Allow Retake" />
      <br />
      <br />
      <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
