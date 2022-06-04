<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Grades" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="ViewCourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">Grades</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="GradesDataSource" CssClass="table table-hover table-striped" AllowSorting="True" AllowPaging="True">
        <Columns>
          <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
          <asp:BoundField DataField="full_name" HeaderText="Full Name" SortExpression="full_name" />
          <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
          <asp:BoundField DataField="gender" HeaderText="Gender" SortExpression="gender" />
          <asp:BoundField DataField="value" HeaderText="Score" SortExpression="value" />
          <asp:BoundField DataField="total" HeaderText="Total" ReadOnly="True" SortExpression="total" />
          <asp:HyperLinkField DataNavigateUrlFields="exam_id,student_id" DataNavigateUrlFormatString="/Student/Learn/ReviewExam.aspx?exam_id={0}&amp;student_id={1}" HeaderText="Review" Text="Review" ControlStyle-CssClass="btn btn-primary btn-sm" />
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="GradesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT exam.exam_id, student.student_id, exam.title, student.full_name, student.email, student.gender, grade.value, (SELECT SUM(weight) FROM question WHERE question.exam_id=grade.exam_id) AS total FROM student INNER JOIN grade ON grade.student_id = student.student_id INNER JOIN exam ON grade.exam_id=exam.exam_id WHERE grade.exam_id IN (SELECT exam_id FROM exam WHERE course_id=@course_id)">
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="course_id" QueryStringField="course_id" />
        </SelectParameters>
      </asp:SqlDataSource>
    </div>
  </form>
</asp:Content>
