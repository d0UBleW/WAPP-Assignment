<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WAPP_Assignment.Dashboard" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="/Content/course.css" />
  <style>
    .statistic {
      align-items: center;
    }

    .statistic span {
      display: block;
      margin-bottom: 10px;
    }
  </style>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Dashboard</li>
</asp:Content>

<asp:Content ID="DashContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h1 class="border-bottom mb-3">Dashboard</h1>
    <asp:Panel ID="AdminPanel" runat="server" Visible="false">
      <asp:Panel ID="AdminGridPanel" runat="server" CssClass="row g-4">
        <div class="col-lg-6">
          <div class="card text-center">
            <div class="card-header">
              <h4>Course</h4>
            </div>
            <div class="row g-0 card-body">
              <div class="col">
                <div class="card-title" style="font-size: 8em">
                  <i class="bi bi-mortarboard-fill"></i>
                </div>
              </div>
              <div class="col p-4 fs-5 statistic d-flex flex-column justify-content-center align-items-start">
                <span>
                  <i class="bi bi-hash"></i>Count: <asp:Literal ID="CourseCount" runat="server"></asp:Literal>
                </span>
              </div>
              <div class="my-2">
                <a href="/ListCourse.aspx" class="btn btn-primary btn-lg">View</a>
              </div>
            </div>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="card text-center">
            <div class="card-header">
              <h4>Student</h4>
            </div>
            <div class="row g-0 card-body">
              <div class="col">
                <div class="card-title" style="font-size: 8em">
                  <i class="bi bi-person-fill"></i>
                </div>
              </div>
              <div class="col p-4 fs-5 statistic d-flex flex-column justify-content-center align-items-start">
                <span>
                  <i class="bi bi-hash"></i>Count: <asp:Literal ID="StudentCount" runat="server"></asp:Literal>
                </span>
                <span>
                  <i class="bi bi-gender-male"></i>Male: <asp:Literal ID="MaleCount" runat="server"></asp:Literal>
                </span>
                <span>
                  <i class="bi bi-gender-female"></i>Female: <asp:Literal ID="FemaleCount" runat="server"></asp:Literal>
                </span>
              </div>
              <div class="my-2">
                <a href="/Admin/StudentData/StudentList.aspx" class="btn btn-primary btn-lg">View</a>
              </div>
            </div>
          </div>
        </div>
      </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="StudentPanel" runat="server" Visible="false">
      <h2 class="border-bottom mb-3">Popular Courses</h2>
      <asp:Panel ID="StudentGridPanel" runat="server" CssClass="row g-4">
      </asp:Panel>
    </asp:Panel>
  </div>
  <input type="hidden" id="NavLocation" value="dashboard" disabled="disabled" />
</asp:Content>
