<%@ Page Title="Edit Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditExam.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.EditExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="EditExamContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:Panel ID="MainPanel" CssClass="container" runat="server">
      <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
      <asp:TextBox ID="TitleTxtBox" runat="server"></asp:TextBox>
      <br />
      <br />
      <asp:LinkButton ID="AddQueBtnLink" runat="server" Text="Add Question" CssClass="btn btn-secondary btn-md" OnClick="AddQueBtnLink_Click"></asp:LinkButton>
      <br />
      <br />
      <asp:Panel ID="QuePanel" runat="server">
        <asp:PlaceHolder ID="QuePlaceholder" runat="server"></asp:PlaceHolder>
      </asp:Panel>
    </asp:Panel>
  </form>
</asp:Content>
