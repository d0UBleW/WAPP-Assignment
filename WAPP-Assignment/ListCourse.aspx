<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCourse.aspx.cs" Inherits="WAPP_Assignment.ListCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Courses</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" integrity="sha512-894YE6QWD5I59HgZOGReFYm4dnWc1Qt5NtvYSaNcOP+u1T9qYdvdihz0PPSiiqn/+/3e7Jo4EaG7TubfWGUrMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="/Scripts/searchCourse.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="SearchTitleLbl" runat="server" Text="Search: "></asp:Label>
            <asp:TextBox ID="SearchTitleTxtBox" runat="server"></asp:TextBox>
            <asp:Label ID="SearchCatLbl" runat="server" Text="Search: " style="display: none;"></asp:Label>
            <asp:TextBox ID="SearchCatTxtBox" runat="server" style="display: none;"></asp:TextBox>
            <asp:Label ID="FilterLbl" runat="server" Text="Filter: "></asp:Label>
            <ajaxToolkit:AutoCompleteExtender ID="AutoTitle" runat="server" ServiceMethod="SearchTitle"
                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
                CompletionSetCount="10" TargetControlID="SearchTitleTxtBox" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCat" runat="server" ServiceMethod="SearchCategory"
                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false"
                CompletionSetCount="10" TargetControlID="SearchCatTxtBox" FirstRowSelected="false"></ajaxToolkit:AutoCompleteExtender>
            <asp:DropDownList ID="FilterList" runat="server">
                <asp:ListItem Selected="True" Text="Title" Value="title"></asp:ListItem>
                <asp:ListItem Selected="False" Text="Category" Value="category"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <button type="button" id="searchBtn" class="btn btn-primary btn-sm">Search</button>
        </div>
    </form>
    <br /><br />
    <asp:HyperLink ID="DashboardLink" runat="server" NavigateUrl="/Dashboard.aspx" Text="Dashboard" CssClass="btn btn-secondary btn-sm"></asp:HyperLink>
    <asp:HyperLink ID="AddCourseLink" runat="server" Text="Add Course" NavigateUrl="/Admin/Course/AddCourse.aspx" CssClass="btn btn-secondary btn-sm"></asp:HyperLink>
    <br />
    <br /><br />
    <asp:Panel ID="CoursePanel" runat="server">
        <asp:PlaceHolder ID="CoursePlaceholder" runat="server"></asp:PlaceHolder>
    </asp:Panel>
</body>
</html>
