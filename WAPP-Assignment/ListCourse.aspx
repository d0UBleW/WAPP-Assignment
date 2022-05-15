<%@ Page Title="All Courses" Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ListCourse.aspx.cs" Inherits="WAPP_Assignment.ListCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/searchCourse.js" defer></script>
</asp:Content>

<asp:Content ID="CourseContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div>
      <asp:Label ID="SearchLbl" runat="server" Text="Search: "></asp:Label>
      <asp:TextBox ID="SearchTitleTxtBox" runat="server" CssClass="form-control" Placeholder="Search"></asp:TextBox>
      <asp:TextBox ID="SearchCatTxtBox" runat="server" Style="display: none;" CssClass="form-control" Placeholder="Search"></asp:TextBox>
      <asp:Label ID="FilterLbl" runat="server" Text="Filter: "></asp:Label>
      <asp:RadioButtonList ID="EnrollmentRadioList" runat="server">
        <asp:ListItem Selected="True" Text="All" Value="All"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Enrolled" Value="Enrolled"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Unenrolled" Value="Unenrolled"></asp:ListItem>
      </asp:RadioButtonList>
      <ajaxToolkit:AutoCompleteExtender ID="AutoTitle" runat="server" ServiceMethod="SearchTitle"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchTitleTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <ajaxToolkit:AutoCompleteExtender ID="AutoCat" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchCatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <asp:DropDownList ID="FilterList" runat="server">
        <asp:ListItem Selected="True" Text="Title" Value="title"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Category" Value="category"></asp:ListItem>
      </asp:DropDownList>
      <button type="button" id="searchBtn" class="btn btn-primary btn-sm">Search</button>
    </div>
  </form>
  <br />
  <br />
  <asp:HyperLink ID="DashboardLink" runat="server" NavigateUrl="/Dashboard.aspx" Text="Dashboard" CssClass="btn btn-secondary btn-sm"></asp:HyperLink>
  <asp:HyperLink ID="AddCourseLink" runat="server" Text="Add Course" NavigateUrl="/Admin/Course/AddCourse.aspx" CssClass="btn btn-secondary btn-sm"></asp:HyperLink>
  <br />
  <br />
  <br />
  <asp:Panel ID="CoursePanel" runat="server">
    <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
  </asp:Panel>
  <asp:Panel ID="ExamPanel" runat="server">
    <asp:PlaceHolder ID="ExamPlaceholder" runat="server"></asp:PlaceHolder>
  </asp:Panel>
</asp:Content>
