<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WAPP_Assignment.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="ProfileImg" runat="server" Height="200" Width="200" />
            <br /><br />
            <asp:FileUpload ID="ProfileUpload" runat="server" />
            <br /><br />
            <asp:Button ID="RemoveBtn" runat="server" Text="Remove Image" UseSubmitBehavior="false" CausesValidation="false" />
            <br /><br />
            <asp:Label ID="FullNameLbl" runat="server" Text="Full Name"></asp:Label>
            <asp:TextBox ID="FullNameTxtBox" runat="server" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="EmailLbl" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="EmailTxtBox" runat="server" TextMode="Email" Required="required"></asp:TextBox>
        </div>
    </form>
</body>
</html>
