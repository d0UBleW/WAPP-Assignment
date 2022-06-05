<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="WAPP_Assignment.Admin.Student.StudentList" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Student</li>
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h1 class="border-bottom mb-3">Student</h1>
    <form id="form1" runat="server">
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="student_id" DataSourceID="StudentListDataSource" CssClass="table table-hover table-striped" AllowSorting="True" AllowPaging="True">
        <Columns>
          <asp:BoundField DataField="student_id" HeaderText="student_id" InsertVisible="False" ReadOnly="True" SortExpression="student_id" Visible="False" />
          <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
          <asp:BoundField DataField="full_name" HeaderText="Full Name" SortExpression="full_name" />
          <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
          <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
          <asp:HyperLinkField DataNavigateUrlFields="student_id" DataNavigateUrlFormatString="/Student/Profile.aspx?student_id={0}" HeaderText="Profile" Text="View">
            <ControlStyle CssClass="btn btn-primary btn-sm" />
          </asp:HyperLinkField>
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="StudentListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT [student_id], [username], [full_name], [email], [gender] FROM [student]"></asp:SqlDataSource>
    </form>
  </div>
  <input type="hidden" id="NavLocation" value="student" disabled="disabled" />
</asp:Content>
