<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/addCourse.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <button type="button" id="BackBtn">Back</button>
            <br /><br />
            <asp:Image ID="ThumbnailImg" runat="server" Height="200" Width="200" />
            <br />
            <asp:FileUpload ID="ThumbnailUpload" runat="server" />
            <br /><br />
            <asp:Button ID="RemoveBtn" runat="server" OnClick="RemoveBtn_Click" Text="Remove image" UseSubmitBehavior="False" />
            <br /><br />
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="DescLbl" runat="server" Text="Description"></asp:Label>
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
            <asp:Button ID="EditBtn" runat="server" Text="Edit Course" OnClick="EditBtn_Click" />
            <br />
            <asp:Panel ID="UploadStatusPanel" runat="server">
                <asp:Label ID="UploadStatusLbl" runat="server" Text=""></asp:Label>
                <br />
            </asp:Panel>
            <br />
            <asp:Button ID="AddChapBtn" runat="server" Text="Add Chapter" OnClick="AddChapBtn_Click" />
            <br /><br />
            <asp:Placeholder ID="ChapterPlaceholder" runat="server"></asp:Placeholder>
        </div>
    </form>
    <script>
        $("#BackBtn").on('click', function () {
            if (confirm("Go back?")) {
                window.location.href = "/ViewCourse.aspx"
            }
        })
    </script>
</body>
</html>
