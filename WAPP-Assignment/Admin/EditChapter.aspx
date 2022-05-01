﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.EditChapter" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/addChapter.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit" />
        </div>
    </form>
</body>
</html>