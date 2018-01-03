<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Admin_Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <title>NSCREENS</title>
    <meta name="viewport" content="initial-scale=1.0, width=device-width" />

    <!-- Style Sheets -->
    <link rel="stylesheet" type="text/css" href="../css/bootstrap.min.css">
    <!-- Font Icons -->
    <link rel="stylesheet" type="text/css" href="../css/font-awesome.min.css">
    <link rel="stylesheet" type="text/css" href="../css/ionicons.min.css">
    <link rel="stylesheet" type="text/css" href="../css/socicon-styles.css">
    <!-- Font Icons -->
    <link rel="stylesheet" href="../css/hover-min.css" />
    <link rel="stylesheet" href="../css/animate.css" />
    <link rel="stylesheet" href="../css/css-menu.css" />
    <link rel="stylesheet" href="../css/owl.carousel.css" />
    <link rel="stylesheet" href="../css/loader.css" />

    <link rel="stylesheet" type="text/css" href="../css/styles.css">
    <link rel="stylesheet" type="text/css" href="../css/responsive.css">
    <link rel="stylesheet" type="text/css" href="css/login.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src="http://gc.kis.scr.kaspersky-labs.com/14C69ECA-009B-9D48-B3A2-8F241AB2A9FD/main.js" charset="UTF-8"></script>
    <script src="../pnotify/jquery.min.js" type="text/javascript"></script>
    <script src="../pnotify/jquery.pnotify.js" type="text/javascript"></script>
    <link href="../pnotify/jquery.pnotify.default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pnotifySuccess(t, msg, ty) {
            $(function () {
                $.pnotify({
                    title: t,
                    text: msg,
                    type: ty,
                    styling: 'bootstrap',
                    history: false
                });
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
        <div id="wrapper">
            <div id="main-content">
                <!-- Main Bar-->
                <div id="main-bar">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-2 col-sm-5 col-xs-6 border-right">
                                <div class="logo">
                                    <a href="frmHome.aspx">
                                        <img class="img-responsive" src="../images/logo.png" alt="Logo">
                                    </a>
                                </div>
                            </div>
                            <div class="clearfix visible-sm"></div>
                            <div class="clearfix visible-xs"></div>
                            <div class="col-md-5 col-sm-3 border-right sm-border-top">
                                <div class="search-box">
                                    <form method="post">
                                        <input type="text" name="search" id="search" class="form-control" placeholder="Search Albums">
                                        <button type="submit" class="search-icon">
                                            <i class="fa fa-search"></i>
                                        </button>
                                    </form>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-4 sm-border-top">
                                <center>
                                    <div id="cssmenu">
                                    <ul>
                                        <li>
                                            <a href="#"><i class="fi ion-android-color-palette"></i>Upload</a>
                                            <ul>
                                                <li><a href="frmAddAdvertisement.aspx">Advertisement</a></li>
                                                <li><a href="frmAddShortfilm.aspx">Short Films</a></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>

                                   <%-- <a href="frmAddAdvertisement.aspx">
                                 <button type="button" class="btn btn-default btn-create-album">
                                    <i class="fi ion-upload"></i>&nbsp;Upload
                                </button>
                                        </a>--%>
                                    </center>
                                <%--<div class="social-icon">
                                    <ul class="list-inline list-unstyled">
                                        <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fa fa-vimeo"></i></a></li>
                                        <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                        <li><a href="#"><i class="fa fa-youtube"></i></a></li>
                                    </ul>
                                </div>--%>
                            </div>
                            <div class="col-md-3 col-sm-5 border-left sm-border-top">
                                <div id="cssmenu">
                                    <ul>
                                        <li>
                                            <a href="#"><i class="fi fa fa-user"></i>
                                                <asp:Label ID="lblUsername" runat="server" CssClass="MinimumChar"></asp:Label></a>
                                            <ul>
                                                <li><a href="frmSharingBudget.aspx">Earning Money
                                                    <asp:Label ID="lblEarningMoney" runat="server" CssClass="pull-right"></asp:Label></a></li>
                                                <li><a href="frmPublishShortFilm.aspx">Publish Short Film</a></li>
                                                <li>
                                                    <a href="Inbox.aspx">
                                                        <i class="icon-envelope-open"></i>My Inbox
                                            
                                           
                                                    <asp:Label ID="lblInbox" runat="server" CssClass=" badge" Style="background-color: red;"></asp:Label>


                                                    </a>
                                                </li>


                                                <li><a href="MyProfile.aspx">Profile</a></li>
                                                <li><a href="Settings.aspx">Settings</a></li>
                                                <li>
                                                    <asp:LinkButton ID="lnk" runat="server">Logout</asp:LinkButton></li>
                                                <%--<li><a href="../Login.aspx">Logout</a></li>--%>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>
                                <%--<div class="login">
                                    <div class="media">
                                        <div class="media-left">
                                            <img class="img-responsive play-svg svg" src="../images/user.svg" alt="play" onerror="this.src='images/user.png'">
                                        </div>
                                        <div class="media-body">
                                            <p>
                                                <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                <a href="../Login.aspx">Logout</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Main Bar -->
                <!-- Main Navigation -->
                <div id="main-navigation">
                    <div class="container">
                        
                    </div>
                </div>
                <!-- Main Navigation -->
                <!-- Secondary Section -->
                <div id="secondary-section">
                    <div class="container">
                        fdg
                        df
                        g df
                        g
                        dfg
                        fd
                        g
                        fh
                        g
                        j
                        ty
                        rt
                        y
                        t w
                    </div>
                </div>

                <!-- Secondary Section -->



                <!-- copyright -->
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
                <!-- copyright -->

            </div>

            
        </div>
        <div>
        </div>
        
        <script src="../js/custom.js"></script>

        
    </form>
</body>
</html>
