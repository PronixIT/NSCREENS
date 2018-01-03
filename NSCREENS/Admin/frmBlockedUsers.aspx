<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmBlockedUsers.aspx.cs" Inherits="Admin_frmAdvatizmentReport" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
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
                    <h3 class="panel-title">Search User</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchName" runat="server" CssClass="col-sm-1 control-label" Text="Name"></asp:Label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-info pull-right"
                                                    CommandName="Save" OnClick="btn_Click" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="box-footer">
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
                    <h3 class="panel-title">Blocked User List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="false" OnRowCommand="gvUserList_RowCommand"
                                            CssClass="table table-bordered table-hover table-striped" EmptyDataText="No records Found">
                                            <RowStyle />

                                            <Columns>
                                                <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />--%>
                                                <asp:TemplateField HeaderText="S.No:.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("Register_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:ImageField DataImageUrlField="Photo" ControlStyle-Width="100"
                                            ControlStyle-Height="100" HeaderText="Preview Image" />--%>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile_No" runat="server" Text='<%#Eval("Mobile_No") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmailId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridUsername" runat="server" Text='<%#Eval("Username") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Password" HeaderText="Password" ControlStyle-CssClass="form-control" ReadOnly />
                                                <asp:TemplateField HeaderText="Active">
                                            <ItemTemplate>
                                                <span onclick="return confirm('Are you sure you want to Active this user?')">
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="detail" OnClick="btnDelete_Click"  ToolTip="Active User" CssClass="glyphicon glyphicon-refresh" />
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
                                <h4 id="myModalLabel" class="modal-title"><asp:Label ID="lblName" runat="server"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUsrname" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Block :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesState" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="State" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoState" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="State" TabIndex="502" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btnUpdate_Click" TabIndex="504" />
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
</asp:Content>

