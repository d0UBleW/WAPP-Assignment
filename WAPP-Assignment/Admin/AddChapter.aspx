<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.AddChapter" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Chapter</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddBtn" runat="server" OnClick="AddBtn_Click" Text="Add" />
        </div>
    </form>
    <script>
        CKEDITOR.replace('EditorTxtBox', {
            allowedContent: true,
            embed_provider: '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}',
        })
    </script>
</body>
</html>
