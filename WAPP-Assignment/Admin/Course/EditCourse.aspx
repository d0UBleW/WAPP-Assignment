<%@ Page Title="Edit Course" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="WAPP_Assignment.Admin.EditCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server">
    <script src="/Scripts/addCourse.js" defer></script>
</asp:Content>

<asp:Content ID="EditCourseContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:LinkButton ID="BackLinkButton" runat="server"
                OnClick="BackLinkButton_Click" OnClientClick="return confirm('Go back?');"
                Text="Back" CssClass="btn btn-secondary btn-sm"></asp:LinkButton>
            <br /><br />
            <asp:Image ID="ThumbnailImg" runat="server" Height="200" Width="200" />
            <br />
            <asp:FileUpload ID="ThumbnailUpload" runat="server" />
            <br /><br />
            <asp:Button ID="RemoveBtn" runat="server" OnClick="RemoveBtn_Click" Text="Remove image" UseSubmitBehavior="false" CausesValidation="false" />
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
</asp:Content>
