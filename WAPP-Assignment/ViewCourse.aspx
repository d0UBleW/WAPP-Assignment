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
        <h1 class="text-break mb-3">
          <asp:Label ID="TitleLbl" runat="server"></asp:Label>
        </h1>
        <asp:Panel ID="CategoryPanel" runat="server"></asp:Panel>
        <asp:Label ID="DescriptionLbl" CssClass="text-break" runat="server"></asp:Label>
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
            <asp:HyperLink ID="DelLink" runat="server" Text="Delete Course" CssClass="btn btn-outline-danger" data-action="warn"></asp:HyperLink>
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
            <asp:HyperLink ID="EnrollStudLink" runat="server" Text="Enroll Student" CssClass="btn btn-outline-primary btn-md"></asp:HyperLink>
          </div>
        </div>
        <form id="form1" runat="server">
          <h5>
            <button id="ratingToggler" type="button" class="btn btn-sm" data-bs-toggle="collapse" data-bs-target="#<%= RatingPanel.ClientID %>"
            aria-expanded="true" aria-controls="<%= RatingPanel.ClientID %>">
              <i class="bi bi-chevron-down"></i>
            </button>
            <asp:Label ID="RtgLbl" runat="server" Text="Rating"></asp:Label>
          </h5>
          <asp:Panel ID="RatingPanel" runat="server" CssClass="collapse show">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:Panel ID="RatingSubPanel" CssClass="mb-3" runat="server">
              <asp:Label ID="ScoreLbl" runat="server" Text="Score: "></asp:Label>
              <ajaxToolkit:Rating ID="Rating1" runat="server" MaxRating="5"
                StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="EmptyStar"
                FilledStarCssClass="FilledStar" CurrentRating="0">
              </ajaxToolkit:Rating>
              <br />
              <div class="form-floating mb-3">
                <asp:TextBox
                  ID="RatingContentTxtBox"
                  runat="server"
                  CssClass="form-control"
                  Placeholder="Rating comment"
                  ToolTip="Rating comment"
                  TextMode="MultiLine"
                  Height="100"
                  MaxLength="1000"
                  data-max-len="true"></asp:TextBox>
                <label for="<%= RatingContentTxtBox.ClientID %>" class="text-muted">Comment</label>
              </div>
              <asp:Button ID="RatingBtn" runat="server" Text="Rate" OnClick="RatingBtn_Click" CssClass="btn btn-outline-primary btn-sm" />
            </asp:Panel>
            <div id="RatingListPanel" class="list-group list-group-flush" runat="server"></div>
          </asp:Panel>
        </form>
      </asp:Panel>

      <div class="col-md-8">
        <asp:Panel ID="ChapterTOCPanel" CssClass="mb-3" runat="server">
          <h2 class="mb-1">
            <button id="chapToggler" type="button" class="btn btn-sm" data-bs-toggle="collapse" data-bs-target="#<%= ChapListPanel.ClientID %>" aria-expanded="true" aria-controls="<%= ChapListPanel.ClientID %>">
              <i class="bi bi-chevron-down"></i>
            </button>
            <asp:Label ID="ChpLbl" runat="server" Text="Chapter"></asp:Label>
          </h2>
          <div id="ChapListPanel" class="list-group list-group-flush collapse show text-truncate" runat="server">
          </div>
        </asp:Panel>
        <br />
        <asp:Panel ID="ExamPanel" CssClass="mb-3" runat="server">
          <h2 class="mb-1">
            <button id="examToggler" type="button" class="btn btn-sm" data-bs-toggle="collapse" data-bs-target="#<%= ExamListPanel.ClientID %>" aria-expanded="true" aria-controls="<%= ExamListPanel.ClientID %>">
              <i class="bi bi-chevron-down"></i>
            </button>
            <asp:Label ID="ExmLbl" runat="server" Text="Exam"></asp:Label>
          </h2>
          <div id="ExamListPanel" class="list-group list-group-flush collapse show text-truncate" runat="server">
          </div>
        </asp:Panel>
      </div>
      <br />
    </div>
    <div class="modal fade" id="enrollModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="enrollModalTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="enrollModalTitle">Unable to view</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <p>
              You are not enrolled to this course yet. Please enroll before viewing its content.
            </p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <input type="hidden" id="NavLocation" value="course" disabled="disabled" />
  <script>
    $("[data-bs-toggle='collapse']").on('click', function () {
      $(this).find("i").toggleClass("bi-chevron-down bi-chevron-right")
    })
  </script>
</asp:Content>
