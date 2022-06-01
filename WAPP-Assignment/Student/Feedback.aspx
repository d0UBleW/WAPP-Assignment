<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="WAPP_Assignment.Student.Feedback" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Feedback</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <div class="form-floating mb-5">
        <asp:TextBox
          ID="SubjectTxtBox"
          runat="server"
          TextMode="SingleLine"
          Placeholder="Feedback Subject"
          ToolTip="Feedback Subject"
          CssClass="form-control"
          Required="required"
          maxlength="200"
          data-max-len="true"
          ></asp:TextBox>
        <label for="<%=SubjectTxtBox.ClientID %>" class="text-muted">Subject</label>
      </div>
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="ContentTxtBox"
          runat="server"
          TextMode="MultiLine"
          Placeholder="Feedback"
          ToolTip="Feedback"
          CssClass="form-control"
          Required="required"
          Height="250"
          maxlength="1000"
          data-max-len="true"
          ></asp:TextBox>
        <label for="<%=ContentTxtBox.ClientID %>" class="text-muted">Content</label>
      </div>
      <asp:Button ID="SubmitBtn" runat="server" Text="Submit" CssClass="btn btn-outline-primary btn-md" OnClick="SubmitBtn_Click" />
      <asp:Panel ID="StatusPanel" runat="server" CssClass="alert alert-success mt-3" role="alert" Visible="false">
        Thank you for the feedback
      </asp:Panel>
    </div>
  </form>
</asp:Content>
