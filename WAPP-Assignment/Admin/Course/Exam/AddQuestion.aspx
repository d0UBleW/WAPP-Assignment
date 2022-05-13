<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/SiteAdmin.Master" AutoEventWireup="true" CodeBehind="AddQuestion.aspx.cs" Inherits="WAPP_Assignment.Admin.Course.Exam.AddQuestion" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="SubHead" runat="server">
    <script src="https://cdn.ckeditor.com/4.18.0/full/ckeditor.js"></script>
    <script src="/Scripts/ckeditor.js" defer></script>
    <script src="/Scripts/addOption.js" defer></script>
</asp:Content>

<asp:Content ID="AddQuestionContent" ContentPlaceHolderID="SubMainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="QueNoLbl" runat="server" Text="Question No."></asp:Label>
            <asp:TextBox ID="QueNoTxtBox" runat="server" TextMode="Number" Required="required" Min="1" Max="5"></asp:TextBox>
            <asp:RangeValidator ID="QueNoRangeValidator" runat="server" ErrorMessage="Invalid Question Number"
                ForeColor="Red" ControlToValidate="QueNoTxtBox" Type="Integer" SetFocusOnError="True" MinimumValue="1" MaximumValue="5"
                ></asp:RangeValidator>
            <br /><br />
            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleTxtBox" runat="server" TextMode="SingleLine" Required="required"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ContentLbl" runat="server" Text="Content"></asp:Label>
            <br />
            <asp:TextBox ID="EditorTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="AddBtn" runat="server" OnClick="AddBtn_Click" OnClientClick="return CheckOption();" Text="Add" />
            <br /><br />
            <input type="text" id="OptTxtBox" />
            <br /><br />
            <button type="button" id="AddOptBtn">Add Option</button>
            <br /><br />
            <asp:Label ID="OptStatus" runat="server" ForeColor="Red" style="display: none;" Text="Please choose at least one option as answer."></asp:Label>
            <br /><br />
            <h4>Option</h4>
            <table id="OptTable">
                <tbody>
                </tbody>
            </table>
            <br /><br />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </div>
    </form>
</asp:Content>
