<%@ Page Title="Edit Chapter" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.EditChapter" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
  <script src="/Scripts/ckeditor.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/ListCourse.aspx">All Courses</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="EditLink" runat="server" Text="Edit Course"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Chapter</li>
</asp:Content>

<asp:Content ID="EditChapterContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Label ID="ChapNoLbl" runat="server" Text="Chapter No."></asp:Label>
      <asp:TextBox ID="ChapNoTxtBox" runat="server" TextMode="Number" Min="1" Required="required"></asp:TextBox>
      <asp:RangeValidator ID="ChapNoRangeValidator" runat="server" ErrorMessage="Invalid Chapter Number"
        ForeColor="Red" ControlToValidate="ChapNoTxtBox" Type="Integer" SetFocusOnError="True" MinimumValue="1"></asp:RangeValidator>
      <br />
      <br />
      <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
      <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
      <br />
      <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
      <br />
      <br />
      <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit" />
      <asp:HiddenField ID="CourseIDField" runat="server" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
