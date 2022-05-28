<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WAPP_Assignment.Login" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/togglePassword.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Login</li>
</asp:Content>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Label ID="UserTypeLbl" runat="server" Text="Login as"></asp:Label>
      <asp:RadioButtonList ID="UserTypeRadio" runat="server">
        <asp:ListItem Selected="True" Text="Admin" Value="admin"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Student" Value="student"></asp:ListItem>
      </asp:RadioButtonList>
      <br />
      <div class="form-floating mb-3">
        <asp:TextBox ID="UsernameTxtBox"
          runat="server"
          TextMode="SingleLine"
          ToolTip="Username"
          Required="required" CssClass="form-control"
          Placeholder="Username"
          ></asp:TextBox>
        <label for="<%= UsernameTxtBox.ClientID %>">Username</label>
      </div>
      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1">
          <asp:TextBox ID="PasswordTxtBox"
            runat="server"
            TextMode="Password"
            ToolTip="Password"
            Required="required"
            CssClass="form-control"
            Placeholder="Password"
            data-toggle="password"
            ></asp:TextBox>
          <label for="<%= PasswordTxtBox.ClientID %>">Password</label>
        </div>
        <span class="input-group-text" data-toggle="passwordToggler" style="cursor: pointer;">
          <i class="bi bi-eye-slash" id="togglePassword"></i>
        </span>
      </div>
      <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" CssClass="btn btn-primary btn-md btn-block" />
      <br />
      <br />
      <asp:Label ID="ErrorLbl" runat="server" Text="Login credential is incorrect." ForeColor="Red" Visible="false"></asp:Label>
    </div>
  </form>
  <input type="hidden" id="NavLocation" value="login" disabled="disabled" />
  <script>
  </script>
</asp:Content>
