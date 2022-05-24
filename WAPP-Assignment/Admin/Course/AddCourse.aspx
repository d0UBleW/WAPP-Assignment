<%@ Page Title="Add Course" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.AddCourse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/addCourse.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Add Course</li>
</asp:Content>

<asp:Content ID="AddCourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="container">
      <asp:Label ID="TitleLbl" runat="server" Text="Course Title"></asp:Label>
      <br />
      <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="DescLbl" runat="server" Text="Course Description"></asp:Label>
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
      <asp:Label ID="ThumbnailLbl" runat="server" Text="Thumbnail"></asp:Label>
      <br />
      <asp:FileUpload ID="ThumbnailUpload" runat="server" />
      <br />
      <asp:Panel ID="UploadStatusPanel" runat="server">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
        <br />
      </asp:Panel>
      <br />
      <asp:Button ID="AddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
