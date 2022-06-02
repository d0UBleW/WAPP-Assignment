<%@ Page Title="All Courses" Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ListCourse.aspx.cs" Inherits="WAPP_Assignment.ListCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/searchCourse.js" defer></script>
  <link rel="stylesheet" href="/Content/course.css" />
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">All Courses</li>
</asp:Content>

<asp:Content ID="CourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="container">
      <div class="mb-3">
        <div class="input-group" id="searchTitle">
          <div class="form-floating flex-grow-1">
            <asp:TextBox
              ID="SearchTitleTxtBox"
              runat="server"
              CssClass="form-control"
              Placeholder="Search"
              ToolTip="Search"></asp:TextBox>
            <label for="<%= SearchTitleTxtBox.ClientID %>" class="text-muted">Search</label>
          </div>
          <button type="button" name="searchBtn" class="btn btn-outline-secondary">
            <i class="bi bi-search"></i>
          </button>
        </div>

        <div class="input-group" id="searchCat" style="display: none;">
          <div class="form-floating flex-grow-1">
            <asp:TextBox
              ID="SearchCatTxtBox"
              runat="server"
              CssClass="form-control"
              Placeholder="Search"
              ToolTip="Search"
              ></asp:TextBox>
            <label for="<%= SearchCatTxtBox.ClientID %>" class="text-muted">Search</label>
          </div>
            <button type="button" name="searchBtn" class="btn btn-outline-secondary">
              <i class="bi bi-search"></i>
            </button>
        </div>
      </div>
      <ajaxToolkit:AutoCompleteExtender ID="AutoTitle" runat="server" ServiceMethod="SearchTitle"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchTitleTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <ajaxToolkit:AutoCompleteExtender ID="AutoCat" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchCatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>

      <div class="input-group mb-3">
        <span class="input-group-text">
          <i class="bi bi-funnel"></i>
        </span>
        <div class="form-floating flex-grow-1">
          <asp:DropDownList ID="FilterList" runat="server" CssClass="form-select">
            <asp:ListItem Selected="True" Text="Title" Value="title"></asp:ListItem>
            <asp:ListItem Selected="False" Text="Category" Value="category"></asp:ListItem>
          </asp:DropDownList>
          <label for="<%= FilterList.ClientID %>" class="text-muted">Filter</label>
        </div>
      </div>

      <div class="form-check" id="EnrollmentDiv" runat="server">
        <input type="checkbox" id="enrollmentChk" class="form-check-input" />
        <label for="enrollmentChk" class="form-check-label">Unenrolled</label>
        <br />
      </div>
    </div>
    <br />
    <br />
    <asp:Panel ID="CoursePanel" runat="server" CssClass="container">
      <h1 class="border-bottom mb-3">All Courses</h1>
      <asp:Panel ID="GridPanel" runat="server" CssClass="row g-4">
        <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
      </asp:Panel>
    </asp:Panel>
  </form>
  <input type="hidden" id="NavLocation" value="course" disabled="disabled" />
</asp:Content>
