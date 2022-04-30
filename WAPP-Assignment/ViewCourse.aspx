<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCourse.aspx.cs" Inherits="WAPP_Assignment.ViewCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True"></asp:RadioButtonList>
        </div>
    </form>
</body>
</html>
