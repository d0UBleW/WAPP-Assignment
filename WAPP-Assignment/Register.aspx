<%@ Page Title="Register" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WAPP_Assignment.Register" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/register.js" defer></script>
  <script src="/Scripts/togglePassword.js" defer></script>
  <script src="/Scripts/strengthMeter.js" defer></script>
  <style>
    .BarIndicatorPoor {
      color:red;
      background-color:red;
    }
    .BarIndicatorWeak {
      color:orange;
      background-color:orange;
    }
    .BarIndicatorAverage {
      color:yellow;
      background-color:yellow;
    }
    .BarIndicatorStrong {
      color:greenyellow;
      background-color:greenyellow;
    }
    .BarIndicatorExcellent {
      color:lawngreen;
      background-color: lawngreen;
    }

    .BarBorder {
      border-style:solid;
      border-color:Gray;
      border-width: 1px;
      width: 150px;
      padding: 2px;
    }
  </style>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Register</li>
</asp:Content>

<asp:Content ID="RegisterContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
      <asp:Label ID="UserTypeLbl" runat="server" Text="Register as"></asp:Label>
      <asp:RadioButtonList ID="UserTypeRadio" runat="server">
        <asp:ListItem Selected="True" Text="Admin" Value="admin"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Student" Value="student"></asp:ListItem>
      </asp:RadioButtonList>
      <br />
      <div class="form-floating mb-3">
        <asp:TextBox ID="UsernameTxtBox"
          runat="server"
          CssClass="form-control"
          TextMode="SingleLine"
          ToolTip="Username"
          Placeholder="Username"
          Required="required"></asp:TextBox>
        <label for="<%= UsernameTxtBox.ClientID %>">Username</label>
      </div>

      <div id="username_feedback" class="mb-3"></div>

      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1">
          <asp:TextBox ID="PasswordTxtBox"
            runat="server"
            CssClass="form-control"
            TextMode="Password"
            ToolTip="Password"
            Placeholder="Password"
            data-toggle="password"
            data-strength-meter=""
            Required="required"></asp:TextBox>
          <label for="<%= PasswordTxtBox.ClientID %>">Password</label>
        </div>
        <span class="input-group-text" data-toggle="passwordToggler" style="cursor: pointer;">
          <i class="bi bi-eye-slash"></i>
        </span>
      </div>
      <div class="progress mb-3">
        <div class="progress-bar" role="progressbar" style="width: 0%;" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">Strong</div>
      </div>
      <div class="mb-3">
        <span class="text-muted" id="helpLbl"></span>
      </div>

      <div id="student-div">
        <asp:Label ID="FullNameLbl" runat="server" Text="Full Name"></asp:Label>
        <br />
        <asp:TextBox ID="FullNameTxtBox" runat="server" TextMode="SingleLine" Enabled="false"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="EmailLbl" runat="server" Text="Email"></asp:Label>
        <br />
        <asp:TextBox ID="EmailTxtBox" runat="server" TextMode="Email" Enabled="false"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="GenderLbl" runat="server" Text="Gender"></asp:Label>
        <br />
        <asp:DropDownList ID="GenderDropDownList" runat="server" Enabled="false">
          <asp:ListItem Selected="True" Text="Please select a gender" Value=""></asp:ListItem>
          <asp:ListItem Selected="False" Text="Male" Value="m"></asp:ListItem>
          <asp:ListItem Selected="False" Text="Female" Value="f"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
      </div>
      <asp:Button ID="RegisterBtn" runat="server" Text="Register" OnClick="RegisterBtn_Click" CssClass="btn btn-md btn-primary btn-block" />
    </div>
  </form>
  <input type="hidden" id="NavLocation" value="register" disabled="disabled" />
  <script>
    $("[id$='RegisterBtn']").on('click', function () {
      const passwd = $passwordTxtBox.val()
      const valid = isValidPassword(passwd)
      if (!valid) {
        $passwordTxtBox.addClass("is-invalid")
      }
      return valid
    })

  </script>
</asp:Content>
