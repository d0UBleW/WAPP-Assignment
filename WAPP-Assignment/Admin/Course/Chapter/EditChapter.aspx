<%@ Page Title="Edit Chapter" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.EditChapter" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
  <script src="/Scripts/ckeditor.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:HyperLink ID="EditLink" runat="server" Text="Edit Chapter Menu"></asp:HyperLink></li>
  <li class="breadcrumb-item"><asp:Label ID="ChapLbl" runat="server"></asp:Label></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Chapter</li>
</asp:Content>

<asp:Content ID="EditChapterContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="ChapNoTxtBox"
          runat="server"
          CssClass="form-control"
          Placeholder="Chapter Number"
          ToolTip="Chapter Number"
          TextMode="Number"
          Required="required"
          Min="1"></asp:TextBox>
        <label for="<%= ChapNoTxtBox.ClientID %>" class="text-muted">Chapter Number</label>
      </div>
      <div class="mb-3" style="display: none;">
        <asp:RangeValidator
          ID="ChapNoRangeValidator"
          runat="server"
          ErrorMessage="Invalid Chapter Number"
          ForeColor="Red"
          ControlToValidate="ChapNoTxtBox"
          Type="Integer"
          SetFocusOnError="True"
          MinimumValue="1"
          ></asp:RangeValidator>
      </div>

      <div class="form-floating mb-3">
        <asp:TextBox
          ID="TitleTxtBox"
          runat="server"
          CssClass="form-control"
          Placeholder="Chapter Title"
          ToolTip="Chapter Title"
          TextMode="SingleLine"
          Required="required"></asp:TextBox>
        <label for="<%= TitleTxtBox.ClientID %>" class="text-muted">Chapter Title</label>
      </div>
      <div class="mb-3">
        <label for="<%= EditorTxtBox.ClientID %>" class="form-label">Chapter Content</label>
        <asp:TextBox
          ID="EditorTxtBox"
          runat="server"
          CssClass="form-control"
          Placeholder="Chapter Content"
          ToolTip="Chapter Content"
          TextMode="MultiLine"
          ClientIDMode="Static"></asp:TextBox>
      </div>
      <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit" CssClass="btn btn-outline-primary" />
      <asp:HiddenField ID="CourseIDField" runat="server" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
    $("input[id$='ChapNoTxtBox']").on('change', function () {
      const $val = $("span[id$='ChapNoRangeValidator']")
      const valid = RangeValidatorEvaluateIsValid($val.get(0))
      if (!valid) {
        $val.closest("div").show()
      }
      else {
        $val.closest("div").hide()
      }
    })
  </script>
</asp:Content>
