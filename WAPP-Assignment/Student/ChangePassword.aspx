<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WAPP_Assignment.Student.Course.ChangePassword" ValidateRequest="false" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/Scripts/togglePassword.js" defer></script>
  <script src="/Scripts/strengthMeter.js" defer></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/Student/Profile.aspx">Profile</a></li>
  <li class="breadcrumb-item active" aria-current="page">Change Password</li>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1" id="CurrPasswd" runat="server">
          <asp:TextBox ID="CurrPasswdTxtBox"
            runat="server"
            CssClass="form-control"
            TextMode="Password"
            ToolTip="Current Password"
            Placeholder="Password"
            data-toggle="password"
            MaxLength="50"
            Required="required"></asp:TextBox>
          <label for="<%= CurrPasswdTxtBox.ClientID %>" class="text-muted" runat="server" id="CurrPasswdLabel">Current Password</label>
        </div>
        <span class="input-group-text" data-toggle="passwordToggler" style="cursor: pointer;" id="CurrToggler" runat="server">
          <i class="bi bi-eye-slash"></i>
        </span>
      </div>

      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1">
          <asp:TextBox ID="NewPasswdTxtBox"
            runat="server"
            CssClass="form-control"
            TextMode="Password"
            ToolTip="New Password"
            Placeholder="Password"
            data-toggle="password"
            data-strength-meter=""
            MaxLength="50"
            Required="required"></asp:TextBox>
          <label for="<%= NewPasswdTxtBox.ClientID %>" class="text-muted">New Password</label>
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

      <div class="input-group mb-3">
        <div class="form-floating flex-grow-1">
          <asp:TextBox ID="RetypePasswdTxtBox"
            runat="server"
            CssClass="form-control"
            TextMode="Password"
            ToolTip="Retype Password"
            Placeholder="Password"
            data-toggle="password"
            MaxLength="50"
            Required="required"></asp:TextBox>
          <label for="<%= RetypePasswdTxtBox.ClientID %>" class="text-muted">Retype Password</label>
        </div>
        <span class="input-group-text" data-toggle="passwordToggler" style="cursor: pointer;">
          <i class="bi bi-eye-slash"></i>
        </span>
      </div>

      <div id="password_feedback" class="invalid-feedback" style="display: none;">New and retyped password do not match</div>
      <br />
      <asp:Button ID="SubmitBtn" runat="server" Text="Change password" OnClick="SubmitBtn_Click" CssClass="btn btn-outline-primary" />
      <br />
      <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    </div>
  </form>
  <script>
    const checkNewPassMatch = () => {
      const newPass = $("input[id$='NewPasswdTxtBox']").val()
      const retypePass = $("input[id$='RetypePasswdTxtBox']").val()
      return newPass === retypePass
    }

    const warnTxtBox = () => {
      const $newTxtBox = $("input[id$='NewPasswdTxtBox']")
      const $retypeTxtBox = $("input[id$='RetypePasswdTxtBox']")
      $newTxtBox.removeClass("is-invalid")
      $retypeTxtBox.removeClass("is-invalid")
      $("#password_feedback").hide()
      if (!checkNewPassMatch()) {
        $newTxtBox.addClass("is-invalid")
        $retypeTxtBox.addClass("is-invalid")
        $("#password_feedback").show()
      }
    }

    $("input[id$='NewPasswdTxtBox']").on('keyup', warnTxtBox)
    $("input[id$='RetypePasswdTxtBox']").on('keyup', warnTxtBox)

    $("[id$='SubmitBtn']").on('click', function () {
      return checkNewPassMatch
    })

    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
