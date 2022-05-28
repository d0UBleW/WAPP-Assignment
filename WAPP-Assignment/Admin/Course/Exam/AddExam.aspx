<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="WAPP_Assignment.Admin.AddExam" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:HyperLink ID="EditLink" runat="server" Text="Edit Exam Menu"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Add Exam</li>
</asp:Content>

<asp:Content ID="AddExamContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="TitleTxtBox"
          runat="server"
          CssClass="form-control"
          Placeholder="Exam Title"
          ToolTip="ExamTitle"
          Required="required"
          ></asp:TextBox>
        <label for="<%= TitleTxtBox.ClientID %>" class="text-muted">Exam Title</label>
      </div>
      <div class="form-check mb-3">
        <asp:CheckBox ID="RetakeChkBox" runat="server" Text="Allow Retake" />
      </div>
      <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" CssClass="btn btn-outline-primary" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })

    const $retakeChkBox = $("input[id$='RetakeChkBox']")
    $retakeChkBox.addClass("form-check-input")
    $retakeChkBox.css({ "cursor": "pointer" })

    const $retakeLbl = $retakeChkBox.siblings("label")
    $retakeLbl.addClass("form-check-label")
    $retakeLbl.css({"cursor": "pointer"})
  </script>
</asp:Content>
