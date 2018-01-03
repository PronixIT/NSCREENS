<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmProductions.aspx.cs" Inherits="Admin_frmProductionList" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:UpdatePanel ID="upld" runat="server">
            <ContentTemplate>
                <div class="col-md-12 col-sm-12">
                    <div class="latst-vid secondary-vid">
                        <div class="vid-heading overflow-hidden">
                            <div class="col-sm-6">
                                <span class="wow" data-wow-duration="0.8s">Production List
                                </span>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Name" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lnkSearch" runat="server" OnClick="txtSearch_TextChanged"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>

                            <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                        </div>
                        <div class="row auto-clear">
                            <div class="vid-container">
                                <asp:ListView ID="lstOther" runat="server" GroupItemCount="1">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Img") %>' alt="video image" style="height: 171px; width: 180px;">
                                                    <asp:LinkButton ID="lnkMore" runat="server" CssClass="play-icon play-small-icon" OnClick="lnkMore_Click" CommandName='<%#Eval("Channel_Id") %>'>
                                                        <asp:Image ID="imgView" runat="server" CssClass="img-responsive play-svg svg" ImageUrl="~/images/eye.png" />
                                                    </asp:LinkButton>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1>
                                                        <asp:LinkButton ID="lnk" runat="server" OnClick="lnkMore_Click" CommandName='<%#Eval("Channel_Id") %>'>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Channel_Name") %>'></asp:Label>
                                                        </asp:LinkButton></h1>
                                                    <%--<p class="vid-info-text">
                                                        <span>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                        </span>
                                                    </p>--%>
                                                </div>
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

