<%@ Page Title="Edit Exam" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditExam.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.EditExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server">

</asp:Content>

<asp:Content ID="EditExamContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddQueBtn" runat="server" Text="Add Question" OnClick="AddQueBtn_Click" />
            <br /><br />
            <asp:PlaceHolder ID="QuePlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</asp:Content>
