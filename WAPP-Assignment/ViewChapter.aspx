<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewChapter.aspx.cs" Inherits="WAPP_Assignment.ViewChapter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="jquery-oembed-all/jquery.oembed.js" defer></script>
    <link rel="stylesheet" href="jquery-oembed-all/jquery.oembed.css" />
</head>
<body>
    <asp:Label ID="TitleLbl" runat="server"></asp:Label>
    <asp:PlaceHolder ID="ContentPlaceholder" runat="server"></asp:PlaceHolder>
    <script>
        $('oembed').each(function() {
          $(this).replaceWith('<a class="oembed" href="' + this.textContent + '"></a>')
        })
        $(function () {
            $("a.oembed").oembed()
        })
        $(document).ready(function () {
            $("span.oembedall-closehide").remove()
        })
    </script>
</body>
</html>
