<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="PlayVideo.aspx.cs" Inherits="Admin_PlayVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
            <div class="PlayVideo text-center video_background">

                <div class="container">
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/a5FvgdI6h-c" frameborder="0" allowfullscreen></iframe>
                    </div>
                </div>
            </div>

            <div id="video-detail">
                <div class="container">
                    <div class="row">
                        <div class="col-md-9 col-sm-8">
                            <div class="vid-detail-container">
                                <div class="row">

                                    <div class="col-sm-12">

                                        <div class="vid-text">

                                            <div class="page-title">
                                                <h1 class="text-uppercase">Video Detail</h1>
                                            </div>

                                            <p><span>By</span> <a href="http://localhost:4958/Admin/frmSingle.aspx?shortfilm=13#">Admin</a></p>
                                            <h1>
                                                <span id="ContentPlaceHolder1_lblTitle">sat</span>
                                            </h1>
                                        </div>
                                        <div class="video-info-bar">
                                            <ul class="list-inline list-unstyled">
                                                <li>
                                                    <i class="fa fa-calendar"></i>
                                                    <span id="ContentPlaceHolder1_lblPublished">May  2 2017 </span>
                                                </li>
                                                <li>
                                                    <i class="fa fa-eye"></i>
                                                    <span id="ContentPlaceHolder1_lblViews">2</span>
                                                </li>
                                                <li>
                                                    <input type="submit" name="ctl00$ContentPlaceHolder1$btnTrailer" value="Trailer" id="ContentPlaceHolder1_btnTrailer" class="btn btn-primary"></li>
                                                <li>
                                                    <input type="submit" name="ctl00$ContentPlaceHolder1$btnShortFilm" value="Short Film" id="ContentPlaceHolder1_btnShortFilm" class="btn btn-primary"></li>
                                                <li class="pull-right">
                                                    <a id="ContentPlaceHolder1_a">

                                                        <span id="ContentPlaceHolder1_lbl">Google+</span>
                                                    </a>
                                                </li>


                                                <li class="pull-right">
                                                    <div id="fb-root" class=" fb_reset">
                                                        <div style="position: absolute; top: -10000px; height: 0px; width: 0px;">
                                                            <div></div>
                                                        </div>
                                                        <div style="position: absolute; top: -10000px; height: 0px; width: 0px;">
                                                            <div>
                                                                <iframe name="fb_xdm_frame_http" frameborder="0" allowtransparency="true" allowfullscreen="true" scrolling="no" id="fb_xdm_frame_http" aria-hidden="true" title="Facebook Cross Domain Communication Frame" tabindex="-1" src="./NSCREENS_files/JtmcTFxyLye.html" style="border: none;"></iframe>
                                                                <iframe name="fb_xdm_frame_https" frameborder="0" allowtransparency="true" allowfullscreen="true" scrolling="no" id="fb_xdm_frame_https" aria-hidden="true" title="Facebook Cross Domain Communication Frame" tabindex="-1" src="./NSCREENS_files/JtmcTFxyLye(1).html" style="border: none;"></iframe>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <script>
                                                        (function (d, s, id) {
                                                            var js, fjs = d.getElementsByTagName(s)[0];
                                                            if (d.getElementById(id)) return;
                                                            js = d.createElement(s); js.id = id;
                                                            js.src = "//connect.facebook.net/en_IN/sdk.js#xfbml=1&version=v2.8&appId=566086956918274";
                                                            fjs.parentNode.insertBefore(js, fjs);
                                                        }(document, 'script', 'facebook-jssdk'));</script>


                                                    <div id="ContentPlaceHolder1_id" class="fb-share-button fb_iframe_widget" data-href="https://developers.facebook.com/docs/plugins/" data-layout="button" data-size="large" data-mobile-iframe="true" fb-xfbml-state="rendered" fb-iframe-plugin-query="app_id=566086956918274&amp;container_width=54&amp;href=https%3A%2F%2Fdevelopers.facebook.com%2Fdocs%2Fplugins%2F&amp;layout=button&amp;locale=en_US&amp;mobile_iframe=true&amp;sdk=joey&amp;size=large"><span style="vertical-align: bottom; width: 72px; height: 28px;">
                                                        <iframe name="f21848df9f29f04" width="1000px" height="1000px" frameborder="0" allowtransparency="true" allowfullscreen="true" scrolling="no" title="fb:share_button Facebook Social Plugin" src="./NSCREENS_files/share_button.html" style="border: none; visibility: visible; width: 72px; height: 28px;" class=""></iframe>
                                                    </span></div>
                                                </li>


                                            </ul>

                                        </div>
                                        <div class="video-detail-text">
                                            <p>
                                                <span id="ContentPlaceHolder1_lblDescription">None</span>
                                            </p>
                                        </div>
                                        <div class="related-item">
                                            <div class="vid-heading overflow-hidden">
                                                <span class="wow fadeInUp" data-wow-duration="0.8s" style="visibility: visible; animation-duration: 0.8s; animation-name: fadeInUp;">Related Videos
                                                </span>
                                                <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s" style="visibility: visible; animation-duration: 0.8s; animation-name: zoomIn;">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="vid-container">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-4">
                            <div class="sidebar">
                                <div class="sidebar-vid most-liked">
                                    <h1 class="sidebar-heading">Most Liked</h1>

                                    <div class="media">
                                        <div class="media-left">
                                            <img src="./NSCREENS_files/Shortfilm_14.jpg" id="ContentPlaceHolder1_lstMostLiked_imgDis_0" alt="video" style="width: 90px; height: 60px;">
                                        </div>
                                        <div class="media-body">
                                            <h1>
                                                <a href="http://localhost:4958/Admin/frmSingle.aspx?shortfilm=14" id="ContentPlaceHolder1_lstMostLiked_sss_0">
                                                    <span id="ContentPlaceHolder1_lstMostLiked_lblDisTitle_0">Okade</span>
                                                </a>
                                            </h1>
                                            <p>

                                                <span><i class="fa fa-eye"></i>&nbsp;<span id="ContentPlaceHolder1_lstMostLiked_lblView_0">2</span></span>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="media">
                                        <div class="media-left">
                                            <img src="./NSCREENS_files/Shortfilm_13.jpg" id="ContentPlaceHolder1_lstMostLiked_imgDis_1" alt="video" style="width: 90px; height: 60px;">
                                        </div>
                                        <div class="media-body">
                                            <h1>
                                                <a href="http://localhost:4958/Admin/frmSingle.aspx?shortfilm=13" id="ContentPlaceHolder1_lstMostLiked_sss_1">
                                                    <span id="ContentPlaceHolder1_lstMostLiked_lblDisTitle_1">sat</span>
                                                </a>
                                            </h1>
                                            <p>

                                                <span><i class="fa fa-eye"></i>&nbsp;<span id="ContentPlaceHolder1_lstMostLiked_lblView_1">2</span></span>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="media">
                                        <div class="media-left">
                                            <img src="./NSCREENS_files/Shortfilm_12.jpg" id="ContentPlaceHolder1_lstMostLiked_imgDis_2" alt="video" style="width: 90px; height: 60px;">
                                        </div>
                                        <div class="media-body">
                                            <h1>
                                                <a href="http://localhost:4958/Admin/frmSingle.aspx?shortfilm=12" id="ContentPlaceHolder1_lstMostLiked_sss_2">
                                                    <span id="ContentPlaceHolder1_lstMostLiked_lblDisTitle_2">The Last</span>
                                                </a>
                                            </h1>
                                            <p>

                                                <span><i class="fa fa-eye"></i>&nbsp;<span id="ContentPlaceHolder1_lstMostLiked_lblView_2">11</span></span>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="media">
                                        <div class="media-left">
                                            <img src="./NSCREENS_files/Shortfilm_11.jpg" id="ContentPlaceHolder1_lstMostLiked_imgDis_3" alt="video" style="width: 90px; height: 60px;">
                                        </div>
                                        <div class="media-body">
                                            <h1>
                                                <a href="http://localhost:4958/Admin/frmSingle.aspx?shortfilm=11" id="ContentPlaceHolder1_lstMostLiked_sss_3">
                                                    <span id="ContentPlaceHolder1_lstMostLiked_lblDisTitle_3">ab</span>
                                                </a>
                                            </h1>
                                            <p>

                                                <span><i class="fa fa-eye"></i>&nbsp;<span id="ContentPlaceHolder1_lstMostLiked_lblView_3">13</span></span>
                                            </p>
                                        </div>
                                    </div>

                                </div>
                                <div class="sidebar-vid most-viewd">
                                    <h1 class="sidebar-heading">Most Viewed</h1>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        
            
        </div>
    
</asp:Content>

