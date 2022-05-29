<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="EnrolledStudent.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.EnrolledStudent" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Enrolled Student</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">

      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="student_id" DataSourceID="EnrolledStudentDataSource" Width="785px" CssClass="table table-hover table-striped">
        <Columns>
          <asp:BoundField DataField="student_id" HeaderText="student_id" SortExpression="student_id" InsertVisible="False" ReadOnly="True" Visible="False" />
          <asp:BoundField DataField="full_name" HeaderText="Full Name" SortExpression="full_name" />
          <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
          <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
          <asp:BoundField DataField="course_id" HeaderText="course_id" SortExpression="course_id" Visible="False" />
          <asp:HyperLinkField DataNavigateUrlFields="course_id,student_id" DataNavigateUrlFormatString="/Student/Course/UnenrollCourse.aspx?course_id={0}&amp;student_id={1}" HeaderText="Action" Text="Unenroll" />
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="EnrolledStudentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT student.student_id, student.full_name, student.gender, student.email, enroll.course_id FROM student INNER JOIN enroll ON enroll.student_id = student.student_id WHERE (enroll.course_id = @course_id)">
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="course_id" QueryStringField="course_id" />
        </SelectParameters>
      </asp:SqlDataSource>
    </div>
  </form>
</asp:Content>
