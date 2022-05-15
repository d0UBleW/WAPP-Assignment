<%@ Page Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ViewCourse.aspx.cs" Inherits="WAPP_Assignment.ViewCourse" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="CourseDetailPanel" CssClass="container" runat="server">
      <asp:Panel ID="ImagePanel" runat="server">
        <asp:Image ID="ThumbnailImage" runat="server" Width="200" Height="200" />
      </asp:Panel>
      <h1>
        <asp:Label ID="TitleLbl" runat="server"></asp:Label></h1>
      <asp:Label ID="DescriptionLbl" runat="server"></asp:Label>
      <br />
      <asp:HyperLink ID="EnrollLink" runat="server" Text="Enroll" OnClick="EnrollLink_Click"></asp:HyperLink>
      <asp:HyperLink ID="UnenrollLink" runat="server" Text="Unenroll" OnClick="UnenrollLink_Click"></asp:HyperLink>
    </asp:Panel>
  <div class="container">
    <asp:Panel ID="ChapterTOCPanel" runat="server"></asp:Panel>
    <asp:Panel ID="ExamPanel" runat="server"></asp:Panel>
    <asp:Panel ID="RatingPanel" runat="server">
      <form id="form1" runat="server">
      </form>
    </asp:Panel>
  </div>
  <input type="hidden" id="NavLocation" value="Course" disabled="disabled" />
</asp:Content>
