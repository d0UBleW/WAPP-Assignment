<%@ Page Title="Add Chapter" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddChapter.aspx.cs" Inherits="WAPP_Assignment.Admin.AddChapter" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
    <script src="/Scripts/ckeditor.js" defer></script>
</asp:Content>
<asp:Content ID="AddChapterContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="ChapNoLbl" runat="server" Text="Chapter No."></asp:Label>
            <asp:TextBox ID="ChapNoTxtBox" runat="server" TextMode="Number" Required="required" Min="1"></asp:TextBox>
            <asp:RangeValidator ID="ChapNoRangeValidator" runat="server" ErrorMessage="Invalid Chapter Number"
                ForeColor="Red" ControlToValidate="ChapNoTxtBox" Type="Integer" SetFocusOnError="True" MinimumValue="1"
                ></asp:RangeValidator>
            <br /><br />
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddBtn" runat="server" OnClick="AddBtn_Click" Text="Add" />
        </div>
    </form>
</asp:Content>
