<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="FeedbackList.aspx.cs" Inherits="WAPP_Assignment.Admin.FeedbackList" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Feedback</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <h1 class="border-bottom mb-3">Feedback</h1>
      <div class="table-responsive">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="feed_id" DataSourceID="FeedbackDataSource" CssClass="table table-hover table-striped">
      <Columns>
        <asp:BoundField DataField="feed_id" HeaderText="feed_id" InsertVisible="False" ReadOnly="True" SortExpression="feed_id" HeaderStyle-CssClass="w-10" />
        <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" HeaderStyle-CssClass="w-25" />
        <asp:BoundField DataField="subject" HeaderText="subject" SortExpression="subject" HeaderStyle-CssClass="" ItemStyle-CssClass="text-break" />
        <asp:HyperLinkField DataNavigateUrlFields="feed_id" DataNavigateUrlFormatString="/Admin/Feedback/ViewFeedback.aspx?feed_id={0}" HeaderText="Action" Text="View" ItemStyle-CssClass="btn btn-primary btn-sm" />
      </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="FeedbackDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:iLearnDBConStr %>" SelectCommand="SELECT feed.feed_id, student.username, feed.subject FROM feed INNER JOIN student ON feed.student_id = student.student_id"></asp:SqlDataSource>
      </div>
    <input type="hidden" id="NavLocation" value="dashboard" disabled="disabled" />
    </div>
  </form>
</asp:Content>
