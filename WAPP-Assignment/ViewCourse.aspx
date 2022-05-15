<%@ Page Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ViewCourse.aspx.cs" Inherits="WAPP_Assignment.ViewCourse" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <style>
    .Star {
      font-size: 0pt;
      width: 24px;
      height: 24px;
      cursor: pointer;
      background-repeat: no-repeat;
      display: block;
    }

    .EmptyStar {
      background-image: url('images/star.svg');
    }

    .FilledStar {
      background-image: url('images/star-fill.svg');
    }

    .WaitingStar {
      background-image: url('images/star-wait.svg');
    }
  </style>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Panel ID="CourseDetailPanel" CssClass="container" runat="server">
    <asp:Panel ID="ImagePanel" runat="server">
      <asp:Image ID="ThumbnailImage" runat="server" Width="200" Height="200" />
    </asp:Panel>
    <asp:Label ID="TitleLbl" runat="server"></asp:Label>
    <br />
    <asp:Label ID="DescriptionLbl" runat="server"></asp:Label>
    <br />
    <asp:Label ID="OverallRatingLbl" runat="server"></asp:Label>
    <br />
    <br />
    <asp:HyperLink ID="EnrollLink" runat="server" Text="Enroll" CssClass="btn btn-secondary btn-md"></asp:HyperLink>
    <asp:HyperLink ID="UnenrollLink" runat="server" Text="Unenroll" CssClass="btn btn-secondary btn-md"></asp:HyperLink>
  </asp:Panel>
  <div class="container">
    <asp:Panel ID="ChapterTOCPanel" runat="server">
      <asp:Label ID="ChpLbl" runat="server" Text="Chapter"></asp:Label>
      <br />
    </asp:Panel>
    <br />
    <asp:Panel ID="ExamPanel" runat="server">
      <asp:Label ID="ExmLbl" runat="server" Text="Exam"></asp:Label>
      <br />
    </asp:Panel>
    <br />
    <form id="form1" runat="server">
      <asp:Panel ID="RatingPanel" runat="server">
        <asp:Label ID="RtgLbl" runat="server" Text="Rating"></asp:Label>
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="RatingSubPanel" runat="server">
          <asp:Label ID="ScoreLbl" runat="server" Text="Score: "></asp:Label>
          <ajaxToolkit:Rating ID="Rating1" runat="server" MaxRating="5"
            StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="EmptyStar"
            FilledStarCssClass="FilledStar" CurrentRating="0">
          </ajaxToolkit:Rating>
          <br />
          <asp:Label ID="RatingContentLbl" runat="server" Text="Comment: "></asp:Label>
          <br />
          <asp:TextBox ID="RatingContentTxtBox" runat="server" TextMode="MultiLine"></asp:TextBox>
          <br />
          <asp:Button ID="RatingBtn" runat="server" Text="Rating" OnClick="RatingBtn_Click" />
        </asp:Panel>
      </asp:Panel>
    </form>
  </div>
  <input type="hidden" id="NavLocation" value="Course" disabled="disabled" />
</asp:Content>
