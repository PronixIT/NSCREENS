<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default9.aspx.cs" Inherits="Admin_Default9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
    <asp:ListView ID="lstRecentVideos" runat="server">
        <ItemTemplate>
            <div class="form-group">
            <div class="blog-list-container listing-container" style="display: inline;">
                <div class="col-sm-12" style="border: solid 1px;border-color: #bcc1c0;">
                    <div class="col-sm-2">
                        <div class="vid-img">
                            <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="Add image" style="height: 150px; width: 142.5px;">
                            <asp:LinkButton class="play-icon play-small-icon" ID="play" runat="server" OnClick="play_Click">
                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                <asp:Label ID="lblURLPlay" runat="server" Visible="false" Text='<%#Eval("shortfilm") %>'></asp:Label>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-sm-10">
                        <div class="blog-text">
                            <h1 class="sm-heading">
                                <asp:LinkButton ID="lnkplay" runat="server" OnClick="play_Click">
                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                </asp:LinkButton>
                            </h1>
                            <p>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                            </p>

                        </div>
                    </div>
                </div>
            </div>
                </div>
        </ItemTemplate>
    </asp:ListView>
    </div>

    <%--<div class="vid-container">
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-1.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-2.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-3.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-4.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-5.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-6.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-7.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>
        <div class="blog-list-container listing-container">
            <div class="col-md-5 col-sm-6">
                <div class="vid-img">
                    <img class="img-responsive" src="images/blog-img-8.jpg" alt="video image">
                </div>
            </div>
            <div class="col-md-7 col-sm-6">
                <div class="blog-vid-info">
                    <p class="blog-vid-auth-name">
                        <span class="by-in">By</span>
                        <a href="#">Jhon Doe</a>
                        <span>/</span>
                        <span class="by-in">In</span>
                        <a href="#">Fantasy</a>
                    </p>
                    <p class="blog-vid-info-text text-right">
                        <span>4 month ago</span>
                        <span>/</span>
                        <span>12 <i class="fa fa-comments"></i>
                        </span>
                        <span>/</span>
                        <span>410 <i class="fa fa-eye"></i>
                        </span>
                    </p>
                </div>
                <div class="blog-text">
                    <h1 class="sm-heading">
                        <a href="blog-detail.html">Vestibulum quis leo fringilla, congue ex eget
                        </a>
                    </h1>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Sit voluptas quidem officia architecto eveniet ipsum cum quas error, autem ex odit tempora minima quisquam quasi vero illo optio quaerat veniam.
                    </p>
                    <a href="blog-detail.html" class="btn btn-blog">Read More</a>
                </div>
            </div>
        </div>


        <!--  pagination -->

        <div class="pagination-container text-center">
            <ul class="pagination">
                <li>
                    <a href="#">«</a>
                </li>
                <li class="active">
                    <a href="#">1</a>
                    <div class="pagination-hvr"></div>
                </li>
                <li>
                    <a href="#">2</a>
                    <div class="pagination-hvr"></div>
                </li>
                <li>
                    <a href="#">3</a>
                    <div class="pagination-hvr"></div>
                </li>
                <li>
                    <a href="#">4</a>
                    <div class="pagination-hvr"></div>
                </li>
                <li>
                    <a href="#">5</a>
                    <div class="pagination-hvr"></div>
                </li>
                <li>
                    <a href="#">»</a>
                </li>
            </ul>
        </div>

        <!-- pagination -->

    </div>--%>
</asp:Content>

