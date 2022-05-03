<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCourse.aspx.cs" Inherits="WAPP_Assignment.ListCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="AddCourseBtn" runat="server" Text="Add Course" OnClick="AddCourseBtn_Click" />
            <br /><br />
            <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
