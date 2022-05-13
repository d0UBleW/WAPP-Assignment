<%@ Page Title="My Courses" Language="C#" MasterPageFile="~/SiteStudent.Master" AutoEventWireup="true" CodeBehind="MyCourse.aspx.cs" Inherits="WAPP_Assignment.MyCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server">

</asp:Content>

<asp:Content ID="CourseContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <asp:Panel ID="CoursePanel" runat="server">
        <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</asp:Content>
