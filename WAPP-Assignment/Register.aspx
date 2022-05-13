<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WAPP_Assignment.Register" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/register.js" defer></script>
</asp:Content>
<asp:Content ID="RegisterContent" ContentPlaceHolderID="MainContent" runat="server">
    <input id="RegNavHidden" name="RegNavHidden" type="hidden" value="Register" disabled="disabled" />
    <form id="form1" runat="server">
        <div class="container">
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
            <asp:Button ID="RegisterBtn" runat="server" Text="Register" OnClick="RegisterBtn_Click" CssClass="btn btn-md btn-primary btn-block" />
        </div>
    </form>
</asp:Content>
