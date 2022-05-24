<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/addCourse.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/ListCourse.aspx">All Courses</a></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Course</li>
</asp:Content>

<asp:Content ID="EditCourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" CssClass="container" runat="server">
      <asp:Image ID="ThumbnailImg" runat="server" Height="200" Width="200" />
      <br />
      <asp:FileUpload ID="ThumbnailUpload" runat="server" />
      <br />
      <br />
      <asp:LinkButton ID="RemoveLinkBtn" runat="server" Text="Remove Image" CssClass="btn btn-secondary btn-md" OnClick="RemoveLinkBtn_Click"></asp:LinkButton>
      <br />
      <br />
      <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
      <asp:TextBox ID="TitleTxtBox" runat="server" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="DescLbl" runat="server" Text="Description"></asp:Label>
      <br />
      <asp:TextBox ID="DescTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
      <br />
      <br />
      <asp:ListBox ID="CatList" runat="server" Height="200px" Width="200px" SelectionMode="Multiple"></asp:ListBox>
      <br />
      <asp:TextBox ID="CatTxtBox" runat="server"></asp:TextBox>
      <ajaxToolkit:AutoCompleteExtender ID="Auto1" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="CatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <button id="CatAddBtn" type="button">Add Category</button>
      <button id="CatDelBtn" type="button">Remove Category</button>
      <asp:HiddenField ID="CatField" runat="server" />
      <br />
      <br />
      <asp:Button ID="EditBtn" runat="server" Text="Edit Course" OnClick="EditBtn_Click" />
      <br />
      <asp:Panel ID="UploadStatusPanel" runat="server">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
        <br />
      </asp:Panel>
      <br />
      <asp:Button ID="AddChapBtn" runat="server" Text="Add Chapter" OnClick="AddChapBtn_Click" />
      <br />
      <br />
      <asp:PlaceHolder ID="ChapterPlaceholder" runat="server"></asp:PlaceHolder>
      <br />
      <br />
      <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" />
      <br />
      <br />
      <asp:PlaceHolder ID="ExamPlaceholder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
