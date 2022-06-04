<%@ Page Title="Profile" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WAPP_Assignment.Profile" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item active" aria-current="page">Profile</li>
</asp:Content>

<asp:Content ID="ProfileContent" ContentPlaceHolderID="MainContent" runat="server">
  <form id="form1" runat="server">
    <div class="container">
      <asp:Image
        ID="ProfileImg"
        runat="server"
        Height="200"
        Width="200"
        style="object-fit: cover;"
        AlternateText="Profile Image"
        />
      <br />
      <br />
      <div class="input-group mb-3">
        <asp:FileUpload ID="ProfileUpload" runat="server" CssClass="form-control"/>
        <asp:Button
          ID="RemoveBtn"
          runat="server"
          Text="Remove Image"
          UseSubmitBehavior="false"
          CausesValidation="false"
          OnClick="RemoveBtn_Click"
          CssClass="btn btn-outline-secondary"
          />
      </div>
      <div class="form-floating mb-3 mt-3">
        <asp:TextBox
          ID="FullNameTxtBox"
          runat="server"
          TextMode="SingleLine"
          ToolTip="Full Name"
          Required="required"
          CssClass="form-control"
          Placeholder="Full Name"
          MaxLength="50"
          data-max-len="true"
          ></asp:TextBox>
        <label for="<%= FullNameTxtBox.ClientID %>" class="text-muted">Full Name</label>
      </div>
      <div class="form-floating mb-3">
        <asp:TextBox
          ID="EmailTxtBox"
          runat="server"
          TextMode="Email"
          CssClass="form-control"
          ToolTip="Email"
          Placeholder="Email"
          Required="required"
          MaxLength="100"
          data-max-len="true"
          ></asp:TextBox>
        <label for="<%= EmailTxtBox.ClientID %>" class="text-muted">Email</label>
      </div>
      <div class="form-floating mb-3">
        <asp:DropDownList ID="GenderList" runat="server" Required="required" CssClass="form-select">
          <asp:ListItem Selected="True" Text="Please select a gender" Value=""></asp:ListItem>
          <asp:ListItem Selected="False" Text="Male" Value="m"></asp:ListItem>
          <asp:ListItem Selected="False" Text="Female" Value="f"></asp:ListItem>
        </asp:DropDownList>
        <label for="<%= GenderList.ClientID %>" class="text-muted"">Gender</label>
      </div>
      <asp:Button ID="EditBtn" runat="server" Text="Edit Profile" OnClick="EditBtn_Click" CssClass="btn btn-outline-primary mb-3" />
      <br />
      <asp:Panel ID="UploadStatusPanel" runat="server" Visible="false" CssClass="mb-3">
        <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
        <br />
      </asp:Panel>
      <div class="btn-group" role="group">
        <asp:HyperLink ID="ChangePasswdLink" runat="server" NavigateUrl="/Student/ChangePassword.aspx"
          Text="Change Password" CssClass="btn btn-outline-primary btn-sm"></asp:HyperLink>
        <asp:HyperLink ID="DeleteAccLink" runat="server" NavigateUrl="/Student/DeleteAccount.aspx"
          Text="Delete Account" CssClass="btn btn-outline-danger btn-sm" data-action="warn"></asp:HyperLink>
      </div>
    </div>
  </form>
  <script>
    $("a:not(a[id$='DeleteAccLink']").on('click', function () {
      return confirm('Discard current working progress?');
    })
  </script>
</asp:Content>
