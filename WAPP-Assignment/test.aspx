<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WAPP_Assignment.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/addOption.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tbody>
              <tr>
                <td><input type="checkbox" name="color_1[]" value="123" /></td>
                <td><input type="text" name="color_2[]" /></td>
              </tr>
              <tr>
                <td><input type="text" name="color_3[]" /></td>
                <td><input type="text" name="color_4[]" /></td>
              </tr>
                </tbody>
            </table>
            <br />
            <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="test"/>
        </div>
    </form>
</body>
</html>
