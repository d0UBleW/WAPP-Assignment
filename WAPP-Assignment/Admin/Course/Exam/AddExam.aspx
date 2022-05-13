<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="WAPP_Assignment.Admin.AddExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server"></asp:Content>

<asp:Content ID="AddExamContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" />
        </div>
    </form>
</asp:Content>
