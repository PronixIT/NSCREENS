<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmUserProfile.aspx.cs" Inherits="Admin_frmHome" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">
                        <asp:Image ID="img" runat="server" Width="1140px" Height="200px" ImageUrl="~/Video_Images/Advatizments_1.jpg" />
                    </span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-4">
                            <asp:Label ID="lblProductionName" runat="server" Text="Production Name"></asp:Label>
                            <br />
                            <asp:Label ID="lblDescription" runat="server" Text="Production Name"></asp:Label>
                        </div>
                        <div class="col-sm-8">
                            <br />
                            <asp:Button ID="btnFollow" runat="server" Text="Follow" CssClass="btn btn-info pull-right" />
                        </div>
                    </div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1">
                            <ItemTemplate>
                                <div class="col-md-4 col-sm-6">
                                    <div class="latest-vid-img-container">
                                        <div class="vid-img">
                                            <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image">
                                            <a href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                            </a>
                                            <div class="overlay-div"></div>
                                        </div>
                                        <div class="vid-text">
                                            <%--<p>
                                                <span>By</span> <a href="#">
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("Hero") %>'></asp:Label></a>
                                            </p>--%>
                                            <h1><a href='<%#Eval("shortfilm") %>'>
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                            <p class="vid-info-text">
                                                <span>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Duration") %>'></asp:Label></span>
                                                <span>&#47;</span>
                                                <span>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>
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
</asp:Content>

