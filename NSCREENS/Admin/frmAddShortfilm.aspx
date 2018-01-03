<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddShortfilm.aspx.cs" Inherits="Admin_frmAddShortfilm" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('[id*=lstLanguage]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstCategory]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=ddlAName]').multiselect({
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


        function msg1() {
            //showMessage('<strong>Upload Successful</strong>')
            alert('navya');
        }
    </script>

    <script type="text/javascript">
        //function showimagepreview(input) {
        //    if (input.files && input.files[0]) {
        //        var filerdr = new FileReader();
        //        filerdr.onload = function (e) {
        //            alert(e.target.result);
        //            $('#ContentPlaceHolder1_imgPhoto').attr('src', e.target.result);
        //        }
        //        filerdr.readAsDataURL(input.files[0]);
        //    }



        //    //var vid = document.getElementById("fudUploadVideo");
        //    //alert(input.file);
        //    //    alert(vid.duration);
        //}

        var myVideos = [];
        window.URL = window.URL || window.webkitURL;
        function setFileInfo(files) {
            myVideos.push(files[0]);
            var video = document.createElement('video');
            video.preload = 'metadata';
            video.onloadedmetadata = function () {
                window.URL.revokeObjectURL(this.src)
                var duration = video.duration;
                myVideos[myVideos.length - 1].duration = duration;
                updateInfos();
            }
            video.src = URL.createObjectURL(files[0]);
        }

        function updateInfos() {
            document.querySelector('#infos').innerHTML = "";
            for (i = 0; i < myVideos.length; i++) {
                document.getElementById('<%= txtDuration.ClientID %>').value = (myVideos[i].duration);

                document.querySelector('#infos').innerHTML = (myVideos[i].duration);
                //alert("<div>" + myVideos[i].name + " duration: " + myVideos[i].duration + '</div>');
            }
        }


    </script>
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css">--%>
    <%--<style>
        html {
            position: relative;
            min-height: 100%;
        }

        body {
            margin-bottom: 60px;
            color: #505662;
        }

        .help {
            font-size: smaller;
        }

        .page-header {
            padding-bottom: 18px;
            margin: 40px 0 12px;
        }

        .logo {
            width: 100%;
            margin-bottom: 20px;
        }

        .lead {
            font-size: 18px;
            margin-bottom: 12px;
        }

        .footer {
            position: absolute;
            bottom: 0;
            padding-top: 15px;
            width: 100%;
            /* Set the fixed height of the footer here */
            height: 120px;
            color: #505662;
        }

        .footer a.brand {
            color: #505662;
        }

        .footer a.brand:hover {
            color: #393e46;
            text-decoration: none;
        }

        .footer .container {
            border-top: 1px solid #eee;
            padding-top: 45px;
        }

        /* Custom page CSS */

        .container {
            width: auto;
            max-width: 680px;
            padding: 0 15px;
        }

        .container .text-muted {
            margin: 20px 0;
        }

        #progress-container {
            -webkit-box-shadow: none;
            box-shadow: inset none;
            display:none;
        }

        #drop_zone {
            border: 2px dashed #bbb;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            padding-top: 60px;
            text-align: center;
            font: 20pt bold 'Helvetica';
            color: #bbb;
            height:140px;
        }

        #video-data {
            margin-top: 1em;
            font-size: 1.1em;
            font-weight: 500;
        }

       
        .ui.bragit.button,
        .ui.bragit.buttons .button {
            background-color: #676f7e;
            color: #fff!important;
        }

        .ui.bragit.label {
            color: #505662!important;
            border-color: #676f7e!important;
            background-color: #ffffff;
        }

        .ui.bragit.button:focus,
        .ui.bragit.buttons .button:focus,
        .ui.bragit.button:hover,
        .ui.bragit.buttons .button:hover {
            background-color: #505662;
        }

        .ui.bragit.labels .label:focus,
        .ui.bragit.label:focus,
        .ui.bragit.labels .label:hover,
        .ui.bragit.label:hover {
            color: #505662!important;
            border-color: #505662!important;
        }

        .ui.labeled .ui.button .star.icon {
            color: #F5CC7A!important;
        }
    </style>--%>
    <style>
        .multiselect-container > li > a > label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 3px 20px 3px 10px;
        }
    </style>
    <script src="../js/bootstrap-multiselectImage.js" type="text/javascript"></script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Short film Entry</h3>
                </div>
                <div class="panel-body">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                        <asp:GridView ID="gvArtist" runat="server" AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Artist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Artist") %>' />
                                                        <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("ArtistId") %>' Visible="false" />
                                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id") %>' Visible="false" />
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
                                                                <%--<asp:DropDownList ID="ddlAName" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                                <asp:ListBox ID="ddlAName" runat="server" SelectionMode="Single" class="multiselect multiselect-icon" role="multiselect"></asp:ListBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:LinkButton ID="lnkAddArtist" runat="server" Text="Add Artist" OnClick="lnkAddArtist_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="fa fa-remove" OnClick="btnCancel_Click" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" CssClass="fa fa-plus" OnClick="lnkAdd_Click"></asp:LinkButton>&nbsp;(Add)
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    <hr />
                    <div id="progress-container" class="progress">
                        <div id="progress" class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar" aria-valuenow="46" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                            &nbsp;0%
           
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="results"></div>
                        </div>
                    </div>
                    <hr />
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-xs-12">
                                            <div class="col-xs-6">
                                                  <div class="form-group">
                                                    <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtTitle_TextChanged"></asp:DropDownList>
                                                        <%--<asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple">
                                                        </asp:ListBox>--%>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblTitleAvailable" runat="server" />
                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTitle_TextChanged" />
                                                        <asp:TextBox ID="txtDuration" runat="server" CssClass="hidden" />
                                                        <asp:TextBox ID="txtURL" runat="server" CssClass="hidden" />
                                                        <asp:TextBox ID="txtURLTRailer" runat="server" CssClass="hidden" />
                                                        <asp:Label ID="lblAvailable" runat="server" Visible="false" />
                                                        <asp:Label ID="lblRegId" runat="server" Visible="false" />
                                                        <ajax:AutoCompleteExtender ID="aceCustomerName" runat="server" ServiceMethod="SearchCustomerName"
                                                            TargetControlID="txtTitle" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"
                                                            TextMode="MultiLine" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="fupImage" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" Text="Category :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:ListBox ID="lstCategory" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblChannel" runat="server" Text="Production :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlProduction" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label7" runat="server" Text="Upload Film :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-md-6">
                                                        <div id="drop_zone">It should be (>=20 minutes) to get approved </div>
                                                        <br />
                                                        <label class="btn btn-block btn-info">
                                                            Browse&hellip;
                                                                    <input id="browse" type="file" style="display: none;">
                                                        </label>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="Clear" OnClick="btnClear_Click" />
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
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Short Film List</h3>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <asp:UpdatePanel ID="UplShortFilm" runat="server">
                            <ContentTemplate>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvShortFilm" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvShortFilm_RowCommand">
                                        <Columns>
                                            <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />--%>
                                            <asp:TemplateField HeaderText="S. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                    <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Short_film_Id") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tag">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Language">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language") %>' />
                                                    <asp:Label ID="lblGridCategoryId" runat="server" Text='<%#Eval("Category") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Channel Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridChannel_Name" runat="server" Text='<%#Eval("Channel_Name") %>' />
                                                    <asp:Label ID="lblGridChannel" runat="server" Text='<%#Eval("Channel") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Views">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridVisits" runat="server" Text='<%#Eval("Visits") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <div class='<%#Eval("StatusClr") %>'>
                                                        <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Active">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridStateIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
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
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Add Artist</h4>
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
                                        <asp:Label ID="Label3" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:RadioButton ID="rdbUpdateMale" runat="server" CssClass="radio radio-inline" Text="Male" Checked="true" GroupName="Gender1" />
                                            <asp:RadioButton ID="rdbUpdateFemale" runat="server" CssClass="radio radio-inline" Text="Female" GroupName="Gender1" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Interest Area :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUpdateInterestArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblLocation" runat="server" Text="Location :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblContactInformation" runat="server" Text="Contact Information :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtContactInformation" runat="server" CssClass="form-control" placeholder="Email or Phone No"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateDescription" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" Text="Photo :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:FileUpload ID="fupupdatePhoto" runat="server" />
                                        </div>
                                    </div>
                                    <%-- <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesArtist" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Artist" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoArtist" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Artist" TabIndex="502" /></div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Submit" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btnUpdate_Click" TabIndex="504" />
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
    <script type="text/javascript" src="vimeo-upload.js"></script>
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

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstCategory]').multiselect({
                        includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstCategory]').multiselect({
                        includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });
                }
            });
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=ddlAName]').multiselect({
                        includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });
                }
            });
        };
    </script>

    <script type="text/javascript">

        /**
         * Called when files are dropped on to the drop target or selected by the browse button.
         * For each file, uploads the content to Drive & displays the results when complete.
         */
        function handleFileSelect(evt) {
            evt.stopPropagation()
            evt.preventDefault()

            var files = evt.dataTransfer ? evt.dataTransfer.files : $(this).get(0).files
            var results = document.getElementById('results')

            /* Clear the results div */
            while (results.hasChildNodes()) results.removeChild(results.firstChild)

            /* Rest the progress bar and show it */
            updateProgress(0)
            document.getElementById('progress-container').style.display = 'block'

            /* Instantiate Vimeo Uploader */
            ; (new VimeoUpload({
                name: document.getElementById('<%= txtTitle.ClientID %>').value,//document.getElementById('videoName').value,
                description: document.getElementById('<%= txtTitle.ClientID %>').value, //document.getElementById('videoDescription').value,
                private: false,//document.getElementById('make_private').checked,
                file: files[0],
                token: "fbfdcdfadd16cee9207dcf04217c8545", //document.getElementById('accessToken').value,
                upgrade_to_1080: false,//document.getElementById('upgrade_to_1080').checked,
                onError: function (data) {
                    showMessage('<strong>Error</strong>: ' + JSON.parse(data).error, 'danger')
                },
                onProgress: function (data) {
                    updateProgress(data.loaded / data.total)
                },
                onComplete: function (videoId, index) {
                    var url = 'https://vimeo.com/' + videoId
                    //alert('1')
                    if (index > -1) {
                        /* The metadata contains all of the uploaded video(s) details see: https://developer.vimeo.com/api/endpoints/videos#/{video_id} */
                        url = this.metadata[index].link //

                        /* add stringify the json object for displaying in a text area */
                        var pretty = JSON.stringify(this.metadata[index], null, 2)

                        console.log(pretty) /* echo server data */
                    }

                    document.getElementById('<%= txtURL.ClientID %>').value = url;

                    //showMessage('<strong>Upload Successful</strong>: check uploaded video @ <a href="' + url + '">' + url + '</a>. ')
                    showMessage('<strong>Upload Successful</strong>')
                }
            })).upload()

            /* local function: show a user message */
            function showMessage(html, type) {
                /* hide progress bar */
                document.getElementById('progress-container').style.display = 'none'

                /* display alert message */
                var element = document.createElement('div')
                element.setAttribute('class', 'alert alert-' + (type || 'success'))
                element.innerHTML = html
                results.appendChild(element)
            }
        }

        /**
         * Dragover handler to set the drop effect.
         */
        function handleDragOver(evt) {
            evt.stopPropagation()
            evt.preventDefault()
            evt.dataTransfer.dropEffect = 'copy'
        }

        /**
         * Updat progress bar.
         */
        function updateProgress(progress) {
            progress = Math.floor(progress * 100)
            var element = document.getElementById('progress')
            element.setAttribute('style', 'width:' + progress + '%')
            element.innerHTML = '&nbsp;' + progress + '%'
        }
        /**
         * Wire up drag & drop listeners once page loads
         */
        document.addEventListener('DOMContentLoaded', function () {
            var dropZone = document.getElementById('drop_zone')
            var browse = document.getElementById('browse')
            dropZone.addEventListener('dragover', handleDragOver, false)
            dropZone.addEventListener('drop', handleFileSelect, false)
            browse.addEventListener('change', handleFileSelect, false)
        })

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            var dropZone = document.getElementById('drop_zone')
            var browse = document.getElementById('browse')
            dropZone.addEventListener('dragover', handleDragOver, false)
            dropZone.addEventListener('drop', handleFileSelect, false)
            browse.addEventListener('change', handleFileSelect, false)
        });

    </script>

</asp:Content>

