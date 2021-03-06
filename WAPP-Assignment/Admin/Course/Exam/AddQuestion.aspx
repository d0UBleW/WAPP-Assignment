<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddQuestion.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.AddQuestion" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
  <script src="/Scripts/ckeditor.js" defer></script>
  <script src="/Scripts/addOption.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:HyperLink ID="EditLink" runat="server" Text="Edit Exam Menu"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:Label ID="ExamLbl" runat="server"></asp:Label></li>
  <li class="breadcrumb-item"><asp:HyperLink ID="EditExamLink" runat="server" Text="Edit Exam"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Add Question</li>
</asp:Content>

<asp:Content ID="AddQuestionContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="QueNoTxtBox"
          runat="server"
          TextMode="Number"
          CssClass="form-control"
          ToolTip="Question Number"
          Placeholder="Question Number"
          Required="required"
          Min="1"
          aria-label="Question Number"></asp:TextBox>
        <label for="<%= QueNoTxtBox.ClientID %>" class="text-muted">Question Number</label>
      </div>
      <div class="alert alert-danger mb-3" style="display: none;" role="alert">
        <asp:RangeValidator
          ID="QueNoRangeValidator"
          runat="server"
          ErrorMessage="Invalid Question Number"
          ControlToValidate="QueNoTxtBox"
          Type="Integer"
          SetFocusOnError="True"
          MinimumValue="1"></asp:RangeValidator>
      </div>
      <div class="mb-3">
        <label for="<%= EditorTxtBox.ClientID %>" class="form-label">Question Content</label>
        <asp:TextBox
          ID="EditorTxtBox"
          runat="server"
          TextMode="MultiLine"
          CssClass="form-control"
          ToolTip="Question Content"
          Placeholder="Question Content"
          aria-label="Question Content"
          ClientIDMode="Static"></asp:TextBox>
      </div>
      <div class="input-group mb-3">
        <button type="button" id="AddOptBtn" class="btn btn-outline-secondary">Add</button>
        <div class="form-floating flex-grow-1">
          <input type="text" id="OptTxtBox" class="form-control" aria-label="Option Textbox" placeholder="Option" title="Option" />
          <label for="OptTxtBox" class="text-muted">Option</label>
        </div>
      </div>
      <asp:Label ID="OptStatus" runat="server" ForeColor="Red" Style="display: none;" Text="Please choose at least one option as answer."></asp:Label>
      <br />
      <h4 class="mb-3">Option List</h4>
      <ul class="list-group mb-3" id="OptList">
      </ul>
      <asp:Button ID="AddBtn" runat="server" OnClick="AddBtn_Click" OnClientClick="return CheckOption();" Text="Add Question" CssClass="btn btn-outline-primary" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
    $("input[id$='QueNoTxtBox']").on('change', function () {
      const $val = $("span[id$='QueNoRangeValidator']")
      const valid = RangeValidatorEvaluateIsValid($val.get(0))
      if (!valid) {
        $val.closest("div").show()
      }
      else {
        $val.closest("div").hide()
      }
    })
    $("#OptTxtBox").keypress(function (e) {
      var key = e.keyCode || e.which
      if (key == 13) {
        $("#AddOptBtn").click()
        return false
      }
    })
  </script>
</asp:Content>
