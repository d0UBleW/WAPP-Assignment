<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WAPP_Assignment.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="UserTypeLbl" runat="server" Text="Login as"></asp:Label>
            <asp:RadioButtonList ID="UserTypeRadio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="UserTypeRadio_SelectedIndexChanged">
                <asp:ListItem Selected="True" Text="Admin" Value="admin"></asp:ListItem>
                <asp:ListItem Selected="False" Text="Student" Value="student"></asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="UsernameLbl" runat="server" Text="Username"></asp:Label>
            <br />
            <asp:TextBox ID="UsernameTxtBox" runat="server" TextMode="SingleLine" ToolTip="username for login" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="PasswordLbl" runat="server" Text="Password"></asp:Label>
            <br />
            <asp:TextBox ID="PasswordTxtBox" runat="server" TextMode="Password" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" />
            <br /><br />
            <asp:Label ID="ErrorLbl" runat="server" Text="Login credential is incorrect." ForeColor="Red" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
