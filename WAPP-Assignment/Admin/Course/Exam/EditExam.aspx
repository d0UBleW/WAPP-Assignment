<%@ Page Title="Edit Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditExam.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.EditExam" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:HyperLink ID="EditLink" runat="server" Text="Edit Exam Menu"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:Label ID="ExamLbl" runat="server"></asp:Label></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Exam</li>
</asp:Content>

<asp:Content ID="EditExamContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:Panel ID="MainPanel" CssClass="container" runat="server">
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="TitleTxtBox"
          runat="server"
          CssClass="form-control"
          Placeholder="Exam Title"
          ToolTip="ExamTitle"
          Required="required"
          MaxLength="100"
          data-max-len=true
          ></asp:TextBox>
        <label for="<%= TitleTxtBox.ClientID %>" class="text-muted">Exam Title</label>
      </div>
      <div class="form-check mb-3">
        <asp:CheckBox ID="RetakeChkBox" runat="server" Text="Allow Retake" />
      </div>
      <asp:Button ID="EditBtn" runat="server" Text="Edit Exam" OnClick="EditBtn_Click" CssClass="btn btn-primary btn-main-date mb-3" />
      <br />
      <asp:HyperLink ID="AddQueLink" runat="server" Text="Add Question" CssClass="btn btn-outline-primary mb-3"></asp:HyperLink>
      <asp:Panel ID="QuePanel" runat="server" CssClass="list-group list-group-flush text-truncate">
        <asp:PlaceHolder ID="QuePlaceholder" runat="server"></asp:PlaceHolder>
      </asp:Panel>
    </asp:Panel>
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
