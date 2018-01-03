<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmDeleteShortFilm.aspx.cs" Inherits="Admin_frmDeleteShortFilm" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"%>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstUsers]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Search Short Film</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnDisplay">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Title :" CssClass="col-sm-1 control-label" />
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlSearchTitle" runat="server" CssClass="form-control" />
                                            </div>
                                            <asp:Label ID="Label7" runat="server" Text="Category :" CssClass="col-sm-1 control-label" />
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlSearchCategory" runat="server" CssClass="form-control" />
                                            </div>
                                            <asp:Label ID="Label8" runat="server" Text="Channel :" CssClass="col-sm-1 control-label" />
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlSearchChannel" runat="server" CssClass="form-control" />
                                            </div>
                                            <asp:Label ID="lblUsers" runat="server" Text="User :" CssClass="col-sm-1 control-label" />
                                            <div class="col-sm-2">
                                                <asp:ListBox ID="lstUsers" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblStatus" runat="server" Text="Status :" CssClass="col-sm-1 control-label" />
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Unapprove"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Requested"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-info" OnClick="btnDisplay_Click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
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
                    <div class="table-responsive">
                        <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSearchFilm" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                        <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Lan_Short_film_Id") %>' Visible="false" />
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Short_film_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tag">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Language">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridCategoryId" runat="server" Text='<%#Eval("Category") %>' Visible="false" />
                                                        <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language_Name") %>' />
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
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <span onclick="return confirm('Are you sure!... you want to Delete?')">   
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-remove" CommandName='<%#Eval("Lan_Short_film_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
                                                            </span>
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
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstUsers]').multiselect({
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
                    $('[id*=lstUsers]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
    </script>
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
                                <h4 id="myModalLabel" class="modal-title">Views</h4>
                            </div>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <asp:Label ID="lblDumpEmail" runat="server" Visible="false"></asp:Label>
                                    <asp:GridView ID="gvViews" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        CssClass="table table-bordered table-hover table-striped">
                                        <Columns>
                                            <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />--%>
                                            <asp:TemplateField HeaderText="S. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Username">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridUsername" runat="server" Text='<%#Eval("Username") %>' />
                                                    <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IPAddress">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridIPAddress" runat="server" Text='<%#Eval("IPAddress") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date & Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCity_Id" runat="server" Text='<%#Eval("City_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>List is Empty</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-info" OnClick="btnSend_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

