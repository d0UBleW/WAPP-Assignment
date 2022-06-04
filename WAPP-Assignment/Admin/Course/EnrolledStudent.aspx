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

      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="student_id" DataSourceID="EnrolledStudentDataSource" CssClass="table table-hover table-striped" AllowSorting="True" AllowPaging="True">
        <Columns>
          <asp:BoundField DataField="student_id" HeaderText="student_id" SortExpression="student_id" InsertVisible="False" ReadOnly="True" Visible="False" />
          <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
          <asp:BoundField DataField="full_name" HeaderText="full_name" SortExpression="full_name" />
          <asp:BoundField DataField="gender" HeaderText="gender" SortExpression="gender" />
          <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
          <asp:BoundField DataField="course_id" HeaderText="course_id" SortExpression="course_id" Visible="False" />
          <asp:TemplateField HeaderText="unenroll" SortExpression="unenroll">
            <ItemTemplate>
              <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("unenroll") %>' Enabled="true" />
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="EnrolledStudentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT student.student_id, student.username, student.full_name, student.gender, student.email, enroll.course_id, CAST(0 AS BIT) AS unenroll FROM student INNER JOIN enroll ON enroll.student_id = student.student_id WHERE (enroll.course_id = @course_id)">
        <SelectParameters>
          <asp:QueryStringParameter DefaultValue="0" Name="course_id" QueryStringField="course_id" />
        </SelectParameters>
      </asp:SqlDataSource>
      <asp:Button ID="UnenrollBtn" runat="server" CssClass="btn btn-danger btn-main-date" data-action="warn" Text="Unenroll" OnClick="UnenrollBtn_Click" />
    </div>
  </form>
  <script>
    $("a.warn").attr("data-action", "warn");
  </script>
</asp:Content>
