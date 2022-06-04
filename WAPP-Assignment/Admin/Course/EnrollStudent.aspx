<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="EnrollStudent.aspx.cs" Inherits="WAPP_Assignment.Admin.StudentData.EnrollStudent" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Enroll Student</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">

  <form id="form1" runat="server">
  <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="student_id" DataSourceID="StudentDataSource" CssClass="table table-hover table-striped" AllowPaging="True">
    <Columns>
      <asp:BoundField DataField="student_id" HeaderText="student_id" InsertVisible="False" ReadOnly="True" SortExpression="student_id" Visible="False" />
      <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
      <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" Visible="False" />
      <asp:BoundField DataField="full_name" HeaderText="full_name" SortExpression="full_name" />
      <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
      <asp:BoundField DataField="gender" HeaderText="gender" SortExpression="gender" />
      <asp:BoundField DataField="profile" HeaderText="profile" SortExpression="profile" Visible="False" />
      <asp:TemplateField HeaderText="enroll" SortExpression="enroll">
        <EditItemTemplate>
          <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("enroll") %>' />
        </EditItemTemplate>
        <ItemTemplate>
          <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("enroll") %>' Enabled="true" />
        </ItemTemplate>
      </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="StudentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT *, CAST(0 AS BIT) AS enroll FROM student WHERE student_id NOT IN (SELECT DISTINCT student_id FROM enroll WHERE course_id=@course_id)">
      <SelectParameters>
        <asp:QueryStringParameter DefaultValue="0" Name="course_id" QueryStringField="course_id" />
      </SelectParameters>
    </asp:SqlDataSource>
    <asp:Button ID="EnrollBtn" runat="server" CssClass="btn btn-primary btn-md" Text="Enroll" OnClick="EnrollBtn_Click" />
  </form>
  </div>
</asp:Content>
