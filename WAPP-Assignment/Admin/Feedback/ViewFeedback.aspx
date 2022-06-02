<%@ Page Title="" Language="C#" MasterPageFile="~/SiteAdmin.master" AutoEventWireup="true" CodeBehind="ViewFeedback.aspx.cs" Inherits="WAPP_Assignment.Admin.Feedback.ViewFeedback" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/Admin/Feedback/FeedbackList.aspx">Feedback</a></li>
  <li class="breadcrumb-item active" aria-current="page">View Feedback</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <h2 class="border-bottom mb-3">Subject:
        <asp:Literal ID="SubjectLit" runat="server"></asp:Literal></h2>
      <div id="ProfileDiv" class="d-flex flex-row justify-content-start mb-3" style="height: 50px !important;">
        <asp:Image ID="ProfileImg" CssClass="img-fluid me-3" runat="server" AlternateText="Profile Image" />
        <asp:Label ID="UsernameLbl" CssClass="align-self-center" runat="server"></asp:Label>
      </div>
      <div id="ContentDiv">
        <div class="form-floating mb-3">
          <asp:TextBox
            ID="ContentTxtBox"
            ToolTip="Feedback content"
            CssClass="form-control"
            runat="server"
            Enabled="false"></asp:TextBox>
          <label for="<%=ContentTxtBox.ClientID %>" class="text-muted">Content</label>
        </div>
      </div>
      <asp:HyperLink ID="DeleteLink" runat="server" CssClass="btn btn-outline-danger" Text="Delete" data-action="warn"></asp:HyperLink>
    </div>
  </form>
</asp:Content>
