<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStudent.master" AutoEventWireup="true" CodeBehind="ViewExam.aspx.cs" Inherits="WAPP_Assignment.Learn.ViewExam" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <script src="/jquery-oembed-all/jquery.oembed.js" defer></script>
  <link rel="stylesheet" href="/jquery-oembed-all/jquery.oembed.css" />
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.5.0/styles/default.min.css" />
  <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.5.0/highlight.min.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">

  <asp:Panel ID="TOCPanel" runat="server"></asp:Panel>
  <h1>
    <asp:Label ID="TitleLbl" runat="server"></asp:Label>
  </h1>
  <asp:Label ID="RetakeLbl" runat="server"></asp:Label>
  <form id="form1" runat="server">
    <asp:Panel ID="ContentPanel" runat="server">
    </asp:Panel>
    <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" />
  </form>
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
  </script>
</asp:Content>
