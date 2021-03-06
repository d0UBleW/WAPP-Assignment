<%@ Page Title="Add Course" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.AddCourse" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/category.js" defer></script>
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
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="TitleTxtBox"
          runat="server"
          CssClass="form-control"
          TextMode="SingleLine"
          Placeholder="Course Title"
          ToolTip="Course Title"
          MaxLength="100"
          data-max-len="true"
          Required="required"
          ></asp:TextBox>
        <label for="<%= TitleTxtBox.ClientID %>" class="text-muted">Course Title</label>
      </div>
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="DescTxtBox"
          runat="server"
          CssClass="form-control"
          TextMode="MultiLine"
          Placeholder="Course Description"
          ToolTip="Course Description"
          MaxLength="1000"
          data-max-len="true"
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
            CssClass="form-control"
            TextMode="SingleLine"
            Placeholder="Course Category"
            ToolTip="Course Category"
            MaxLength="50"
            data-max-len="true"
            ></asp:TextBox>
          <label for="<%= CatTxtBox.ClientID %>" class="text-muted">Course Category</label>
        </div>
        <button id="CatAddBtn" type="button" class="btn btn-outline-primary">Add Category</button>
        <button id="CatDelBtn" type="button" class="btn btn-outline-danger">Remove Category</button>
      </div>
      <ajaxToolkit:AutoCompleteExtender ID="Auto1" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="CatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>


      <div class="input-group mb-3">
        <span class="input-group-text">
          Thumbnail
        </span>
        <asp:FileUpload ID="ThumbnailUpload" runat="server" CssClass="form-control"/>
        <span class="input-group-text">
          image only, max. 400 KBs
        </span>
      </div>

      <asp:HiddenField ID="CatField" runat="server" />
      <asp:Panel ID="UploadStatusPanel" CssClass="alert alert-danger mb-3" runat="server" role="alert">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
      </asp:Panel>
      <asp:Button ID="CourseAddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" CssClass="btn btn-outline-primary" role="button" />
    </div>
  </form>
  <script>
    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
    $("input[id$='CourseAddBtn']").on('click', function () {
      return confirm('Proceed to add course?')
    })
  </script>
</asp:Content>
