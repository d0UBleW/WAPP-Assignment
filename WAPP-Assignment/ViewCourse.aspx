<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCourse.aspx.cs" Inherits="WAPP_Assignment.ViewCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="CourseDetailPanel" runat="server">
            <asp:Panel ID="ImagePanel" runat="server">
                <asp:Image ID="ThumbnailImage" runat="server" Width="200" Height="200" />
            </asp:Panel>
            <h1><asp:Label ID="TitleLbl" runat="server"></asp:Label></h1>
            <asp:Label ID="DescriptionLbl" runat="server"></asp:Label>
            <br />
            <asp:Button ID="EnrollBtn" runat="server" Visible="false" OnClick="EnrollBtn_Click" Text="Enroll" />
            <asp:Button ID="LearnBtn" runat="server" Visible="false" OnClick="LearnBtn_Click" Text="Learn" />
        </asp:Panel>
    </form>
    <asp:Panel ID="ChapterTOCPanel" runat="server"></asp:Panel>
    <asp:Panel ID="RatingPanel" runat="server"></asp:Panel>
</body>
</html>
