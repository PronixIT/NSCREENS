<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddArtistByUser.aspx.cs" Inherits="Admin_frmHome" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('[id*=lstLanguage]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

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
       
   
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <div class="container">
                        <div class="col-sm-6">
                            <asp:Label ID="lblArtistTypeMy" runat="server"></asp:Label>
                            <span class="wow" data-wow-duration="0.8s">&nbsp;&nbsp;<a id="A1" runat="server" class="fa fa-plus" href="frmArtistDetails.aspx" />&nbsp;(Add)
                            </span>
                        </div>
                    </div>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lstMyArtist" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 171px; width: 180px;" />
                                                    <a id="ashortfilm" class="play-icon play-small-icon">
                                                        <asp:ImageButton ID="ibnThdddumbnail" runat="server" class="img-responsive play-svg svg" Style="height: 30px; width: 30px; margin-left: -10px;" ImageUrl="~/images/eye.png" alt="play" CommandName="Display" />
                                                    </a>
                                                    <a href="#" class="play-icon play-small-icon" style="height: 30px; width: 30px; margin-left: -5px;">
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CssClass="" CommandName="DisplayEdit" ImageUrl="../images/edit.png" alt="play" Style="height: 30px; width: 30px; margin-left: 30px;" onerror="this.src='images/play-button.png'" />
                                                    </a>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1><a>
                                                        <asp:Label ID="lblArtistId" runat="server" Text='<%#Eval("Artist_Details_Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Name") %>'></asp:Label></a></h1>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                    <div class="container">
                        <div class="col-sm-4">
                            <asp:Label ID="lblArtistType" runat="server"></asp:Label>
                            <span class="wow" data-wow-duration="0.8s"></span>
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                         <div class="col-sm-2">
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                             <div class="search-box">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="autosuggest1 form-control" placeholder="Search Name"></asp:TextBox>                                  

                                </div>
                            <%--<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Name" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>--%>
                        </div>
                         <div class="col-sm-2 hide">
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtSearch_TextChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                <asp:ListItem Value="1">Female</asp:ListItem>
                                <asp:ListItem Value="2">Male</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 7px;" class="col-sm-1">
                            <asp:LinkButton ID="lnkSearch" runat="server" OnClick="txtSearch_TextChanged"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                                  </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 171px; width: 180px;" />
                                                    <a id="ashortfilm" class="play-icon play-small-icon">
                                                        <asp:ImageButton ID="ibnThdddumbnail" runat="server" class="img-responsive play-svg svg" ImageUrl="~/images/eye.png" alt="play" CommandName="Display" />
                                                    </a>
                                                    <div class="overlay-div"></div>
                                                </div>

                                                <div class="vid-text">

                                                    <h1><a>
                                                        <asp:Label ID="lblArtistId" runat="server" Text='<%#Eval("Artist_Details_Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Name") %>'></asp:Label></a></h1>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                                <asp:Label ID="Label3" runat="server" Text="Interest Area :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblArtist" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" Text="Gender :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" Text="Location :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lbltactInformation" runat="server" Text="Contact Info :" CssClass="col-sm-4 control-label"></asp:Label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="lblContactInformation" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
                                    <div class="col-sm-12">
                                         <div class="form-group">
                                            <asp:Label ID="lblLanguagesDis" runat="server" Text="Languages :" CssClass="col-sm-3 control-label"></asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtLanguagesDis" runat="server" CssClass="form-control" Enabled="false" TextMode="SingleLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Text="Description :" CssClass="col-sm-3 control-label"></asp:Label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="lblDescription" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
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
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdate">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button><h4 id="myModalLabel" class="modal-title">Update Artist</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateArtist" runat="server" Text="Name :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateArtist" runat="server" CssClass="form-control" TabIndex="501" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:RadioButton ID="rdbUpdateMale" runat="server" CssClass="radio radio-inline" Text="Male" Checked="true" GroupName="Gender1" />
                                            <asp:RadioButton ID="rdbUpdateFemale" runat="server" CssClass="radio radio-inline" Text="Female" GroupName="Gender1" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblContactInformation1" runat="server" Text="Contact Information :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtContactInformation" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                                 <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                             <div class="col-sm-6">
                                                        <asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple">
                                                           
                                                        </asp:ListBox>
                                                    </div>
                                            </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Location :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlLocationId" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Interest Area :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUpdateInterestArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateDescription" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Photo :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:FileUpload ID="fupupdatePhoto" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesArtist" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Artist" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoArtist" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Artist" TabIndex="502" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="504" />
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
    </a>
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstLanguage]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
         </script>
</asp:Content>

