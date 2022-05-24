<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ReviewExam.aspx.cs" Inherits="WAPP_Assignment.Learn.ReviewExam" %>

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

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <div class="row">
      <div class="col-sm-2">
        <nav id="toc" class="sticky-top" style="margin-top: -100px; padding-top: 100px;"></nav>
      </div>
      <div class="col-sm-8">
        <asp:Panel ID="TOCPanel" runat="server"></asp:Panel>
        <h1 data-toc-skip="true">
          <asp:Label ID="TitleLbl" runat="server"></asp:Label>
        </h1>
        <asp:Label ID="ScoreLbl" runat="server"></asp:Label>
        <asp:Panel ID="RetakePanel" runat="server">
          <br />
          <asp:HyperLink ID="RetakeLink" runat="server" CssClass="btn btn-primary btn-md" Text="Retake"></asp:HyperLink>
          <br />
          <br />
        </asp:Panel>
        <div class="form-check">
          <input type="checkbox" class="form-check-input" id="toggleAnswer" />
          <label for="toggleAnswer" class="form-check-label">Show answer</label>
        </div>
        <br />

        <form id="form1" runat="server">
          <asp:Panel ID="ContentPanel" runat="server">
          </asp:Panel>
          <asp:HiddenField ID="CorrectOptIDField" runat="server" />
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

    $("#toggleAnswer").on('change', function () {
      console.log("ch")
      const answerIds = $("input[id$='CorrectOptIDField']").val().split(',')
      console.log(answerIds)
      $("input[id*='optList']").each(function () {
        if (answerIds.includes($(this).val())) {
          console.log($(this))
          const $label = $(this).closest("span").find("label")
          console.log($label)
          const currText = $label.text()
          console.log(currText)
          if ($("#toggleAnswer").is(":checked")) {
            $label.text(currText + " ✔")
          }
          else {
            $label.text(currText.slice(0, currText.length - 2))
          }
        }
      })
    })
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
