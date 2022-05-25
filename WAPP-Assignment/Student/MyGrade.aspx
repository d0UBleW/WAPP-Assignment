<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="MyGrade.aspx.cs" Inherits="WAPP_Assignment.Student.MyGrade" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">My Grades</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:GridView ID="GradeView" runat="server" AutoGenerateColumns="False" DataSourceID="MyGradeDataSource" DataKeyNames="exam_id">
        <Columns>
          <asp:BoundField DataField="exam_id" HeaderText="exam_id" SortExpression="exam_id" InsertVisible="False" ReadOnly="True" Visible="False" />
          <asp:BoundField DataField="title" HeaderText="Course" SortExpression="title" />
          <asp:BoundField DataField="title1" HeaderText="Exam" SortExpression="title1" />
          <asp:BoundField DataField="value" HeaderText="value" SortExpression="value" />
          <asp:BoundField DataField="Column1" HeaderText="total" ReadOnly="True" SortExpression="Column1" />
          <asp:HyperLinkField DataNavigateUrlFields="exam_id" DataNavigateUrlFormatString="/Student/Learn/ReviewExam.aspx?exam_id={0}" HeaderText="Review" Text="Review" />
        </Columns>
      </asp:GridView>
      <asp:SqlDataSource ID="MyGradeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT exam.exam_id, course.title, exam.title, grade.value, (SELECT SUM(weight) FROM question WHERE question.exam_id=exam.exam_id) FROM [grade] INNER JOIN [exam] ON grade.exam_id = exam.exam_id INNER JOIN [course] ON exam.course_id=course.course_id WHERE grade.student_id=@student_id">
        <SelectParameters>
          <asp:SessionParameter Name="student_id" SessionField="user_id" DefaultValue="0" />
        </SelectParameters>
      </asp:SqlDataSource>
    </div>
  </form>
  <input type="hidden" id="NavLocation" value="grade" disabled="disabled" />
  <script>
    $("[id$='GradeView']").find("td.review-exam a").addClass("btn btn-primary btn-sm")
  </script>
</asp:Content>
