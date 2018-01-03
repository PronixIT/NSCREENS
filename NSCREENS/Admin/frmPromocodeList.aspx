<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmPromocodeList.aspx.cs" Inherits="Admin_frmPromocodeList" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
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

        function divPopupEdit() {
            $('#myModalEdit').modal('show');

            $('#myModalEdit').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=lblIndex.ClientID %>").value = fileUpload.id;
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">My Profile
                        
                    </span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="hide"></asp:TextBox>

                        <asp:ListView ID="lstAllPromoters" runat="server" GroupItemCount="1" OnItemCommand="lstAllPromoters_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <asp:UpdatePanel ID="upla" runat="server">
                                            <ContentTemplate>
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 171px; width: 180px;">
                                                     <a id="ashortfilm" class="play-icon play-small-icon">
                                                                <asp:ImageButton ID="ibnThdddumbnail1" runat="server" class="img-responsive play-svg svg" ImageUrl="~/images/eye.png" alt="play" CommandName="Display" />
                                                            </a>
                                                    <%--<a class="play-icon play-small-icon" style="margin-top: -80px; margin-left: -70px; height: 30px; width: 30px;" href="#">
                                                        <asp:Image ID="imgCam" runat="server" ImageUrl="~/images/camera.png" Style="margin-top: -10px; margin-left: -10px; height: 30px; width: 30px;" />
                                                        <asp:FileUpload ID="fup" runat="server" Style="opacity: 0; position: absolute; top: 4px; width: 30px; height: 30px;" onchange="UploadFile(this);" />
                                                    </a>--%>
                                                    <%--<asp:Button ID="btnUpload" Text='<%#Eval("MainPhoto") %>' runat="server" OnClick="Upload" Style="display: none;" />--%>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1>
                                                        <asp:LinkButton ID="LinkButton1" runat="server">
                                                            <asp:Label ID="lblArtistId1234" runat="server" Text='<%#Eval("Register_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGridArtist" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                            <asp:Label ID="lblImage" runat="server" Text='<%#Eval("MainPhoto") %>' Visible="false"></asp:Label>
                                                        </asp:LinkButton></h1>
                                                    <p class="vid-info-text">
                                                        <span>
                                                            <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("City_Name") %>'></asp:Label></span>
                                                    </p>
                                                </div>
                                            </ContentTemplate>
                                            <%--<Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>--%>
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
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <div class="col-sm-4">
                        <span class="wow fadeInUp" data-wow-duration="0.8s">Promoter List
                        </span>
                    </div>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                    <asp:UpdatePanel ID="uplpromocode" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-8">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 hide">
                                    <div class="search-box">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Name"></asp:TextBox>
                                    </div>
                                </div>
                                <%--<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Name" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>--%>
                                <div style="margin-top: 7px;" class="col-sm-1">
                                    <asp:LinkButton ID="lnkSearch" runat="server" OnClick="txtSearch_TextChanged"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:UpdatePanel ID="uplre" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="lblIndex" runat="server" CssClass="hide"></asp:TextBox>
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
                                                            <%-- <a class="play-icon play-small-icon" style="margin-top: -80px;margin-left: -70px;height: 15px;width: 15px;" href="#">
                                                    <asp:Image ID="imgCam" runat="server" ImageUrl="~/images/camera.png"  style="margin-top: -15px;margin-left: -15px;height: 30px;width: 30px;"/>
                                                         <asp:FileUpload ID="fup" runat="server" style="opacity:0;position:absolute;top: 4px;width: 15px;height: 15px;" onchange="UploadFile(this);" />
                                            </a>
                                                <asp:Button ID="btnUpload" Text='<%#Eval("MainPhoto") %>' runat="server" OnClick="Upload" Style="display: none;"/>--%>
                                                            <div class="overlay-div"></div>
                                                        </div>
                                                        <div class="vid-text">
                                                            <h1>
                                                                <asp:LinkButton ID="LinkButton11" runat="server">
                                                                    <asp:Label ID="lblArtistId" runat="server" Text='<%#Eval("Register_Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGridArtist1" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblImage1" runat="server" Text='<%#Eval("MainPhoto") %>' Visible="false"></asp:Label>
                                                                </asp:LinkButton></h1>
                                                            <p class="vid-info-text">
                                                                <span>
                                                                    <asp:Label ID="lblGridDescription1" runat="server" Text='<%#Eval("City_Name") %>'></asp:Label></span>
                                                            </p>
                                                        </div>
                                                    </ContentTemplate>
                                                    <%--<Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>--%>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p>List is empty</p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="uplUpdate" runat="server">
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
                                                <asp:Label ID="Label3" runat="server" Text="Contact info :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblMobile_No" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group hide">
                                                <asp:Label ID="Label5" runat="server" Text="EmailId :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblEmailId" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                                <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" Text="State :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                                <div class="form-group">
                                                <asp:Label ID="Label4" runat="server" Text="District :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group hide">
                                                <asp:Label ID="Label6" runat="server" Text="Address :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblAddress" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="myModalEdit" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                                <asp:Label ID="Label10" runat="server" Text="Name :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtpromoName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                    <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Contact info :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtContactinfo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                     <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="State :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPromoState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPromoState_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPromoDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPromoDistrict_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPromoCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                     <div class="form-group">
                                            <asp:Label ID="lblImage12" runat="server" Text="Image :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="fupImage" runat="server" />
                                            </div>
                                        </div>
                                            </div>
                                             <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Image ID="imgpromo" runat="server" Style="height: 171px; width: 180px;" />
                                            </div>
                                        </div>
                                           </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                             <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="506" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info pull-right"
                                    ValidationState="Update" CommandName="Update" OnClick="btn_Click" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

