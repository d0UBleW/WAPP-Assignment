﻿<%@ Page Title="All Courses" Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ListCourse.aspx.cs" Inherits="WAPP_Assignment.ListCourse" %>

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
      <asp:Label ID="SearchLbl" runat="server" Text="Search: "></asp:Label>
      <asp:TextBox ID="SearchTitleTxtBox" runat="server" CssClass="form-control" Placeholder="Search"></asp:TextBox>
      <asp:TextBox ID="SearchCatTxtBox" runat="server" Style="display: none;" CssClass="form-control" Placeholder="Search"></asp:TextBox>
      <ajaxToolkit:AutoCompleteExtender ID="AutoTitle" runat="server" ServiceMethod="SearchTitle"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchTitleTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <ajaxToolkit:AutoCompleteExtender ID="AutoCat" runat="server" ServiceMethod="SearchCategory"
        MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
        CompletionSetCount="10" TargetControlID="SearchCatTxtBox" FirstRowSelected="false">
      </ajaxToolkit:AutoCompleteExtender>
      <br />
      <asp:Label ID="FilterLbl" runat="server" Text="Filter: "></asp:Label>
      <br />
      <asp:DropDownList ID="FilterList" runat="server">
        <asp:ListItem Selected="True" Text="Title" Value="title"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Category" Value="category"></asp:ListItem>
      </asp:DropDownList>
      <br />
      <div class="form-check" id="EnrollmentDiv" runat="server">
        <input type="checkbox" id="enrollmentChk" class="form-check-input" />
        <label for="enrollmentChk" class="form-check-label">Unenrolled</label>
        <br />
      </div>
      <br />
      <button type="button" id="searchBtn" class="btn btn-primary btn-sm">Search</button>
    </div>
    <br />
    <br />
    <asp:Panel ID="CoursePanel" runat="server" CssClass="container">
      <asp:Panel ID="GridPanel" runat="server" CssClass="row row-cols-3 g-4">
        <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
      </asp:Panel>
    </asp:Panel>
  </form>
  <input type="hidden" id="NavLocation" value="course" disabled="disabled" />
  <script>
    $("a.del-course-link").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })

    $("a.unenroll-link").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })

    const toggleEnrollment = ($el) => {
      const $unenrollLink = $(".unenroll-link")
      $unenrollLink.closest(".course-container").show();
      if ($el.is(":checked")) {
        $unenrollLink.closest(".course-container").hide();
      }
    }

    $("#enrollmentChk").on('change', function () {
      toggleEnrollment($(this))
    })
  </script>
</asp:Content>
