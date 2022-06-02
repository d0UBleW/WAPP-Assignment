<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ViewExam.aspx.cs" Inherits="WAPP_Assignment.Learn.ViewExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/jquery-oembed-all/jquery.oembed.js" defer></script>
  <link rel="stylesheet" href="/jquery-oembed-all/jquery.oembed.css" />
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.5.0/styles/default.min.css" />
  <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.5.0/highlight.min.js"></script>
  <!-- add after bootstrap.min.css -->
  <link
    rel="stylesheet"
    href="https://cdn.rawgit.com/afeld/bootstrap-toc/v1.0.1/dist/bootstrap-toc.min.css" />
  <!-- add after bootstrap.min.js or bootstrap.bundle.min.js -->
  <script src="https://cdn.rawgit.com/afeld/bootstrap-toc/v1.0.1/dist/bootstrap-toc.min.js"></script>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item"><a href="/Home.aspx">Home</a></li>
  <li class="breadcrumb-item"><a href="/Student/Course/MyCourse.aspx">My Courses</a></li>
  <li class="breadcrumb-item">
    <asp:HyperLink ID="CourseLink" runat="server"></asp:HyperLink></li>
  <li class="breadcrumb-item">Exam</li>
  <li class="breadcrumb-item active" aria-current="page"><asp:Literal ID="ExamBreadLit" runat="server"></asp:Literal></li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-sm-2">
        <nav id="toc" class="sticky-top" style="margin-top: -100px; padding-top: 100px;"></nav>
      </div>
      <div class="col-sm-8">
        <h1 data-toc-skip="true" class="border-bottom mb-3">
          <asp:Label ID="TitleLbl" runat="server"></asp:Label>
        </h1>
        <div class="mb-3 alert alert-info" role="alert">
          Retake: <strong><asp:Label ID="RetakeLbl" runat="server" CssClass=""></asp:Label></strong>
        </div>
        <form id="form1" runat="server">
          <asp:Panel ID="ContentPanel" runat="server">
          </asp:Panel>
          <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" CssClass="btn btn-primary btn-md" />
        </form>
      </div>
    </div>
  </div>
  <script>
    $('oembed').each(function () {
      $(this).replaceWith('<a class="oembed" href="' + this.textContent + '"></a>')
    })
    $(function () {
      $("a.oembed").oembed()
    })
    $(document).ready(function () {
      $("span.oembedall-closehide").remove()
    })
    document.addEventListener('DOMContentLoaded', (event) => {
      document.querySelectorAll('pre code').forEach((el) => {
        hljs.highlightElement(el);
      });
    });
    $(function () {
      var navSelector = "#toc";
      var $myNav = $(navSelector);
      Toc.init($myNav);
      $("body").scrollspy({
        target: navSelector,
      });
    })

    $("a").on('click', function () {
      return confirm('Discard current working progress?');
    })

    $("input[id$='SubmitBtn']").on('click', function () {
      return confirm('Are you sure?');
    })
  </script>
</asp:Content>
