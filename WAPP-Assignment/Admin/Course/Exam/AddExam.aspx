<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="WAPP_Assignment.Admin.AddExam" %>

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
            <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" />
            <br /><br />
            <asp:Button ID="AddQueBtn" runat="server" Text="Add Question" />
            <br /><br />
            <asp:PlaceHolder ID="QuePlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
