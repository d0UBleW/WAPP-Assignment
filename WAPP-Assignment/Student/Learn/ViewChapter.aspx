<%@ Page Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ViewChapter.aspx.cs" Inherits="WAPP_Assignment.ViewChapter" %>

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
  <li class="breadcrumb-item active" aria-current="page">View Chapter</li>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-sm-2">
        <nav id="toc" class="sticky-top" style="margin-top: -100px; padding-top: 100px;"></nav>
      </div>
      <div class="col-sm-8">
        <asp:Panel ID="ContentPanel" runat="server">
          <h1 data-toc-skip="true">
            <asp:Label ID="TitleLbl" runat="server"></asp:Label></h1>
          <asp:PlaceHolder ID="ContentPlaceholder" runat="server"></asp:PlaceHolder>
        </asp:Panel>
      </div>
      <div class="col-sm-2">
        <nav class="sticky-top" style="margin-top: -100px; padding-top: 100px;">
          <asp:Panel ID="OutlinePanel" runat="server" Visible="true" style="width: 150px; margin: 0 auto; height: auto; position: relative;">
            <span>Chapter</span>
            <asp:Panel ID="ChapOutlinePanel" runat="server">
            </asp:Panel>
          </asp:Panel>
          <br />
          <div style="width: 150px; margin: 0 auto; height: auto; position: relative;">
            <form id="form1" runat="server">
              <div class="btn-group btn-md" role="group" style="width : 100%;">
              <asp:Button ID="PrevBtn" runat="server" Text="Previous" CssClass="btn btn-outline-primary btn-sm" Enabled="false" OnClick="PrevBtn_Click" />
              <asp:Button ID="NextBtn" runat="server" Text="Next" CssClass="btn btn-outline-primary btn-sm" Enabled="false" OnClick="NextBtn_Click" />
              </div>
            </form>
          </div>
        </nav>
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
  </script>
</asp:Content>
