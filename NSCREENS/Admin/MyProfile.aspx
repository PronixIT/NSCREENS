<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="Admin_MyProfile" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopupDescription() {
            $('#myModalDescription').modal('show');

            $('#myModalDescription').modal({
                backdrop: true,
                keyboard: true
            })
        }

    </script>
    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnProfileUpload.ClientID %>").click();
            }
        }
    </script>
    <div id="page-bar">
        <div class="container">
            <div class="row">
                <div class="col-md-9 col-sm-8 col-xs-12">
                    <div class="page-title">
                        <h1 class="text-uppercase">Welcome 
                            <asp:Label ID="lblTopName" runat="server"></asp:Label></h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Inner page Bar -->
    <!-- Secondary Section -->

    <div id="video-detail">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="vid-detail-container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row user-menu-container square">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12 no-pad">
                                                <div class="">
                                                    <a id="aImage" runat="server" href="#"><span class="fa fa-camera label label-default" style="position: absolute; top: 10px; right: 10px;">Add Photo </span>
                                                        <asp:FileUpload ID="fup" runat="server" Style="opacity: 0; position: absolute; top: 10px; right: 10px; width: 80px;" onchange="UploadFile(this);" />
                                                    </a>
                                                    <asp:Button ID="btnProfileUpload" runat="server" OnClick="Upload" Style="display: none;" />
                                                    <asp:Image ID="img" runat="server" Width="1170px" Height="333px" CssClass="img-responsive thumbnail" ImageUrl="~/Video_Images/Advatizments_1.jpg" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-8">
                                            <div class="vid-text">
                                                <h1>
                                                    <asp:Label ID="lblProductionName" runat="server" Text="Production Name"></asp:Label></h1>
                                                <%--<p><span>Other Details if any</span></p>--%>
                                            </div>

                                            <div class="video-detail-text">
                                                <p>
                                                    <asp:UpdatePanel ID="uplDesc" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text="Production Name"></asp:Label>&nbsp;<asp:LinkButton ID="lnkDescription" runat="server" CssClass="fa fa-edit" OnClick="lnkDescription_Click"></asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div id="sidemenu" runat="server">
                                                <div id="cssmenu">
                                                    <ul>
                                                        <%--<li>
                                                            <a href="#">
                                                                <asp:Label ID="Label5" runat="server" Text="FOLLOW" CssClass="btn btn-info"></asp:Label></a>
                                                        </li>--%>
                                                        <li>
                                                            <a href="#">
                                                                <asp:Label ID="lbl" runat="server" Text="Title" CssClass="btn btn-info"></asp:Label></a>
                                                            <ul>
                                                                <li><a href="frmTitleRegister.aspx">Register Title</a></li>
                                                                <li><a href="frmMyTitles.aspx">My Titles</a></li>
                                                            </ul>
                                                        </li>
                                                        <li>
                                                            <a href="#">
                                                                <asp:Label ID="Label3" runat="server" Text="Upload" CssClass="btn btn-info"></asp:Label></a>
                                                            <ul>
                                                                <li><a href="frmAddShortfilm.aspx">Short Film</a></li>
                                                                <li><a href="frmTrailerUpload.aspx">Trailer</a></li>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="related-item">
                                    <div class="vid-heading overflow-hidden">
                                        <span class="wow fadeInUp" data-wow-duration="0.8s">Upcoming Films
                                        </span>
                                        <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                                        </div>
                                    </div>
                                    <div class="row auto-clear">
                                        <div class="vid-container">
                                            <asp:ListView ID="lstUpcomingVideo" runat="server" GroupItemCount="1">
                                                <ItemTemplate>
                                                    <div class="col-md-2 col-sm-3">
                                                        <div class="latest-vid-img-container">
                                                            <div class="vid-img">
                                                                <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 200px; width: 142.5px;">
                                                                <a href='<%#Eval("VideoTrailer") %>' runat="server" id="video" class="play-icon play-small-icon">
                                                                    <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" style="height: 200px; width: 142.5px;" onerror="this.src='images/play-button.png'">
                                                                </a>
                                                                <div class="overlay-div"></div>
                                                            </div>
                                                            <div class="vid-text" style="width: 142.5px;">
                                                                <h1><a>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title_Name") %>'></asp:Label></a></h1>
                                                                <p class="vid-info-text">
                                                                <span><asp:Label ID="Label2" runat="server" Text='<%#Eval("Tag") %>'></asp:Label> (<asp:Label ID="lblRelatedViews" runat="server" Text='<%#Eval("Language_Name") %>'></asp:Label>)</span>
                                                                    </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="related-item">
                                    <div class="vid-heading overflow-hidden">
                                        <span class="wow fadeInUp" data-wow-duration="0.8s">Registered Videos
                                        </span>
                                        <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="vid-container">
                                            <asp:UpdatePanel ID="upldd" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                                                        <ItemTemplate>
                                                            <div class="col-md-2 col-sm-3">
                                                                <div class="latest-vid-img-container">
                                                                    <div class="vid-img">
                                                                        <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 200px; width: 142.5px;">
                                                                        <div id="My" runat="server" visible='<%#Eval("own") %>'>
                                                                            <a href='<%#Eval("shortfilm") %>' class="play-icon" style="height: 30px; width: 30px; margin-left: -10px; margin-top: -10px;">
                                                                                <img class="" src="../images/play-button.png" alt="play" style="height: 30px; width: 30px; margin-left: -10px; margin-top: -10px;" onerror="this.src='images/play-button.png'">
                                                                            </a>
                                                                            <a href="#" class="play-icon play-small-icon" style="height: 30px; width: 30px; margin-left: -5px;">
                                                                                <asp:ImageButton ID="imgEdit" runat="server" CssClass="" CommandName="Display" ImageUrl="../images/edit.png" alt="play" Style="height: 30px; width: 30px; margin-left: 30px;" onerror="this.src='images/play-button.png'" />
                                                                            </a>
                                                                        </div>
                                                                        <div id="Div1" runat="server" visible='<%#Eval("other") %>'>
                                                                            <a href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" style="height: 200px; width: 142.5px;" onerror="this.src='images/play-button.png'">
                                                                            </a>
                                                                        </div>
                                                                        <div class="overlay-div"></div>
                                                                    </div>
                                                                    <div class="vid-text" style="width: 142.5px;">
                                                                        <h1><a href='<%#Eval("shortfilm") %>'>
                                                                            <asp:Label ID="lblShort_film_Id" runat="server" Text='<%#Eval("Short_film_Id") %>' Visible="false"></asp:Label>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>--%>
                                <div id="recent" runat="server" class="col-md-12 col-sm-12">
                                    <div class="latst-vid secondary-vid">
                                        <div class="vid-heading overflow-hidden">
                                            <span class="wow fadeInUp" data-wow-duration="0.8s">Released Films</span>
                                            <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                                        </div>
                                        <div class="row auto-clear">
                                            <div class="vid-container">
                                                <asp:UpdatePanel ID="upldd" runat="server">
                                                    <ContentTemplate>
                                                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                                                            <ItemTemplate>
                                                                <div class="col-md-2 col-sm-3">
                                                                    <div class="latest-vid-img-container">
                                                                        <div class="vid-img">
                                                                            <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height: 200px; width: 142.5px;">
                                                                            <div id="My" runat="server" visible='<%#Eval("own") %>'>
                                                                                <a href='<%#Eval("shortfilm") %>' class="play-icon" style="height: 30px; width: 30px; margin-left: -10px; margin-top: -10px;">
                                                                                    <img class="" src="../images/play-button.png" alt="play" style="height: 30px; width: 30px; margin-left: -10px; margin-top: -10px;" onerror="this.src='images/play-button.png'">
                                                                                </a>
                                                                                <a href="#" class="play-icon play-small-icon" style="height: 30px; width: 30px; margin-left: -5px;">
                                                                                    <asp:ImageButton ID="imgEdit" runat="server" CssClass="" CommandName="Display" ImageUrl="../images/edit.png" alt="play" Style="height: 30px; width: 30px; margin-left: 30px;" onerror="this.src='images/play-button.png'" />
                                                                                </a>
                                                                            </div>
                                                                            <div id="Div1" runat="server" visible='<%#Eval("other") %>'>
                                                                                <a href='<%#Eval("shortfilm") %>' class="play-icon play-small-icon">
                                                                                    <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" style="height: 200px; width: 142.5px;" onerror="this.src='images/play-button.png'">
                                                                                </a>
                                                                            </div>
                                                                            <div class="overlay-div"></div>
                                                                        </div>
                                                                        <div class="vid-text" style="width: 142.5px;">
                                                                            <h1><a href='<%#Eval("shortfilm") %>'>
                                                                                <asp:Label ID="lblShort_film_Id" runat="server" Text='<%#Eval("Lan_Short_film_Id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label></a></h1>
                                                                            <asp:Label ID="lblLanguageIDs" runat="server" Text='<%#Eval("Language") %>' Visible="false" />
                                                                            <p class="vid-info-text">
                                                                                <span><asp:Label ID="Label2" runat="server" Text='<%#Eval("Tag") %>'></asp:Label>
                                                            (<asp:Label ID="Label1" runat="server" Text='<%#Eval("Language_Name") %>'></asp:Label>)</span>
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

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Update Artist</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>

                                                    <div class="col-sm-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvArtist" runat="server" AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                                                                CssClass="table table-bordered table-hover table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Artist">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Artist") %>' />
                                                                            <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Artist_Id") %>' Visible="false" />
                                                                            <asp:Label ID="lblShort_Artist_Id" runat="server" Text='<%#Eval("Short_Artist_Id") %>' Visible="false" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:DropDownList ID="ddlArtist" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlArtist_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Name") %>' Visible="false" />
                                                                            <asp:Label ID="lblGridNameId" runat="server" Text='<%#Eval("NameId") %>' Visible="true" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div class="col-sm-12">
                                                                                <div class="col-sm-8">
                                                                                    <asp:DropDownList ID="ddlAName" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="fa fa-remove" OnClick="btnCancel_Click" />
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lnkAdd" runat="server" CssClass="fa fa-plus" OnClick="lnkAdd_Click"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblDescriptionArtist" runat="server" CssClass="control-label col-sm-2" Text="Description"></asp:Label>
                                                            <div class="col-sm-10">
                                                                <asp:TextBox ID="txtDescriptionArtist" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image" CssClass="control-label col-sm-3" />
                                                            <div class="col-sm-4">
                                                                <asp:Label ID="lblShortId" runat="server" Visible="false" />
                                                                <asp:Label ID="lblLangId" runat="server" Visible="false" />
                                                                <asp:FileUpload ID="fupImage" runat="server" />
                                                            </div>
                                                            <div class="col-sm-3 pull-right">
                                                                <asp:Button ID="btnUpload" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpload_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="504" />--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="myModalDescription" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnUpdate1234">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Update Description</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblAddDescription" runat="server" Text="Description:" CssClass="col-sm-2 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtUpdateDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Width="400px" Height="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate1234" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="504" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate1234" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

