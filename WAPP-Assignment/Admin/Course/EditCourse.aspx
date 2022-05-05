<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/addCourse.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:Button ID="BackBtn" runat="server" OnClientClick="return confirm('Go back?');" Text="Back" OnClick="BackBtn_Click" />
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
            <br /><br />
            <asp:Button ID="AddExBtn" runat="server" Text="Add Exam" OnClick="AddExBtn_Click" />
            <br /><br />
            <asp:Placeholder ID="ExamPlaceholder" runat="server"></asp:Placeholder>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $("[id^='delChapBtn']").on('click', function () {
                //const match = this.id.match(/delChapBtn-(\d+)/)
                //if (match != null)
                //    chap_id = match[1]
                //const chap_id = this.data('chap-id')
                const chap_id = this.dataset.chapId
                if (confirm("Are you sure?")) {
                    window.location.href = "/Admin/Course/Chapter/DeleteChapter.aspx?chapter_id=" + chap_id
                }
            })
        })
    </script>
</body>
</html>
