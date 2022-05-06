<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.EditChapter" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
    <script src="/Scripts/ckeditor.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="BackBtn" runat="server" OnClientClick="return confirm('Go back?');" Text="Back" OnClick="BackBtn_Click" CausesValidation="False" />
            <br /><br />
            <asp:Label ID="ChapNoLbl" runat="server" Text="Chapter No."></asp:Label>
            <asp:TextBox ID="ChapNoTxtBox" runat="server" TextMode="Number" Min="1"></asp:TextBox>
            <asp:RangeValidator ID="ChapNoRangeValidator" runat="server" ErrorMessage="Invalid Chapter Number"
                ForeColor="Red" ControlToValidate="ChapNoTxtBox" Type="Integer" SetFocusOnError="True" MinimumValue="1"
                ></asp:RangeValidator>
            <br />
            <asp:RequiredFieldValidator ID="ChapNoRequired" runat="server" ErrorMessage="Chapter No is required" ForeColor="Red" ControlToValidate="ChapNoTxtBox" ValidationGroup="EditChap"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ErrorMessage="Title is required" ForeColor="Red" ControlToValidate="TitleTxtBox" ValidationGroup="EditChap"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit" ValidationGroup="EditChap" />
            <asp:HiddenField ID="CourseIDField" runat="server" />
        </div>
    </form>
</body>
</html>
