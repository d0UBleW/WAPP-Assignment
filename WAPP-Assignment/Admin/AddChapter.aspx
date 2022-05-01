<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.AddChapter" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script src="//cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>--%>
    <script src="/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/addChapter.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:HiddenField ID="dataField" runat="server" Value=""/>
            <br /><br />
            <asp:Button ID="AddBtnASP" runat="server" OnClick="AddBtnASP_Click" Text="Add" />
        </div>
        <asp:Panel ID="TestPanel" runat="server"></asp:Panel>
    </form>
</body>
</html>
