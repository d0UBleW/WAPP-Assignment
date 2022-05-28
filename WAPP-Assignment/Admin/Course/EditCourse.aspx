<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/addCourse.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Edit Course</li>
</asp:Content>

<asp:Content ID="EditCourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" CssClass="container" runat="server">
      <asp:Image
        ID="ThumbnailImg"
        runat="server"
        Height="200"
        Width="200"
        Style="object-fit: cover;"
        CssClass="img-thumbnail mb-3" />
      <div class="input-group mb-3">
        <span class="input-group-text">Thumbnail
        </span>
        <asp:FileUpload ID="ThumbnailUpload" runat="server" CssClass="form-control" />
        <asp:LinkButton ID="RemoveLinkBtn" runat="server" Text="Remove Image" CssClass="btn btn-outline-danger" OnClick="RemoveLinkBtn_Click"></asp:LinkButton>
      </div>
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="TitleTxtBox"
          runat="server"
          Placeholder="Course Title"
          ToolTip="Course Title"
          TextMode="SingleLine"
          CssClass="form-control"
          Required="required"
          aria-label="Course Title"></asp:TextBox>
        <label for="<%= TitleTxtBox.ClientID %>" class="text-muted">Course Title</label>
      </div>

      <div class="form-floating mb-3">
        <asp:TextBox
          ID="DescTxtBox"
          runat="server"
          Placeholder="Course Description"
          ToolTip="Course Description"
          TextMode="MultiLine"
          CssClass="form-control"
          Required="required"
          aria-label="Course Description"
          style="height: 100px;"
          ></asp:TextBox>
        <label for="<%= DescTxtBox.ClientID %>" class="text-muted">Course Description</label>
      </div>
      <div class="list-group mb-3">
        <asp:ListBox ID="CatList" runat="server" Height="200px" Width="100%" SelectionMode="Multiple"></asp:ListBox>
      </div>
      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1">
          <asp:TextBox
            ID="CatTxtBox"
            runat="server"
            Placeholder="Course Category"
            ToolTip="Course Category"
            TextMode="SingleLine"
            CssClass="form-control"
            aria-label="Course Category"></asp:TextBox>
          <label for="<%= CatTxtBox.ClientID %>" class="text-muted">Course Category</label>
        </div>
        <button id="CatAddBtn" type="button" class="btn btn-outline-primary">Add Category</button>
        <button id="CatDelBtn" type="button" class="btn btn-outline-danger">Remove Category</button>
      </div>
      <ajaxToolkit:AutoCompleteExtender ID="Auto1" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="CatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <asp:HiddenField ID="CatField" runat="server" />
      <asp:Button ID="EditBtn" runat="server" Text="Edit Course" OnClick="EditBtn_Click" CssClass="btn btn-outline-primary" />
      <br />
      <asp:Panel ID="UploadStatusPanel" runat="server">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
        <br />
      </asp:Panel>
    </asp:Panel>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
