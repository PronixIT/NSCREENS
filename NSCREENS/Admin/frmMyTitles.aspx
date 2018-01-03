<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmMyTitles.aspx.cs" Inherits="Admin_frmTitleRegister" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=lstLanguage]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstUpdateLanguage]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">My Titles
                    </span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                    </div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <asp:UpdatePanel ID="upla" runat="server">
                                            <ContentTemplate>
                                                <div class="latest-vid-img-container">
                                                    <div class="vid-img">
                                                        <img id="imgRelatedVideos" runat="server" class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 184px; width: 184px">
                                                        <a id="ashortfilm" runat="server" href="#" class="play-icon play-small-icon">
                                                            <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'" style="height: 184px; width: 184px">
                                                        </a>
                                                        <div class="overlay-div"></div>
                                                    </div>
                                                    <div class="vid-text">
                                                        <h1><a id="ashortfilm1" runat="server" href="#">
                                                            <asp:Label ID="lblRelatedTitle" runat="server" Text='<%#Eval("Title_Name") %>'></asp:Label></a></h1>
                                                        <p class="vid-info-text">
                                                            <span>
                                                                <asp:Label ID="lblReatedDuration" runat="server" Text='<%#Eval("Tag") %>'></asp:Label></span>
                                                        </p>
                                                    </div>
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
        </div>
        <%--<div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">My Titles</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvTitle" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title_Name") %>' />
                                                        <asp:Label ID="lblGridTitleId" runat="server" Text='<%#Eval("Title_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tag">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Languages">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridLanguages" runat="server" Text='<%#Eval("Languages") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitleIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</asp:Content>

