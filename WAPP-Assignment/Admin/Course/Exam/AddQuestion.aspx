﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddQuestion.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.AddQuestion" %>

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
            <asp:Label ID="QueNoLbl" runat="server" Text="Question No."></asp:Label>
            <asp:TextBox ID="QueNoTxtBox" runat="server" TextMode="Number" Required="required" Min="1"></asp:TextBox>
            <asp:RangeValidator ID="QueNoRangeValidator" runat="server" ErrorMessage="Invalid Question Number"
                ForeColor="Red" ControlToValidate="QueNoTxtBox" Type="Integer" SetFocusOnError="True" MinimumValue="1"
                ></asp:RangeValidator>
            <br /><br />
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddBtn" runat="server" OnClick="AddBtn_Click" Text="Add" />
            <br /><br />
            <asp:TextBox ID="OptTxtBox" runat="server"></asp:TextBox>
            <asp:Button ID="AddOptBtn" runat="server" Text="Add Option" OnClick="AddOptBtn_Click" />
            <br /><br />
            <asp:RadioButtonList ID="OptRadioList" runat="server"></asp:RadioButtonList>
        </div>
    </form>
</body>
</html>
