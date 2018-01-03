<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmHome.aspx.cs" Inherits="Admin_frmHome" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div id="recent" runat="server" class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">Recently Released</span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <div class="vid-img">
                                            <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 200px; width: 142.5px;">
                                            <a href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" style="height: 200px; width: 142.5px;" onerror="this.src='images/play-button.png'">
                                            </a>
                                            <div class="overlay-div"></div>
                                        </div>
                                        <div class="vid-text" style="width: 142.5px;">
                                            <h1><a href='<%#Eval("shortfilm") %>'>
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                            <p class="vid-info-text">
                                               <span>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Tag") %>'></asp:Label></span>
                                                       <%-- <span>&#47;</span>
                                                        <span>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>
                                                            views</span>--%>
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
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">All Videos</span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:UpdatePanel ID="upl" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lstAllVideos" runat="server" GroupItemCount="1" ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="lstRecentVideos_PagePropertiesChanging" GroupPlaceholderID="groupPlaceHolder1">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 200px; width: 142.5px;">
                                                    <a href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                        <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" style="width: 142.5px;" onerror="this.src='images/play-button.png'">
                                                    </a>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text" style="width: 142.5px;">
                                                    <h1><a href='<%#Eval("shortfilm") %>'>
                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                                    <p class="vid-info-text">
                                                        <span>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Tag") %>'></asp:Label></span>
                                                       <%-- <span>&#47;</span>
                                                        <span>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>
                                                            views</span>--%>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <div>
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
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
                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lstAllVideos" PageSize="6">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                        ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                    </GroupTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

