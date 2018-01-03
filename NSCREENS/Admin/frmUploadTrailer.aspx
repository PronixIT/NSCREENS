<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmUploadTrailer.aspx.cs" Inherits="Admin_frmUploadShortfilm" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    </script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Upload Trailer</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvArtist" runat="server" AutoGenerateColumns="False" GridLines="None" ShowFooter="true"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Artist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Artist") %>' />
                                                        <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("ArtistId") %>' Visible="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <hr />
                     <div id="progress-container" class="progress">
                                                        <div id="progress" class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar" aria-valuenow="46" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                            &nbsp;0%
           
                                                        </div>
                                                    </div>
                    <%--<div id="progress-container1" class="progress">
                        <div id="progress1" class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar" aria-valuenow="46" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                            &nbsp;0%
                        </div>
                    </div>--%>
                    <div class="row ">
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
                                                    <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblTitleAvailable" runat="server" />
                                                        <asp:Label ID="lblShortFilmId" runat="server" Visible="false" />
                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTitle_TextChanged"/>
                                                        <asp:TextBox ID="txtURL" runat="server" CssClass="hidden" />
                                                        <asp:TextBox ID="txtURLTRailer" runat="server" CssClass="hidden" />
                                                        <ajax:AutoCompleteExtender ID="aceCustomerName" runat="server" ServiceMethod="SearchCustomerName"
                                                            TargetControlID="txtTitle" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple" Enabled="false">
                                                            <asp:ListItem Value="1">Telugu</asp:ListItem>
                                                            <asp:ListItem Value="2">Hindi</asp:ListItem>
                                                            <asp:ListItem Value="3">English</asp:ListItem>
                                                            <asp:ListItem Value="4">Bengali</asp:ListItem>
                                                            <asp:ListItem Value="5">Marathi</asp:ListItem>
                                                            <asp:ListItem Value="6">Tamil</asp:ListItem>
                                                            <asp:ListItem Value="7">Urdu</asp:ListItem>
                                                            <asp:ListItem Value="8">Gujarati</asp:ListItem>
                                                            <asp:ListItem Value="9">Gujarati</asp:ListItem>
                                                            <asp:ListItem Value="10">Kannada</asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Enabled="false"
                                                            TextMode="MultiLine" />
                                                    </div>
                                                </div>

                                                <div class="form-group hide">
                                                    <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="fupImage" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" Text="Category :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:ListBox ID="lstCategory" runat="server" SelectionMode="Multiple" Enabled="false"></asp:ListBox>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblChannel" runat="server" Text="Production :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlProduction" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                   <%-- <div class="col-md-4">
                                                        <div id="drop_zone1">Drop Files Here (Trailer)</div>
                                                        <br />
                                                        <label class="btn btn-block btn-info">
                                                            Browse&hellip;
                                                            <input id="browse1" type="file" style="display: none;">
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="hide" />
                                                        </label>
                                                        
                                                    </div>--%>
                                                     
                                                            <div class="col-md-4">
                                                                <div id="drop_zone">Drop Files Here</div>
                                                                <br />
                                                                <label class="btn btn-block btn-info">
                                                                    Browse&hellip;
                                                                    <input id="browse" type="file" style="display: none;">
                                                                </label>
                                                                &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="hidden" />
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
                                            <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
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
                                            <%--<asp:TemplateField HeaderText="Hero">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridHero" runat="server" Text='<%#Eval("Hero") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Heroine">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridHeroine" runat="server" Text='<%#Eval("Heroine") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Technician">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridTechnician" runat="server" Text='<%#Eval("Technician") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Category Name">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
                description: document.getElementById('<%= txtDescription.ClientID %>').value, //document.getElementById('videoDescription').value,
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

                    if (index > -1) {
                        /* The metadata contains all of the uploaded video(s) details see: https://developer.vimeo.com/api/endpoints/videos#/{video_id} */
                        url = this.metadata[index].link //

                        /* add stringify the json object for displaying in a text area */
                        var pretty = JSON.stringify(this.metadata[index], null, 2)

                        console.log(pretty) /* echo server data */
                    }

                    document.getElementById('<%= txtURLTRailer.ClientID %>').value = url;

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


