<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmSingle1.aspx.cs" Inherits="Admin_video_detail" EnableEventValidation="false" %>

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
            { <%=GetData() %> }
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
    <asp:UpdatePanel ID="upl" runat="server">
        <ContentTemplate>
            <div class="col-md-6 col-sm-12 md-grid">
                <div class="vid-img-holder wow pulse" data-wow-duration="1s">
                    <div class="top-shadow hidden">
                        <span>4 Months ago </span>
                        <span>From <a href="#"><i class="fa fa-youtube-play"></i></a></span>
                        <span><i class="fa fa-eye"></i>4481</span>
                    </div>
                    <asp:LinkButton ID="lnk" runat="server" OnClick="btnPlay_Click">
                        <%--<img id="img" runat="server" class="img-responsive" src="../images/main-vid-image-md-1.jpg" alt="video_thumb">--%>
                        <%--<img class="img-responsive hidden-md hidden-lg" src="../images/main-vid-image-smmd-1.jpg" alt="video_thumb">--%>
                        <asp:TextBox ID="lblNextVideo" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:Label ID="lblAddId" runat="server" Visible="false"></asp:Label>
                        <div id="add" runat="server"></div>
                        <span class="play-icon">
                            <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='../images/play-button.png'">
                        </span>
                    </asp:LinkButton>
                    <h3 class="vid-author hidden">
                        <span>By <a href="#" title="Posts by admin" rel="author">Admin</a>
                        </span>
                        <a href="video-detail.html">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label></a>
                    </h3>
                    <div class="bottom-shadow hidden"></div>
                    <div class="overlay-div hidden"></div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-md-3 col-sm-4">
        <%--<asp:Button ID="btnPlay" runat="server" Text="Play" CssClass="btn btn-danger" OnClick="btnPlay_Click" />--%>
        <div class="sidebar">
            <div class="sidebar-vid most-liked">
                <h1 class="sidebar-heading">Most Liked</h1>
                <div class="media">
                    <div class="media-left">
                        <img src="../images/most-liked-img-s1.jpg" alt="video">
                    </div>
                    <div class="media-body">
                        <h1><a href="video-detail.html">Journey</a></h1>
                        <p>
                            <span><i class="fa fa-comment"></i>10</span>
                            <span><i class="fa fa-eye"></i>534</span>
                        </p>
                    </div>
                </div>
                <div class="media">
                    <div class="media-left">
                        <img src="../images/most-liked-img-s2.jpg" alt="video">
                    </div>
                    <div class="media-body">
                        <h1><a href="video-detail.html">Magic</a></h1>
                        <p>
                            <span><i class="fa fa-comment"></i>10</span>
                            <span><i class="fa fa-eye"></i>534</span>
                        </p>
                    </div>
                </div>
                <div class="media">
                    <div class="media-left">
                        <img src="../images/most-liked-img-s3.jpg" alt="video">
                    </div>
                    <div class="media-body">
                        <h1><a href="video-detail.html">Runner</a></h1>
                        <p>
                            <span><i class="fa fa-comment"></i>10</span>
                            <span><i class="fa fa-eye"></i>534</span>
                        </p>
                    </div>
                </div>
                <div class="media">
                    <div class="media-left">
                        <img src="../images/most-liked-img-s4.jpg" alt="video">
                    </div>
                    <div class="media-body">
                        <h1><a href="video-detail.html">Fantasy</a></h1>
                        <p>
                            <span><i class="fa fa-comment"></i>10</span>
                            <span><i class="fa fa-eye"></i>534</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

