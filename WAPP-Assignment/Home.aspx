<%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteAnon.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WAPP_Assignment.Home" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
  <style>
    .carousel-indicators {
      bottom: -30px;
    }

    .carousel-indicators button {
      background-color: gray !important;
    }

    .carousel-indicators .active {
      background-color: black !important;
    }

    #carouselSlides {
      margin-left: 50px;
      margin-right: 50px;
    }

    #carouselSlides .carousel-control-prev {
      margin-left: -150px;
    }

    #carouselSlides .carousel-control-next {
      margin-right: -150px;
    }

    #carouselSlides .carousel-inner {
      margin-bottom: 30px;
    }

    #AboutUs p {
      text-align: justify;
    }
  </style>
</asp:Content>

<asp:Content ID="BreadContent" ContentPlaceHolderID="BreadcrumbContent" runat="server">
  <li class="breadcrumb-item active" aria-current="page">Home</li>
</asp:Content>


<asp:Content ID="HomeContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <div class="d-flex justify-content-center align-items-center">
      <div id="carouselSlides" class="carousel slide carousel-fade" data-bs-ride="carousel" style="width: 1000px;">
        <div class="carousel-indicators">
          <button type="button" data-bs-target="#carouselSlides" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
          <button type="button" data-bs-target="#carouselSlides" data-bs-slide-to="1" aria-label="Slide 2"></button>
          <button type="button" data-bs-target="#carouselSlides" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
          <div class="carousel-item active">
            <img src="/images/slides/a.jpg" class="d-block w-100 img-fluid" alt="...">
          </div>
          <div class="carousel-item">
            <img src="/images/slides/b.jpg" class="d-block w-100 img-fluid" alt="...">
          </div>
          <div class="carousel-item">
            <img src="/images/slides/c.jpg" class="d-block w-100 img-fluid" alt="...">
          </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselSlides" data-bs-slide="prev">
          <span class="bi bi-arrow-left-circle-fill text-dark fs-1" aria-hidden="true"></span>
          <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselSlides" data-bs-slide="next">
          <span class="bi bi-arrow-right-circle-fill text-dark fs-1" aria-hidden="true"></span>
          <span class="visually-hidden">Next</span>
        </button>
      </div>
    </div>

    <div class="border-top pt-5 mt-5" style="text-align: justify;">
      <div class="p-5 mb-4 bg-light rounded-3">
        <div class="container-fluid py-5">
          <h1 class="fw-bold">Embark on Your Journey Now</h1>
          <div class="col-md-8">
            <div id="quoteCarousel" class="carousel slide" data-bs-ride="carousel">
              <div class="carousel-indicators">
                <button type="button" data-bs-target="#quoteCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                <button type="button" data-bs-target="#quoteCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                <button type="button" data-bs-target="#quoteCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
              </div>
              <div class="carousel-inner">

                <div class="carousel-item active">
                  <figure class="fs-4 text-start">
                    <blockquote class="blockquote">
                      <p>Those who keep learning, will keep rising in life.</p>
                    </blockquote>
                    <figcaption class="blockquote-footer">
                      Charlie Munger
                    </figcaption>
                  </figure>
                </div>


                <div class="carousel-item">
                  <figure class="fs-4 text-start">
                    <blockquote class="blockquote">
                      <p>A good education is a foundation for a better future</p>
                    </blockquote>
                    <figcaption class="blockquote-footer">
                      Elizabeth Warren
                    </figcaption>
                  </figure>
                </div>


                <div class="carousel-item">
                  <figure class="fs-4 text-start">
                    <blockquote class="blockquote">
                      <p>The roots of education are bitter, but the fruit is sweet</p>
                    </blockquote>
                    <figcaption class="blockquote-footer">
                      Aristotle
                    </figcaption>
                  </figure>
                </div>

              </div>
            </div>
          </div>
          <asp:HyperLink ID="JumboLink" runat="server" NavigateUrl="/Login.aspx" class="btn btn-primary btn-lg mt-5">Login</asp:HyperLink>
        </div>
      </div>
    </div>

    <div id="AboutUs">
      <div class="border-top pt-5 mt-5" style="text-align: justify;">
        <h1 id="Story">Our Story</h1>
        <p>
          <b>i-Learn</b> web application is a web-based learning platform for students to make learning activities more interactive and entertaining. In this platform, everyone would be able to list down all available resources and view ratings of the materials. To gain more functionality, students must go through the registration process before diving into the learning proccess. Students could take quiz exam to test their knowledge as well. Students would be able to submit feedback and raating so that the learning materials' quality is well-maintained.
        </p>
      </div>
      <div class="border-top pt-5 mt-5" style="text-align: justify;">
        <h1 id="Vision">Our Vision</h1>
        <p>
          We envision ourselves to be everyone's first choice when learning programming.
        </p>
      </div>
      <div class="border-top pt-5 mt-5" style="text-align: justify;">
        <h1 id="Goal">Our Goal</h1>
        <p>
          The goal of creating this web application is to provide students with a fun and interactive learning environment and introduce them to the basic of programming where they get to choose what langauge they wish to learn. Besides, this web application ensures that student understandingg by testing them with questions.
        </p>
      </div>
    </div>

  </div>
  <input type="hidden" id="NavLocation" value="home" disabled="disabled" />
</asp:Content>
