<%@ Page Title="Profile" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WAPP_Assignment.Profile" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Profile</li>
</asp:Content>

<asp:Content ID="ProfileContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Image ID="ProfileImg" runat="server" Height="200" Width="200" />
      <br />
      <br />
      <asp:FileUpload ID="ProfileUpload" runat="server" />
      <br />
      <br />
      <asp:Button ID="RemoveBtn" runat="server" Text="Remove Image" UseSubmitBehavior="false" CausesValidation="false" OnClick="RemoveBtn_Click" />
      <br />
      <br />
      <asp:Label ID="FullNameLbl" runat="server" Text="Full Name"></asp:Label>
      <asp:TextBox ID="FullNameTxtBox" runat="server" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="EmailLbl" runat="server" Text="Email"></asp:Label>
      <asp:TextBox ID="EmailTxtBox" runat="server" TextMode="Email" Required="required"></asp:TextBox>
      <br />
      <br />
      <asp:Label ID="GenderLbl" runat="server" Text="Gender"></asp:Label>
      <asp:DropDownList ID="GenderList" runat="server" Required="required">
        <asp:ListItem Selected="True" Text="Please select a gender" Value=""></asp:ListItem>
        <asp:ListItem Selected="False" Text="Male" Value="m"></asp:ListItem>
        <asp:ListItem Selected="False" Text="Female" Value="f"></asp:ListItem>
      </asp:DropDownList>
      <br />
      <br />
      <asp:Button ID="EditBtn" runat="server" Text="Edit Profile" OnClick="EditBtn_Click" />
      <asp:Panel ID="UploadStatusPanel" runat="server" Visible="false">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
        <br />
      </asp:Panel>
      <br />
      <br />
      <asp:HyperLink ID="ChangePasswdLink" runat="server" NavigateUrl="/Student/ChangePassword.aspx"
        Text="Change Password" CssClass="btn btn-secondary btn-sm"></asp:HyperLink>
      <asp:HyperLink ID="DeleteAccLink" runat="server" NavigateUrl="/Student/DeleteAccount.aspx"
        Text="Delete Account" CssClass="btn btn-outline-danger btn-sm"></asp:HyperLink>
    </div>
  </form>
  <script>
    $("a[id$='DeleteAccLink']").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })

    $("a:not(a[id$='DeleteAccLink']").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
