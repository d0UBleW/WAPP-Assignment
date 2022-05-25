<%@ Page Title="Edit Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditExam.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.EditExam" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/ListCourse.aspx">All Courses</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="EditLink" runat="server" Text="Edit Course"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Exam</li>
</asp:Content>

<asp:Content ID="EditExamContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:Panel ID="MainPanel" CssClass="container" runat="server">
      <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
      <asp:TextBox ID="TitleTxtBox" runat="server" Required="required"></asp:TextBox>
      <asp:CheckBox ID="RetakeChkBox" runat="server" Text="Allow retake" />
      <br />
      <br />
      <asp:Button ID="EditBtn" runat="server" Text="Edit Exam" OnClick="EditBtn_Click" />
      <br />
      <br />
      <asp:LinkButton ID="AddQueBtnLink" runat="server" Text="Add Question" CssClass="btn btn-secondary btn-md" OnClick="AddQueBtnLink_Click"></asp:LinkButton>
      <br />
      <br />
      <asp:Panel ID="QuePanel" runat="server">
        <asp:PlaceHolder ID="QuePlaceholder" runat="server"></asp:PlaceHolder>
      </asp:Panel>
    </asp:Panel>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
