<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Label ID="DescLbl" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="DescTxtBox" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddChapBtn" runat="server" Text="Add Chapter" OnClick="AddChapBtn_Click" />
            <br /><br />
            <asp:Placeholder ID="ChapterPlaceholder" runat="server"></asp:Placeholder>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"></asp:RadioButtonList>
        </div>
    </form>
</body>
</html>
