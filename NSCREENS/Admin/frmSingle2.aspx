<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmSingle2.aspx.cs" Inherits="Admin_frmSingle" EnableEventValidation="true" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function goFullscreen(id) {
            var element = document.getElementById(id);
            if (element.mozRequestFullScreen) {
                element.mozRequestFullScreen();
            } else if (element.webkitRequestFullScreen) {
                element.webkitRequestFullScreen();
            }
        }

        function Play_Pause() {
            //var myVideo = document.getElementById("Video");
            //if (myVideo.paused)
            //    myVideo.play();
            //else
            //    myVideo.pause();

        }

        function Display() {
            { <%=AddBudget() %> }
            document.getElementById('<%= add.ClientID %>').innerHTML = "";
            document.getElementById('<%= add.ClientID %>').innerHTML = "<video id='Video' height='376px' width='100%' controls onclick='Play_Pause()'><source src='" + document.getElementById('<%= lblNextVideo.ClientID %>').value + "' type='video/mp4'>";
            var myVideo = document.getElementById("Video");
            myVideo.play();
        }

        function Add_Budget() {
            alert("Video Completed");
            { <%=GetData() %> };
            
            <%--document.getElementById('<%= add.ClientID %>').innerHTML = "";
            document.getElementById('<%= add.ClientID %>').innerHTML = "<video id='Video' height='376px' width='100%' controls onclick='Play_Pause()'><source src='" + document.getElementById('<%= lblNextVideo.ClientID %>').value + "' type='video/mp4'>";
            var myVideo = document.getElementById("Video");
            myVideo.play();--%>
        }

        function Add_Likes() {
            alert('start');
            { <%=GetLikes() %> };
            alert('end');
        }
    </script>

    <script>
        function Play_Pause123() {
            var myVideo = document.getElementById("Video");
            if (myVideo.paused)
                myVideo.play();
            else
                myVideo.pause();
        }
    </script>
    <div id="page-bar">
        <div class="container">
            <div class="row">
                <div class="col-md-9 col-sm-8 col-xs-12">
                    <div class="page-title">
                        <h1 class="text-uppercase">Video Detail</h1>
                    </div>
                </div>
                <div class="col-md-3 col-sm-4 col-xs-12 text-right">
                    <ul class="breadcrumb">
                        <li>
                            <a href="index.html"><i class="fi ion-ios-home"></i>Home</a>
                        </li>
                        <li class="active">Video Detail</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!-- Inner page Bar -->

    <!-- Secondary Section -->

    <div id="video-detail">
        <div class="container">
            <div class="row">
                <div class="col-md-9 col-sm-8">
                    <div class="vid-detail-container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="vid-player">
                                    <div class="embed-responsive embed-responsive-16by9">
                                        <asp:TextBox ID="lblNextVideo" runat="server" CssClass="hide"></asp:TextBox>
                                        <asp:Label ID="lblAddId" runat="server" Visible="false"></asp:Label>
                                        <div id="add" runat="server" visible="true"></div>
                                        <%--<video id='Video' height='376px' width='100%' autoplay runat="server" onended='Add_Budget()'><source id='add123' src="../Videos/Shortfilm_1.mp4" type='video/mp4'></video>--%>
                                        <span class="play-icon">
                                            <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='../images/play-button.png'">
                                        </span>
                                    </div>
                                </div>
                                <div class="vid-text">
                                    <p><span>By</span> <a href="#">Admin</a></p>
                                    <h1>
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
                                </div>
                                <div class="video-info-bar">
                                    <ul class="list-inline list-unstyled info-ul">
                                        <li><i class="fa fa-calendar"></i>
                                            <asp:Label ID="lblPublished" runat="server"></asp:Label></li>
                                        <li><i class="fa fa-eye"></i>
                                            <asp:Label ID="lblViews" runat="server"></asp:Label></li>
                                        <%--<li><i class="fa fa-thumbs-up"></i>457689</li>--%>
                                        <li class="pull-right sharing-drop">
                                            <button class="btn"></button>
                                        </li>
                                    </ul>
                                    <ul class="list-unstyled list-inline pull-right text-right sharing-bar">
                                        <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                        <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                        <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                                        <li><a href="#"><i class="fa fa-instagram"></i></a></li>
                                        <li><a href="#"><i class="fa fa-dribbble"></i></a></li>
                                    </ul>
                                </div>
                                <div class="video-detail-text">
                                    <p>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div class="related-item">
                                    <div class="vid-heading overflow-hidden">
                                        <span class="wow fadeInUp" data-wow-duration="0.8s">Related Videos
                                        </span>
                                        <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="vid-container">
                                            <asp:ListView ID="lstRelatedVideos" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-md-4 col-sm-6">
                                                        <div class="latest-vid-img-container">
                                                            <div class="vid-img">
                                                                <img id="imgRelatedVideos" runat="server" class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image">
                                                                <a id="ashortfilm" runat="server" href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                                    <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                                                </a>
                                                                &nbsp;&nbsp;<div class="overlay-div"></div>
                                                            </div>
                                                            <div class="vid-text">
                                                                <%-- <p><span>By</span> <a href="#">
                                                                    <asp:Label ID="lblRelatedHero" runat="server" Text='<%#Eval("Hero") %>'></asp:Label></a></p>--%>
                                                                <h1><a id="ashortfilm1" runat="server" href='<%#Eval("shortfilm") %>'>
                                                                    <asp:Label ID="lblRelatedTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                                                <p class="vid-info-text">
                                                                    <span>
                                                                        <asp:Label ID="lblReatedDuration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label></span>
                                                                    <span>&#47;</span>
                                                                    <%-- <span>From <a href="#"><i class="fa fa-youtube-play"></i></a>
                                                                    </span>
                                                                    <span>&#47;</span>--%>
                                                                    <span>
                                                                        <asp:Label ID="lblRelatedViews" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>
                                                                        views</span>
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="fb-root"></div>
                <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_IN/sdk.js#xfbml=1&version=v2.8&appId=566086956918274";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>

                <div id="id" runat="server" class="fb-like" data-layout="standard" data-action="like" data-size="small" data-show-faces="true" data-share="true"></div>
                <div class="col-md-3 col-sm-4">
                    <div class="sidebar">
                        <div class="sidebar-vid most-liked">
                            <h1 class="sidebar-heading">Most Liked</h1>
                            <asp:ListView ID="lstMostLiked" runat="server">
                                <ItemTemplate>
                                    <div class="media">
                                        <div class="media-left">
                                            <img id="imgDis" runat="server" src='<%#Eval("Short_film_Image") %>' alt="video" style="width: 90px; height: 60px;">
                                        </div>
                                        <div class="media-body">
                                            <h1><a id="sss" runat="server" href='<%#Eval("shortfilm") %>'>
                                                <asp:Label ID="lblDisTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                            <p>
                                                <%--<span><i class="fa fa-comment"></i>10</span>--%>
                                                <span><i class="fa fa-eye"></i>&nbsp;<asp:Label ID="lblView" Text='<%#Eval("Visits") %>' runat="server"></asp:Label></span>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="sidebar-vid most-viewd">
                            <h1 class="sidebar-heading">Most Viewed</h1>
                            <asp:ListView ID="lstMost" runat="server">
                                <ItemTemplate>
                                    <div class="most-viewd-container">
                                        <div class="most-viewd-img">
                                            <img id="imgDis" runat="server" src='<%#Eval("Short_film_Image") %>' class="img-responsive" alt="video">
                                        </div>
                                        <div class="most-viewd-text">
                                            <h1><a id="sss" runat="server" href='<%#Eval("shortfilm") %>'>
                                                <asp:Label ID="lblDisTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                            <p>
                                                <%--<span><i class="fa fa-comment"></i>10</span>--%>
                                                <span><i class="fa fa-eye"></i>&nbsp;<asp:Label ID="lblView" Text='<%#Eval("Visits") %>' runat="server"></asp:Label></span>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <%--<div class="tags">
                            <h1 class="sidebar-heading">Tags</h1>
                            <ul class="list-inline list-unstyled">
                                <li><a href="#">3D</a></li>
                                <li><a href="#">Animals &amp; Birds</a></li>
                                <li><a href="#">HD</a></li>
                                <li><a href="#">Horror</a></li>
                                <li><a href="#">Art</a></li>
                                <li><a href="#">Self</a></li>
                                <li><a href="#">HD Songs</a></li>
                                <li><a href="#">Comedy</a></li>
                            </ul>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

