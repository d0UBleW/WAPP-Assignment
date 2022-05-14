<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WAPP_Assignment.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <asp:Label ID="UserTypeLbl" runat="server" Text="Login as"></asp:Label>
            <asp:RadioButtonList ID="UserTypeRadio" runat="server">
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
            <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" CssClass="btn btn-primary btn-md btn-block" />
            <br /><br />
            <asp:Label ID="ErrorLbl" runat="server" Text="Login credential is incorrect." ForeColor="Red" Visible="false"></asp:Label>
        </div>
    </form>
</asp:Content>
