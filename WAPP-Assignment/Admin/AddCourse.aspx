<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.AddCourse" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/addCourse.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Course Title"></asp:Label>
            <br />
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="DescLbl" runat="server" Text="Course Description"></asp:Label>
            <br />
            <asp:TextBox ID="DescTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:ListBox ID="CatList" runat="server" Height="200px" Width="200px" SelectionMode="Multiple"></asp:ListBox>
            <br />
            <asp:TextBox ID="CatTxtBox" runat="server"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="Auto1" runat="server" ServiceMethod="SearchCategory"
                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
                CompletionSetCount="10" TargetControlID="CatTxtBox" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
            <button id="CatAddBtn" type="button">Add Category</button>
            <button id="CatDelBtn" type="button">Remove Category</button>
            <asp:HiddenField ID="CatField" runat="server" />
            <br /><br />
            <asp:Label ID="ThumbnailLbl" runat="server" Text="Thumbnail"></asp:Label>
            <br />
            <asp:FileUpload ID="ThumbnailUpload" runat="server" />
            <br />
            <asp:Panel ID="UploadStatusPanel" runat="server">
                <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Button ID="AddBtn" runat="server" Text="Add" OnClick="AddBtn_Click" />
        </div>
    </form>
</body>
</html>
