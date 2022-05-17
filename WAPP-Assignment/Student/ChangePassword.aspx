﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WAPP_Assignment.Student.Course.ChangePassword" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Label ID="CurrPasswdLbl" runat="server" Text="Current Password"></asp:Label>
      <asp:TextBox ID="CurrPasswdTxtBox" runat="server" CssClass="form-control" TextMode="Password" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="NewPasswdLbl" runat="server" Text="New Password"></asp:Label>
      <asp:TextBox ID="NewPasswdTxtBox" runat="server" CssClass="form-control" TextMode="Password" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="RetypePasswdLbl" runat="server" Text="Retype New Password"></asp:Label>
      <asp:TextBox ID="RetypePasswdTxtBox" runat="server" CssClass="form-control" TextMode="Password" Required="required"></asp:TextBox>
      <br />
      <div id="password_feedback" class="invalid-feedback" style="display: none;">New and retyped password do not match</div>
      <br />
      <br />
      <asp:Button ID="SubmitBtn" runat="server" Text="Change password" OnClick="SubmitBtn_Click" />
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

    $("input[id$='NewPasswdTxtBox']").on('change', warnTxtBox)
    $("input[id$='RetypePasswdTxtBox']").on('change', warnTxtBox)

    $("[id$='SubmitBtn']").on('click', function () {
      return checkNewPassMatch
    })

  </script>
</asp:Content>
