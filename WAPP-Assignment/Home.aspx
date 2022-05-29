<%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WAPP_Assignment.Home" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <style>
    .slide-img {
      object-fit: cover;
      width: 800px;
      height: 600px;
    }
  </style>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item active" aria-current="page">Home</li>
</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h1>Home Page</h1>
    <div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
      <div class="carousel-indicators">
        <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
        <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
        <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
      </div>
      <div class="carousel-inner">
        <div class="carousel-item active">
          <img src="/images/slide1.jpg" class="d-block w-100 slide-img" alt="...">
        </div>
        <div class="carousel-item">
          <img src="/images/slide2.jpg" class="d-block w-100 slide-img" alt="...">
        </div>
        <div class="carousel-item">
          <img src="/images/slide2.jpg" class="d-block w-100 slide-img" alt="...">
        </div>
      </div>
      <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
      </button>
      <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
      </button>
    </div>
  </div>
  <input type="hidden" id="NavLocation" value="home" disabled="disabled" />
</asp:Content>
