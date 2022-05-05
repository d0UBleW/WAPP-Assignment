<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WAPP_Assignment.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container">
        <h1>My First Bootstrap Page</h1>
        <p>Resize this responsive page to see the effect!</p>
    </div>
    <form id="form1" runat="server">
        <asp:Button ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click" Text="Logout" CausesValidation="False" UseSubmitBehavior="False" />
    </form>
    <br /><br />
    <asp:HyperLink ID="AllCoursesLink" runat="server" NavigateUrl="/ListCourse.aspx">All Courses</asp:HyperLink>
    <asp:HyperLink ID="MyCoursesLink" runat="server" NavigateUrl="/MyCourse.aspx" Visible="false">My Courses</asp:HyperLink>
</body>
</html>
