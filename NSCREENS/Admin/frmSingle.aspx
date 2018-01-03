<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmSingle.aspx.cs" Inherits="Admin_frmSingle" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function divPopupView() {
            $('#myModalView').modal('show');

            $('#myModalView').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopupMore() {
            $('#myModalMore').modal('show');

            $('#myModalMore').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
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
            var myVideo = document.getElementById("Video");
            if (myVideo.paused)
                myVideo.play();
            else
                myVideo.pause();

        }

        function OnSuccess(response, userContext, methodName) {
            document.getElementById('<%=(Master.FindControl("lblAmount")).ClientID%>').innerText = response;
        }

    </script>
    <%-- <script src="https://f.vimeocdn.com/js/froogaloop2.min.js"></script>
    <script src="../js/jquery-1.12.3.min.js"></script>--%>

    <script>

        $("#secondary-section .container").removeClass("container");
        $('#secondary-section').removeAttr("id");
    </script>

    <script>


        $(function () {
            var player = $('iframe');
            var playerOrigin = '*';
            var status = $('.status');

            // Listen for messages from the player
            if (window.addEventListener) {
                window.addEventListener('message', onMessageReceived, false);
            }
            else {
                window.attachEvent('onmessage', onMessageReceived, false);
            }

            // Handle messages received from the player
            function onMessageReceived(event) {
                // Handle messages from the vimeo player only
                if (!(/^https?:\/\/player.vimeo.com/).test(event.origin)) {
                    return false;
                }

                if (playerOrigin === '*') {
                    playerOrigin = event.origin;
                }

                var data = JSON.parse(event.data);

                switch (data.event) {
                    case 'ready':
                        onReady();
                        break;

                    case 'playProgress':
                        onPlayProgress(data.data);
                        break;

                    case 'pause':
                        onPause();
                        break;

                    case 'finish':

                        //alert('Short Film Start now..');

                        onFinish();
                        break;
                }
            }

            // Call the API when a button is pressed
            $('button').on('click', function () {
                post($(this).text().toLowerCase());
            });

            // Helper function for sending a message to the player
            function post(action, value) {
                var data = {
                    method: action
                };

                if (value) {
                    data.value = value;
                }

                var message = JSON.stringify(data);
                player[0].contentWindow.postMessage(message, playerOrigin);
            }

            function onReady() {
                status.text('ready');

                post('addEventListener', 'pause');
                post('addEventListener', 'finish');
                post('addEventListener', 'playProgress');
            }

            function onPause() {
                status.text('paused');
            }

            function onFinish() {
                if (document.getElementById('<%= txtNextPN.ClientID %>').value == 'AddB') {
                    var ulr = window.location.href;
                    var str = ulr.split('Admin');
                    PageMethods.GetCurrentTime(document.getElementById("<%=txtUserId.ClientID%>").value, 0, '', OnSuccess);
                }
                else if (document.getElementById('<%= txtNextPN.ClientID %>').value == 'No') {
                    pnotifySuccess('Film', 'Rs.2 has been successfully added to the wallet and deducted.Enjoy the film', 'success');
                    PageMethods.GetAdd(document.getElementById("<%=txtUserId.ClientID%>").value, document.getElementById("<%=lblAddId.ClientID%>").value, '', OnSuccess);

                    document.getElementById('<%= add.ClientID %>').innerHTML = "";
                    document.getElementById('<%= add.ClientID %>').innerHTML = "<iframe id='player1' src='" + document.getElementById('<%= lblNextVideo.ClientID %>').value + "?autoplay=1&api=1&player_id=player1' style='width: 1140px; height: 550px;' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>";
                    var myVideo = document.getElementById("Video");

                    var ipaddress='null';

                    $.get("http://ipinfo.io", function (response) {
                        ipaddress = response.ip;
                    }, "jsonp");

                    PageMethods.GetCurrentTimeShortFilm(document.getElementById("<%=txtVideoUserId.ClientID%>").value, document.getElementById("<%=txtUserId.ClientID%>").value, document.getElementById("<%=txtShortFilmId.ClientID%>").value, ipaddress, OnSuccessEarning);

                    myVideo.play();

                }
                else {

                }

        }

            function OnSuccessEarning(response, userContext, methodName) {
                document.getElementById('<%=(Master.FindControl("lblEarningMoney")).ClientID%>').innerText = response;
            }

            function onPlayProgress(data) {
                status.text(data.seconds + 's played');
            }
        });
    </script>
    <style>
        button {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            cursor: pointer;
            font-weight: 700;
            font-size: 13px;
            padding: 8px 18px 10px;
            line-height: 1;
            color: #fff;
            background: #345;
            border: 0;
            border-radius: 4px;
            margin-left: 0.75em;
        }

        p {
            display: inline-block;
            margin-left: 10px;
        }

        iframe {
            width: 1140px;
            height: 550px;
        }
    </style>

    <!-- Inner page Bar -->

    <div class="PlayVideo text-center video_background">

        <div class="container">
            <div class="embed-responsive embed-responsive-16by9">
                <asp:UpdatePanel ID="uplplay" runat="server">
                    <ContentTemplate>
                        <%--<iframe class="embed-responsive-item" src="https://www.youtube.com/embed/a5FvgdI6h-c" frameborder="0" allowfullscreen></iframe>--%>
                        <asp:TextBox ID="txtUserId" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:TextBox ID="lblNextVideo" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:TextBox ID="lblAddId" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:TextBox ID="txtNextPN" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:TextBox ID="txtShortFilmId" runat="server" CssClass="hide"></asp:TextBox>
                        <asp:TextBox ID="txtVideoUserId" runat="server" CssClass="hide"></asp:TextBox>
                        <div id="add" runat="server" visible="true"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="video-detail">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12" style="top: -20px;">
                    <div class="vid-detail-container">
                        <div class="row">
                            <div class="video-info-bar">
                                <ul class="list-inline list-unstyled info-ul">
                                    <li><i class="fa fa-calendar"></i>
                                        <asp:Label ID="lblPublished" runat="server"></asp:Label></li>
                                    <li><i class="fa fa-eye"></i>
                                        <asp:Label ID="lblViews" runat="server"></asp:Label></li>
                                    <li>
                                        <asp:UpdatePanel ID="uplTrailer" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnTrailer" runat="server" Text="Trailer" CssClass="btn btn-primary" OnClick="btnTrailer_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </li>
                                    <li>
                                        <asp:Button ID="btnShortFilm" runat="server" Text="Play Film" CssClass="btn btn-primary" OnClick="btnPlay_Click" /></li>
                                    <li class="pull-right">
                                        <div id="gacc" runat="server">
                                            <!-- Place this tag where you want the share button to render. -->
                                            <div id="Googleplus" runat="server" class="g-plus" data-action="share" data-annotation="none" data-height="24" data-href="http://www.nscreens.com/Admin/frmSingle.aspx?shortfilm=15&userId=7"></div>

                                            <!-- Place this tag after the last share tag. -->
                                            <script type="text/javascript">
                                                (function () {
                                                    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                                                    po.src = 'https://apis.google.com/js/platform.js';
                                                    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                                                })();
                                            </script>
                                        </div>
                                        <%--<a id="a" runat="server">
                                                <asp:Label ID="lbl" runat="server" CssClass="btn btn-danger" Text="Google+"></asp:Label>
                                            </a>--%>
                                    </li>
                                    <li class="pull-right">
                                        <div id="fbacc" runat="server">
                                            <div id="fb-root"></div>
                                            <div class="copy-right"></div>
                                            <script>
                                                (function (d, s, id) {
                                                    var js, fjs = d.getElementsByTagName(s)[0];
                                                    if (d.getElementById(id)) return;
                                                    js = d.createElement(s); js.id = id;
                                                    js.src = "//connect.facebook.net/en_IN/sdk.js#xfbml=1&version=v2.8&appId=566086956918274&caption=ztest";
                                                    fjs.parentNode.insertBefore(js, fjs);
                                                }(document, 'script', 'facebook-jssdk'));

                                            </script>
                                            <div id="id" runat="server" class="fb-share-button" data-href="https://developers.facebook.com/docs/plugins/" data-layout="button" data-size="large" data-mobile-iframe="true"><a class="fb-xfbml-parse-ignore" target="_blank" href="https://www.facebook.com/sharer/sharer.php?u=https%3A%2F%2Fdevelopers.facebook.com%2Fdocs%2Fplugins%2F&amp;src=sdkpreparse">Share</a></div>
                                        </div>
                                    </li>
                                </ul>

                            </div>
                            <div class="col-sm-12">
                                <div class="vid-text">
                                    <div class="page-title">
                                        <div class="text-uppercase">
                                            <h1 style="display: inline-block">
                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>&nbsp;</h1>
                                            <h6 style="display: inline-block">
                                                <asp:Label ID="lblTag" runat="server"></asp:Label></h6>
                                        </div>
                                    </div>
                                    <p>
                                        <span>By</span> <a id="aproduction" runat="server" href="#">
                                            <asp:Label ID="lblProduction" runat="server"></asp:Label></a>
                                    </p>
                                </div>

                                <div class="video-detail-text">
                                    <p>
                                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div id="ArtistList" runat="server" class="related-item">
                                    <div class="vid-heading overflow-hidden">
                                        <span class="wow fadeInUp" data-wow-duration="0.8s">Artist List
                                        </span>
                                        <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="vid-container">
                                            <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                                                <ItemTemplate>
                                                    <div class="col-md-2 col-sm-3">
                                                        <div class="latest-vid-img-container">
                                                            <asp:UpdatePanel ID="upla" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="vid-img">
                                                                        <img class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 171px; width: 180px;">
                                                                        <a id="ashortfilm" class="play-icon play-small-icon">
                                                                            <asp:ImageButton ID="ibnThdddumbnail" runat="server" class="img-responsive play-svg svg" ImageUrl="~/images/eye.png" alt="play" CommandName="Display" />
                                                                        </a>
                                                                        <div class="overlay-div"></div>
                                                                    </div>
                                                                    <div class="vid-text">
                                                                        <h1>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server">
                                                                                <asp:Label ID="lblGridArtist" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                                <asp:Label ID="lblGridArtistId" runat="server" Text='<%#Eval("Artist_Details_Id") %>' Visible="false" />
                                                                                <asp:Label ID="lblGridGender" runat="server" Text='<%#Eval("Gender") %>' Visible="false" />
                                                                                <asp:Label ID="lblGridInterest_Areas" runat="server" Text='<%#Eval("Artist_Name") %>' Visible="false" />
                                                                                <asp:Label ID="lblGridArtistIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' Visible="false" />
                                                                            </asp:LinkButton></h1>
                                                                        <p class="vid-info-text">
                                                                            <span>
                                                                                <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label></span>
                                                                        </p>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <p>List is empty</p>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                                <div class="related-item">
                                    <div class="vid-heading overflow-hidden">
                                        <span class="wow fadeInUp" data-wow-duration="0.8s">Related Videos </span>
                                        <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="vid-container">
                                            <asp:ListView ID="lstRelatedVideos" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-md-2 col-sm-3">
                                                        <div class="latest-vid-img-container">
                                                            <div class="vid-img">
                                                                <img id="imgRelatedVideos" runat="server" class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 184px; width: 184px" />
                                                                <a id="ashortfilm" runat="server" href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                                    <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'" style="height: 184px; width: 184px">
                                                                </a>
                                                                <div class="overlay-div"></div>
                                                            </div>
                                                            <div class="vid-text">
                                                                <%-- <p><span>By</span> <a href="#">
                                                                    <asp:Label ID="lblRelatedHero" runat="server" Text='<%#Eval("Hero") %>'></asp:Label></a></p>--%>
                                                                <h1><a id="ashortfilm1" runat="server" href='<%#Eval("shortfilm") %>'>
                                                                    <asp:Label ID="lblRelatedTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                                                <p class="vid-info-text">
                                                                    <span>
                                                                        <asp:Label ID="lblReatedDuration" runat="server" Text='<%#Eval("Duration") %>'></asp:Label></span><span>&#47;</span> <%-- <span>From <a href="#"><i class="fa fa-youtube-play"></i></a>
                                                                    </span>
                                                                    <span>&#47;</span>--%><span><asp:Label ID="lblRelatedViews" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>views</span>
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
            </div>
        </div>
    </div>
    <div id="myModal" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="uplUpdate" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button><h4 id="myModalLabel" class="modal-title">Select Trailer</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="related-item">
                                        <div class="row">
                                            <div class="vid-container">
                                                <asp:ListView ID="lstTrailers" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-md-4 col-sm-6">
                                                            <div class="latest-vid-img-container">
                                                                <div class="vid-img">
                                                                    <%--<asp:ImageButton ID="img" runat="server" OnClick="img_Click" ImageUrl="~/images/images.png" AlternateText="Trailer" CommandName='<%#Eval("Trailer_Id") %>' Width="50px" Height="50px" />--%>
                                                                    <asp:ImageButton ID="img" runat="server" OnClick="img_Click" ImageUrl='<%#Eval("Image") %>' AlternateText="Trailer" CommandName='<%#Eval("Trailer_Id") %>' Width="200px" Height="200px" />
                                                                    <h6>
                                                                        <asp:Label ID="lblRelatedTitle" runat="server" Text='<%#Eval("serialnumber") %>'></asp:Label> - <asp:Label ID="Label2" runat="server" Text='<%#Eval("Language_Name") %>'></asp:Label></h6>
                                                                </div>
                                                                <div class="vid-text">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>Trailer is not available</EmptyDataTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-right"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>

    <div id="myModalMore" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button><h4 id="myModalLabel" class="modal-title">More Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="col-md-12 col-sm-12">
                                        <div class="latst-vid secondary-vid">
                                            <div class="vid-heading overflow-hidden">
                                                <span class="wow fadeInUp" data-wow-duration="0.8s">Artist List </span>
                                                <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                                            </div>
                                            <div class="row auto-clear">
                                                <div class="vid-container">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default pull-right"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="myModalView" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">More Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <div class="col-sm-7">

                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" Text="Name :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" Text="Interest Area :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblArtist" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" Text="Gender :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" Text="Location :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lbltactInformation" runat="server" Text="Contact Info :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblContactInformation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Image ID="imgPhoto" runat="server" Style="height: 171px; width: 180px;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblLanguagesDis" runat="server" Text="Languages :" CssClass="col-sm-3 control-label"></asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtLanguagesDis" runat="server" CssClass="form-control" Enabled="false" TextMode="SingleLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Description :" CssClass="col-sm-3 control-label"></asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="lblDescription1" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-footer">
                                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

