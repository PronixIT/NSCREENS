<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAllProductions.aspx.cs" Inherits="Admin_frmHome" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">Production List</span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <div class="vid-img">
                                            <img class="img-responsive" src='<%#Eval("Img") %>' alt="video image" style="height:171px;width:180px;">
                                            <%--<a target="_blank" href='#' class="play-icon play-small-icon">
                                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                            </a>--%>
                                            <div class="overlay-div"></div>
                                        </div>
                                        <div class="vid-text">
                                            <%--<p>
                                                <span>By</span> <a href="#">
                                                    <asp:Label ID="Label3" runat="server"></asp:Label></a>
                                            </p>--%>
                                            <h1><a target="_blank" href='#'>
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Channel_Name") %>'></asp:Label></a></h1>
                                            <p class="vid-info-text">
                                                <%-- <span>
                                                    <asp:Label ID="Label1" runat="server"></asp:Label></span>
                                                <span>&#47;</span>--%>
                                                <span>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
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

