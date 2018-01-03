<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmArtistDetails.aspx.cs" Inherits="Admin_frmArtistDetails" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    </script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New Artist Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblName" runat="server" Text="Name :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="1" />
                                                <asp:Label ID="lblDetailsId" runat="server" Visible="true" />
                                            </div>
                                            <asp:Label ID="lblInterestArea" runat="server" Text="Interest Area :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlInterestArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblGender" runat="server" Text="Gender :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:RadioButton ID="rdbMale" runat="server" CssClass="radio radio-inline" Text="Male" Checked="true" GroupName="Gender" />
                                                <asp:RadioButton ID="rdbFemale" runat="server" CssClass="radio radio-inline" Text="Female" GroupName="Gender" />
                                            </div>
                                            <asp:Label ID="lblLocation" runat="server" Text="Location :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-2 control-label" />
                                             <div class="col-sm-4">
                                                        <asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple">
                                                          
                                                        </asp:ListBox>
                                                    </div>
                                            <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                             <asp:Label ID="lblContactInformation" runat="server" Text="Contact Information :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtContactInformation" runat="server" CssClass="form-control" placeholder="Email or Phone No"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblPhoto" runat="server" Text="Photo :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-4">
                                                <asp:FileUpload ID="fupPhoto" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="Clear" OnClick="btn_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                            CommandName="Save" OnClick="btn_Click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 col-sm-12">
            <div class="latst-vid secondary-vid">
                <div class="vid-heading overflow-hidden">
                    <span class="wow fadeInUp" data-wow-duration="0.8s">Artist List
                        
                    </span>
                    <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                </div>
                <div class="row auto-clear">
                    <div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1" OnItemCommand="lstRecentVideos_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <asp:UpdatePanel ID="upla" runat="server">
                                            <ContentTemplate>
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Photo") %>' alt="video image" style="height: 171px; width: 180px;">
                                                    <a class="play-icon play-small-icon" href="#" onclick="return confirm('Are you sure!... You want to Delete Artist?')">
                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Display" ImageUrl="~/images/delete.png" ToolTip="Delete Artist" AlternateText="Delete" class="img-responsive play-svg svg" />
                                                    </a>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnk_Click">
                                                            <asp:Label ID="lblGridArtist" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                            <asp:Label ID="lblGridArtistId" runat="server" Text='<%#Eval("Artist_Details_Id") %>' Visible="false" />
                                                            <asp:Label ID="lblGridGender" runat="server" Text='<%#Eval("Gender") %>' Visible="false" />
                                                            <asp:Label ID="lblGridInterest_Areas" runat="server" Text='<%#Eval("Artist_Name") %>' Visible="false" />
                                                            <asp:Label ID="lblGridArtistIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' Visible="false" />
                                                        </asp:LinkButton></h1>
                                                    <%--<p class="vid-info-text">
                                                        <span>
                                                            <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label></span>
                                                    </p>--%>
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
        
    </div>
    <div id="myModal" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnUpdate">
                    <asp:UpdatePanel ID="uplUpdate" runat="server">
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
                                        <asp:Label ID="Label1" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:RadioButton ID="rdbUpdateMale" runat="server" CssClass="radio radio-inline" Text="Male" Checked="true" GroupName="Gender1" />
                                            <asp:RadioButton ID="rdbUpdateFemale" runat="server" CssClass="radio radio-inline" Text="Female" GroupName="Gender1" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Interest Area :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUpdateInterestArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
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
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="504" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
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
        <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
</asp:Content>

