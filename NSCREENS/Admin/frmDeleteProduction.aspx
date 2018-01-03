<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmDeleteProduction.aspx.cs" Inherits="Admin_frmProductionList" EnableEventValidation="false" %>

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
                                <asp:ListView ID="lstOther" runat="server" GroupItemCount="1" OnItemCommand="lstOther_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Img") %>' alt="video image" style="height: 171px; width: 180px;">
                                                    <a class="play-icon play-small-icon" href="#" onclick="return confirm('Are you sure!... You want to Delete production?')">
                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Display" ImageUrl="~/images/delete.png" ToolTip="Delete production" AlternateText="Edit" class="img-responsive play-svg svg" />
                                                    </a>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1>
                                                        <asp:LinkButton ID="lnk" runat="server">
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Channel_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbllstChannel" runat="server" Text='<%#Eval("Channel_Id") %>'></asp:Label>
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

