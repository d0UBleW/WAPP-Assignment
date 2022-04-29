<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.AddCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="TitleLbl" runat="server" Text="Course Title"></asp:Label>
            <br />
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="DescLbl" runat="server" Text="Course Description"></asp:Label>
            <br />
            <asp:TextBox ID="DescTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
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
