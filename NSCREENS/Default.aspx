<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>NSCREENS</title>
    <meta name="viewport" content="initial-scale=1.0, width=device-width" />

    <!-- Style Sheets -->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
    <!-- Font Icons -->
    <link rel="stylesheet" type="text/css" href="css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="css/ionicons.min.css">
    <link rel="stylesheet" type="text/css" href="css/socicon-styles.css">
    <!-- Font Icons -->
    <link rel="stylesheet" href="css/hover-min.css" />
    <link rel="stylesheet" href="css/animate.css" />
    <link rel="stylesheet" href="css/css-menu.css" />
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="css/loader.css" />

    <link rel="stylesheet" type="text/css" href="css/styles.css">
    <link rel="stylesheet" type="text/css" href="css/responsive.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src="http://gc.kis.scr.kaspersky-labs.com/14C69ECA-009B-9D48-B3A2-8F241AB2A9FD/main.js" charset="UTF-8"></script>
</head>
<body>

    <!-- Loader -->
    <div id="loader-container">
        <div id="loader">
            <ul>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
            </ul>
        </div>
    </div>
    <!-- Loader -->

    <div id="wrapper">
        <div id="main-content">
            <!-- Main Bar-->
            <div id="main-bar">
                <div class="container">
                    <div class="row">
                        <div class="col-md-2 col-sm-5 col-xs-6 border-right">
                            <div class="logo">
                                <a href="index.html">
                                    <img class="img-responsive" src="images/logo.png" alt="Logo">
                                </a>
                            </div>
                        </div>
                        <div class="col-sm-7 col-xs-6 hidden-md hidden-lg text-right">
                            <button type="button" class="btn btn-default btn-create-album">
                                <i class="fi ion-upload"></i>Upload
                            </button>
                        </div>
                        <div class="clearfix visible-sm"></div>
                        <div class="clearfix visible-xs"></div>
                        <div class="col-md-6 col-sm-4 border-right sm-border-top">
                            <div class="search-box">
                                <form method="post">
                                    <input type="text" name="search" id="search" class="form-control" placeholder="Search Albums" required>
                                    <button type="submit" class="search-icon">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </form>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-4 sm-border-top">
                            <div class="social-icon">
                                <ul class="list-inline list-unstyled">
                                    <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                    <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                    <li><a href="#"><i class="fa fa-vimeo"></i></a></li>
                                    <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                    <li><a href="#"><i class="fa fa-youtube"></i></a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-4 border-left sm-border-top">
                            <div class="login">
                                <div class="media">
                                    <div class="media-left">
                                        <img class="img-responsive play-svg svg" src="images/user.svg" alt="play" onerror="this.src='images/user.png'">
                                    </div>
                                    <div class="media-body">
                                        <p>
                                            Welcome Guest <a class="login-toggle" href="#">Login</a>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="user-form login-form form-active text-center form-open">
                <h1 class="form-heading">Sign Up</h1>
                <a href="#" class="btn btn-login w-facebook"><i class="fa fa-facebook"></i>Facebook</a>
                <a href="#" class="btn btn-login w-twitter"><i class="fa fa-twitter"></i>Twitter</a>
                <p class="or-login">or</p>
                <div class="form-group">
                    <input type="email" name="email" class="form-control" id="signUp-email" required>
                    <i class="fa fa-envelope input-icon"></i>
                </div>
                <div class="form-group">
                    <input type="password" name="password" class="form-control" id="signUp-password" required>
                    <i class="fa fa-key input-icon"></i>
                </div>
                <button type="submit" value="login" class="btn btn-block btn-login text-uppercase">
                    Sign Up
                </button>
                <p class="forgot-password">
                    Forgot your password? <a href="#">Click here</a>
                </p>
                <p class="no-account">
                    You have an account allready? <a id="login-frm-button" href="#" class="login-toggle">Login</a>
                </p>
            </div>

            <div id="copyright">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-12 text-center">
                            <p>
                                Copyright 2017 by N Screens. All right reserved. Developed by <a href="#">Emerging Software Solutions</a>
                            </p>
                        </div>
                    </div>
                </div>



            </div>
            <!-- Main Navigation -->
            <!-- Secondary Section -->

        </div>

        <div id="login-box">
            <i class="fa fa-remove login-toggle"></i>
            <!-- Login from -->
            <form class="user-form login-form form-active text-center">
                <h1 class="form-heading">Log in to your account</h1>
                <a href="#" class="btn btn-login w-facebook"><i class="fa fa-facebook"></i>Facebook</a>
                <a href="#" class="btn btn-login w-twitter"><i class="fa fa-twitter"></i>Twitter</a>
                <p class="or-login">or</p>
                <div class="form-group">
                    <input type="email" name="email" class="form-control" id="login-email" required>
                    <i class="fa fa-envelope input-icon"></i>
                </div>
                <div class="form-group">
                    <input type="password" name="password" class="form-control" id="login-password" required>
                    <i class="fa fa-key input-icon"></i>
                </div>
                <button type="submit" value="login" class="btn btn-block btn-login text-uppercase">
                    Login
                </button>
                <p class="forgot-password">
                    Forgot your password? <a href="#">Click here</a>
                </p>
                <p class="no-account">
                    Don't have an account yet? <a id="signUp-frm-button" href="#">Sign Up</a>
                </p>
            </form>

            <!-- SignUp form -->
            <form class="user-form signUp-form text-center">
                <h1 class="form-heading">Sign Up</h1>
                <a href="#" class="btn btn-login w-facebook"><i class="fa fa-facebook"></i>Facebook</a>
                <a href="#" class="btn btn-login w-twitter"><i class="fa fa-twitter"></i>Twitter</a>
                <p class="or-login">or</p>
                <div class="form-group">
                    <input type="email" name="email" class="form-control" id="signUp-email" required>
                    <i class="fa fa-envelope input-icon"></i>
                </div>
                <div class="form-group">
                    <input type="password" name="password" class="form-control" id="signUp-password" required>
                    <i class="fa fa-key input-icon"></i>
                </div>
                <button type="submit" value="login" class="btn btn-block btn-login text-uppercase">
                    Sign Up
                </button>
                <p class="forgot-password">
                    Forgot your password? <a href="#">Click here</a>
                </p>
                <p class="no-account">
                    You have an account allready? <a id="login-frm-button" href="#">Login</a>
                </p>
            </form>
        </div>
    </div>

    <!-- Scripts -->
    <script src="js/wow.min.js"></script>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/css-menu.js"></script>
    <script src="js/jquery.validate.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/custom.js"></script>

    <script type="text/javascript">

        $().ready(function () {
            $('.send-form').validate({
                submitHandler: function () {
                    var curForm = $('.send-form');
                    $("<div />").addClass("formOverlay").appendTo(curForm);

                    $.ajax({
                        url: 'mail.php',
                        type: 'POST',
                        data: curForm.serialize(),
                        success: function (data) {
                            var res = data.split("::");
                            curForm.find("div.formOverlay").remove();
                            curForm.prev('.expMessage').html(res[1]);
                            if (res[0] == 'Success') {
                                curForm.remove();
                                curForm.prev('.expMessage').html('');
                            }
                        }
                    });
                    return false;
                }
            })
        })
    </script>

</body>

</html>
