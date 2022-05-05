﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WAPP_Assignment.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/register.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="UserTypeLbl" runat="server" Text="Register as"></asp:Label>
            <asp:RadioButtonList ID="UserTypeRadio" runat="server">
                <asp:ListItem Selected="True" Text="Admin" Value="admin"></asp:ListItem>
                <asp:ListItem Selected="False" Text="Student" Value="student"></asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="UsernameLbl" runat="server" Text="Username"></asp:Label>
            <br />
            <asp:TextBox ID="UsernameTxtBox" runat="server" TextMode="SingleLine" ToolTip="username for login" Required="required" AutoPostBack="True" OnTextChanged="UsernameTxtBox_TextChanged"></asp:TextBox>
            <br />
            <asp:Panel ID="UsernameValidPanel" runat="server">
                <asp:Label ID="UsernameValidLbl" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Label ID="PasswordLbl" runat="server" Text="Password"></asp:Label>
            <br />
            <asp:TextBox ID="PasswordTxtBox" runat="server" TextMode="Password" Required="required"></asp:TextBox>
            <br /><br />
            <div id="student-div">
                <asp:Label ID="FullNameLbl" runat="server" Text="Full Name"></asp:Label>
                <br />
                <asp:TextBox ID="FullNameTxtBox" runat="server" TextMode="SingleLine" Enabled="false"></asp:TextBox>
                <br /><br />
                <asp:Label ID="EmailLbl" runat="server" Text="Email"></asp:Label>
                <br />
                <asp:TextBox ID="EmailTxtBox" runat="server" TextMode="Email" Enabled="false"></asp:TextBox>
                <br /><br />
                <asp:Label ID="GenderLbl" runat="server" Text="Gender"></asp:Label>
                <br />
                <asp:DropDownList ID="GenderDropDownList" runat="server" Enabled="false">
                    <asp:ListItem Selected="True" Text="Please select a gender" Value=""></asp:ListItem>
                    <asp:ListItem Selected="False" Text="Male" Value="m"></asp:ListItem>
                    <asp:ListItem Selected="False" Text="Female" Value="f"></asp:ListItem>
                </asp:DropDownList>
                <br /><br />
            </div>
            <asp:Button ID="RegisterBtn" runat="server" Text="Register" OnClick="RegisterBtn_Click" CssClass="btn btn-lg btn-primary btn-block" />
        </div>
    </form>
</body>
</html>
