 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WAPP_Assignment.Home" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
  <head runat="server">
    <title>Home</title>
    <!-- Latest compiled and minified CSS -->
    <link
      href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css"
      rel="stylesheet"
    />

    <!-- Latest compiled JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
  </head>
  <body>
    <nav class="navbar navbar-expand-sm navbar-dark bg-dark fixed-top">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">
          <img
            src="./i-Learn.png"
            alt="Logo"
            style="width: 40px"
            class="rounded-pill"
          />
        </a>
        <span class="navbar-text">iLearn</span>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#collapsibleNavbar"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="collapsibleNavbar">
          <ul class="navbar-nav me-auto">
            <li class="nav-item">
              <a class="nav-link active" href="#">Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Page 1</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Page 2</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#">Page 3</a>
            </li>
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                role="button"
                data-bs-toggle="dropdown"
                >Course</a
              >
              <ul class="dropdown-menu">
                <li><a class="dropdown-item" href="#">List</a></li>
                <li><a class="dropdown-item" href="#">Add</a></li>
                <li><a class="dropdown-item" href="#">Edit</a></li>
              </ul>
            </li>
          </ul>
          <!-- <div class="d-flex"> -->
          <!--   <input class="form-control me-2" type="text" placeholder="Search" /> -->
          <!--   <button class="btn btn-primary" type="button">Search</button> -->
          <!-- </div> -->
          <ul class="navbar-nav justify-content-end">
            <li class="nav-item">
              <a class="nav-link" href="/Login.aspx">Login</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="/Register.aspx">Register</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </body>
</html>
