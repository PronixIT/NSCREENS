<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%--<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>--%>
<html>
<head runat="server" id="mainHead">
    <meta charset="UTF-8">
    <title>NSCREENS</title>
  <meta charset="UTF-8" />
<meta name="viewport" content="initial-scale=1.0, width=device-width" />
<meta charset="UTF-8" />
<meta name="viewport" content="initial-scale=1.0, width=device-width" />
    <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
<%--<meta property="og:url" content="http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=1&userId=7" />
<meta property="og:type" content="video.movie" />
<meta property="og:title" content="ARJUN REDDY">
<meta property="og:description" content="Arjun Reddy is an Indian Telugu-language film written and directed by Sandeep Reddy Vanga, produced by Pranay Vanga for Bhadrakali Pictures. The soundtrack is composed by Radhan">
<meta property="og:image" content="http://www.nscreens.com/Video_Images/Shortfilm_6.jpg" />
    <meta property="fb:app_id" content="566086956918274" />--%>

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
    <link rel="stylesheet" type="text/css" href="css/login.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <%-- <script type="text/javascript">
         $(function () {
             $("input[id$='txtFrmDate']").datepicker();
             $("input[id$='txtToDate']").datepicker();
         });
    </script>--%>
    <script type="text/javascript" src="http://gc.kis.scr.kaspersky-labs.com/14C69ECA-009B-9D48-B3A2-8F241AB2A9FD/main.js" charset="UTF-8"></script>
    <script src="pnotify/jquery.min.js" type="text/javascript"></script>
    <script src="pnotify/jquery.pnotify.js" type="text/javascript"></script>
    <link href="pnotify/jquery.pnotify.default.css" rel="stylesheet" type="text/css" />
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
    <%--<script type="text/javascript">
        (function () {
            var po = document.createElement('script');
            po.type = 'text/javascript'; po.async = true;
            po.src = 'https://plus.google.com/js/client:plusone.js';
            var s = document.getElementsByTagName('script')[0];
            s.parentNode.insertBefore(po, s);
        })();
    </script>--%>
    <%--<script>
        // Load the SDK Asynchronously
        (function (d) {
            var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement('script'); js.id = id; js.async = true;
            js.src = "//connect.facebook.net/en_US/all.js";
            ref.parentNode.insertBefore(js, ref);
        }(document));

        // Init the SDK upon load
        window.fbAsyncInit = function () {
            FB.init({
                appId: '566086956918274', // App ID
                channelUrl: '//' + window.location.hostname + '/channel', // Path to your Channel File
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true  // parse XFBML
            });
            // listen for and handle auth.statusChange events
            FB.Event.subscribe('auth.statusChange', function (response) {
                if (response.authResponse) {
                    // user has auth'd your app and is logged into Facebook
                    var uid = "http://graph.facebook.com/" + response.authResponse.userID + "/picture";
                    FB.api('/me', { locale: 'en_US', fields: 'name, email' }, function (me) {
                        console.log(me);
                        document.getElementById('<%= txtName.ClientID %>').value = me.name;
                        document.getElementById('<%= txtEmailId.ClientID %>').value = me.email;
                        //document.getElementById('auth-displayname').innerHTML = me.name;
                        //document.getElementById('Email').innerHTML = me.age_range;
                        //document.getElementById('profileImg').src = uid;
                    })
                    document.getElementById('auth-loggedout').style.display = 'none';
                    document.getElementById('auth-loggedin').style.display = 'block';
                } else {
                    // user has not auth'd your app, or is not logged into Facebook
                    document.getElementById('auth-loggedout').style.display = 'block';
                    document.getElementById('auth-loggedin').style.display = 'none';
                }
            });
            $("#auth-logoutlink").click(function () { FB.logout(function () { window.location.reload(); }); });
        }
    </script>--%>
    <script language="javascript" type="text/javascript">
        //function to disable browser back button
        function DisableBackButton() {
            window.history.forward();
        }
        setTimeout("DisableBackButton()", 0);
        window.onunload = function () { null };




        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <%--<script type="text/javascript">
        function Loader()
        {
            document.getElementById('<%= btnSubmit.ClientID %>').style.display = 'none';
            document.getElementById('<%= imgloader.ClientID %>').style.display = 'block';
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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



        <div class="container">
            <div class="row">



                <div class="well_01">
                    <div class="well_02">
                        <div class="well_03">
                            <div class="col-lg-8 col-md-7 col-sm-12">
                                <div class="logo_img">
                                    <img class="img-responsive center-block" src="images/N_Screens_Formula.png" alt="formula" />
                                </div>
                                <div class="logo_login">
                                    N Screens
                                </div>

                            </div>

                            <!-- Login Page Code Starts Here -->

                            <div class="col-lg-4 col-md-5 col-sm-12 user-form form-active text-center form-open">
                                <h1 class="form-heading">Log in to your account</h1>
                                <%--<a href="#" class="btn btn-login_1 btn btn-login w-facebook"><i class="fa fa-facebook"></i>Facebook</a>
                                <a href="#" class="btn btn-login_1 btn btn-login w-twitter"><i class="fa fa-twitter"></i>Twitter</a>
                                <p class="or-login">or</p>--%>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>

                                    <i class="fa fa-envelope input-icon"></i>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    <i class=" fa fa-key input-icon"></i>
                                </div>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-login text-uppercase" Text="Login" OnClick="btnLogin_Click" OnClientClick="Loader();" />
                                <%--<a id="imgloader" runat="server" href="#" class="btn btn-default"><asp:Image id="img" runat="server" ImageUrl="~/images/loader.gif" /></a>--%>
                                <p class="forgot-password">
                                    Forgot your password? <a href="#" data-toggle="modal" data-target="#myModal">Click here</a>
                                </p>
                                <p class="no-account">
                                    Don't have an account yet? <a id="signUp-frm-button" href="#" data-toggle="modal" data-target="#myModalUser">Sign Up</a>
                                </p>
                            </div>

                            <!-- Login Page Code Ends Here -->
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnSubmit">
                        <asp:UpdatePanel ID="uplUpdate" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="101">
                                        &times;</button>
                                    <h4 id="myModalLabel" class="modal-title">Forgot Password</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lblForgot_Email" runat="server" Text="Username :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6 has-error">
                                                <asp:TextBox ID="txtUsernameForforgetpass" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-offset-4 col-sm-8">
                                                <span class="help-block"><small>Please Enter USERNAME</small></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAccountClose_ProductList" runat="server" Text="Close" CssClass="btn btn-default"
                                        data-dismiss="modal" aria-hidden="true" TabIndex="105" />
                                    <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btn btn-primary"
                                        OnClick="btnSubmit_Click" TabIndex="104" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div id="myModalUser" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="Button2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="101">
                                        &times;</button>
                                    <h4 id="H1" class="modal-title">User Registration</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Name :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Mobile Number :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return isNumber(event)"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Email Id :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEmailId_TextChanged"
                                                    TabIndex="102"/>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblGender" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:RadioButton ID="rdbMale" runat="server" CssClass="radio radio-inline" Checked="true" GroupName="Gender" Text="Male" />
                                                <asp:RadioButton ID="rdbFemale" runat="server" CssClass="radio radio-inline" GroupName="Gender" Text="Female" />
                                                <asp:RadioButton ID="rdbOthers" runat="server" CssClass="radio radio-inline" GroupName="Gender" Text="Others" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDateofBirth" runat="server" Text="Date of Birth :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <%--<asp:TextBox ID="txtDateofBirth" runat="server" TabIndex="102" CssClass="form-control" placeholder="MM/DD/YYYY" />--%>
                                                <div class="btn-group" role="group" aria-label="...">
                                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                        <asp:ListItem Value="0">Day</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                         <asp:ListItem Value="17">17</asp:ListItem>
                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Style="width: 100px; height: 34px; display: inline-block;">
                                                        <asp:ListItem Value="0">Month</asp:ListItem>
                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                        <asp:ListItem Value="0">Year</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<ajax:CalendarExtender ID="ccl" runat="server" TargetControlID="txtDateofBirth"></ajax:CalendarExtender>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Text="State :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Pincode :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label ID="lblImage" runat="server" Text="Image :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="fupImage" runat="server" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                     <asp:Button ID="Button2" runat="server" Text="Register" CssClass="btn btn-primary"
                                        OnClick="btnRegister_Click" TabIndex="104" />
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default"
                                        data-dismiss="modal" aria-hidden="true" TabIndex="105" />
                                </div>
                                <div class="social-auth-links text-center hide">
                                    <p>- OR -</p>
                                    <div id="auth-status">
                                        <div class="fb-login-button" autologoutlink="true" scope="email,user_checkins,user_birthday">Sign up using Facebook</div>
                                        <div id="auth-loggedin" class="hide" style="display: none">
                                            Name: <b><span id="auth-displayname"></span></b>(<a href="#" id="auth-logoutlink">logout</a>)<br />
                                            Email: <b><span id="Email"></span></b>
                                            <br />
                                            Profile Image:
                    <img id="profileImg" />
                                        </div>
                                    </div>
                                    <br />
                                    <div id="gConnect" class="button">
                                        <button class="g-signin"
                                            data-scope="email"
                                            data-clientid="87129835825-ej8e7d4avtumoscft9n8f4t3en59v53r.apps.googleusercontent.com"
                                            data-callback="onSignInCallback"
                                            data-theme="dark"
                                            data-cookiepolicy="single_host_origin">
                                        </button>
                                        <!-- Textarea for outputting data -->

                                    </div>
                                    <%--<a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i>Sign up using Google+</a>--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Button2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <%--<div id="myModalUser" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSubmit">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="101">
                                        &times;</button>
                                    <h4 id="H1" class="modal-title">User Registration</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Name :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Mobile Number :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="Email Id :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Text="State :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" Text="Address :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                    TabIndex="102" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-default"
                                        data-dismiss="modal" aria-hidden="true" TabIndex="105" />
                                    <asp:Button ID="Button4" runat="server" Text="Register" CssClass="btn btn-primary"
                                        OnClick="btnRegister_Click" TabIndex="104" />
                                </div>
                                <div class="social-auth-links text-center">
                                    <p>- OR -</p>
                                    <div id="auth-status">
                                        <div class="fb-login-button" autologoutlink="true" scope="email,user_checkins,user_birthday">Sign up using Facebook</div>
                                        <div id="auth-loggedin" class="hide" style="display: none">
                                            Name: <b><span id="auth-displayname"></span></b>(<a href="#" id="auth-logoutlink">logout</a>)<br />
                                            Email: <b><span id="Email"></span></b>
                                            <br />
                                            Profile Image:
                    <img id="profileImg" />
                                        </div>
                                    </div>
                                    <br />
                                    <div id="gConnect" class="button">
                                        <button class="g-signin"
                                            data-scope="email"
                                            data-clientid="87129835825-ej8e7d4avtumoscft9n8f4t3en59v53r.apps.googleusercontent.com"
                                            data-callback="onSignInCallback"
                                            data-theme="dark"
                                            data-cookiepolicy="single_host_origin">
                                        </button>
                                        <!-- Textarea for outputting data -->

                                    </div>
                                    <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i>Sign up using Google+</a>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>--%>
        <script>
            /**
             * Handler for the signin callback triggered after the user selects an account.
             */
            function onSignInCallback(resp) {
                gapi.client.load('plus', 'v1', apiClientLoaded);
            }

            /**
             * Sets up an API call after the Google API client loads.
             */
            function apiClientLoaded() {
                gapi.client.plus.people.get({ userId: 'me' }).execute(handleEmailResponse);
            }

            /**
             * Response callback for when the API client receives a response.
             *
             * @param resp The API response object with the user email and profile information.
             */
            function handleEmailResponse(resp) {
                var primaryEmail;
                console.log(resp);

                //document.getElementById('Email').innerHTML = me.age_range;
                //document.getElementById('profileImg').src = uid;
                for (var i = 0; i < resp.emails.length; i++) {
                    if (resp.emails[i].type === 'account') primaryEmail = resp.emails[i].value;

                    document.getElementById('<%= txtEmailId.ClientID %>').value = resp.emails[i].value;
                    document.getElementById('<%= txtName.ClientID %>').value = resp.name.familyName + ' ' + resp.name.givenName;
                }
            }

        </script>
        <!-- Scripts -->
        <script src="js/wow.min.js"></script>
        <%--<script src="js/jquery-1.12.3.min.js"></script>--%>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/css-menu.js"></script>
        <script src="js/jquery.validate.js"></script>
        <script src="js/owl.carousel.min.js"></script>
        <script src="js/custom.js"></script>


    </form>
</body>
</html>
