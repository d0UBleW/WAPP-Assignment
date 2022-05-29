<%@ Page Language="C#" MasterPageFile="~/SiteAnon.master" AutoEventWireup="true" CodeBehind="ViewCourse.aspx.cs" Inherits="WAPP_Assignment.ViewCourse" ValidateRequest="false" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" href="/Content/course.css" />
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

    .user-rating-img {
      display: flex;
      float: left;
    }

    .user-rating-name {
      display: flex;
      float: left;
    }

    .my-break-word {
      word-wrap: break-word;
    }

  </style>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server" Text="All Courses" NavigateUrl="~/ListCourse.aspx"></asp:HyperLink></li>
  <li class="breadcrumb-item active" aria-current="page">
    <asp:Literal ID="BreadLiteral" runat="server"></asp:Literal></li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <div class="row">

      <asp:Panel ID="CourseDetailPanel" CssClass="col-md-4" runat="server">
        <asp:Panel ID="ImagePanel" runat="server">
          <asp:Image ID="ThumbnailImage" runat="server" Width="200" Height="200" />
        </asp:Panel>
        <h1>
          <asp:Label ID="TitleLbl" runat="server"></asp:Label>
        </h1>
        <asp:Panel ID="CategoryPanel" runat="server"></asp:Panel>
        <asp:Label ID="DescriptionLbl" CssClass="my-break-word" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="OverallRatingLbl" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="NonAdminLinkPanel" CssClass="mb-3" runat="server">
          <asp:HyperLink ID="EnrollLink" runat="server" Text="Enroll" CssClass="btn btn-outline-primary"></asp:HyperLink>
          <asp:HyperLink ID="UnenrollLink" runat="server" Text="Unenroll" CssClass="btn btn-outline-primary"></asp:HyperLink>
        </asp:Panel>
        <div id="AdminActionPanel" runat="server">
          <div id="AdminEditPanel_1" runat="server" class="btn-group btn-group-md mb-3" role="group">
            <asp:HyperLink ID="EditLink" runat="server" Text="Edit Course" CssClass="btn btn-outline-primary"></asp:HyperLink>
            <asp:HyperLink ID="DelLink" runat="server" Text="Delete Course" CssClass="btn btn-outline-danger"></asp:HyperLink>
          </div>
          <br />
          <div id="AdminEditPanel_2" runat="server" class="btn-group btn-group-md mb-3" role="group">
            <asp:HyperLink ID="EditChapMenuLink" runat="server" Text="Edit Chapter Menu" CssClass="btn btn-outline-primary"></asp:HyperLink>
            <asp:HyperLink ID="EditExamMenuLink" runat="server" Text="Edit Exam Menu" CssClass="btn btn-outline-primary"></asp:HyperLink>
          </div>
          <br />
          <div id="AdminStudentPanel" runat="server" class="btn-group btn-group-md mb-3" role="group">
            <asp:HyperLink ID="StudentDataLink" runat="server" Text="Enrolled Student" CssClass="btn btn-outline-primary btn-md"></asp:HyperLink>
            <asp:HyperLink ID="GradeLink" runat="server" Text="Grades" CssClass="btn btn-outline-primary btn-md"></asp:HyperLink>
          </div>
        </div>
      <form id="form1" runat="server">
        <asp:Panel ID="RatingPanel" runat="server">
          <h5>
            <asp:Label ID="RtgLbl" runat="server" Text="Rating"></asp:Label>
          </h5>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:Panel ID="RatingSubPanel" CssClass="mb-3" runat="server">
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
            <br />
            <asp:Button ID="RatingBtn" runat="server" Text="Rating" OnClick="RatingBtn_Click" />
          </asp:Panel>
          <div id="RatingListPanel" class="list-group list-group-flush" runat="server"></div>
        </asp:Panel>
      </form>
      </asp:Panel>
      <div class="col-md-8">
        <asp:Panel ID="ChapterTOCPanel" CssClass="mb-3" runat="server">
          <h2 class="mb-1">
            <asp:Label ID="ChpLbl" runat="server" Text="Chapter"></asp:Label>
          </h2>
          <div id="ChapListPanel" class="list-group list-group-flush" runat="server">
          </div>
        </asp:Panel>
        <br />
        <asp:Panel ID="ExamPanel" CssClass="mb-3" runat="server">
          <h2 class="mb-1">
            <asp:Label ID="ExmLbl" runat="server" Text="Exam"></asp:Label>
          </h2>
          <div id="ExamListPanel" class="list-group list-group-flush" runat="server">
          </div>
        </asp:Panel>
      </div>
      <br />
    </div>
  </div>
  <input type="hidden" id="NavLocation" value="course" disabled="disabled" />
  <script>
    $("[id$='DelLink'").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })
    $("[data-del-rating='yes'").on('click', function () {
      return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';
    })
  </script>
</asp:Content>
