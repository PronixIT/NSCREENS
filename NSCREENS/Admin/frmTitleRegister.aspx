<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmTitleRegister.aspx.cs" Inherits="Admin_frmTitleRegister" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"  %>
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
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New Title Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:ListBox ID="lstLanguage" runat="server" SelectionMode="Multiple">
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
                                            <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:FileUpload ID="fupUploadImage" runat="server" />
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
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Title List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvTitle" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvTitle_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />
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
                                                        <asp:Label ID="lblGridLanguages1" runat="server" Text='<%#Eval("Languages1") %>' Visible="false" />
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
                                <h4 id="myModalLabel" class="modal-title">Update Title</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateTitle" runat="server" CssClass="form-control" TabIndex="501" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblDumpLanguages" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateTag" runat="server" Text="Tag :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateTag" runat="server" CssClass="form-control" TabIndex="1" />
                                            <asp:Label ID="lblUpdateTagDump" runat="server" Visible="false"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateLanguage" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" />
                                            <div id="Lang" runat="server" visible="false">
                                                <asp:ListBox ID="lstUpdateLanguage" runat="server" SelectionMode="Multiple">
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
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Upload Image :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:FileUpload ID="fupUpdateImage" runat="server" />
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesTitle" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Title" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoTitle" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Title" TabIndex="502" />
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
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate" />
                        </Triggers>
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
                    $('[id*=lstUpdateLanguage]').multiselect({
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
                    $('[id*=lstUpdateLanguage]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
    </script>
</asp:Content>

