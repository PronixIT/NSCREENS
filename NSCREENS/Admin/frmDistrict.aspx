<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmDistrict.aspx.cs" Inherits="Admin_frmDistrict" EnableEventValidation="false" %>

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
    </script>
    <div class="row">
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New District Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblState" runat="server" Text="State/UT :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" TabIndex="2" />
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
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">District List</h3>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <asp:UpdatePanel ID="uplSearchDistrict" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Search State Wise :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlSearchState" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearchState_SelectedIndexChanged" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server">
                        <asp:UpdatePanel ID="UplState" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvDistrict_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
                                        <asp:TemplateField HeaderText="S. No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State/UT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridState" runat="server" Text='<%#Eval("State_Name") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDistrict" runat="server" Text='<%#Eval("District_Name") %>' />
                                                <asp:Label ID="lblGridDistrictId" runat="server" Text='<%#Eval("District_Id") %>'
                                                    Visible="false" />
                                                <asp:Label ID="lblGridStateId" runat="server" Text='<%#Eval("State_Id") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDistrictIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                                <h4 id="myModalLabel" class="modal-title">Update District</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateState" runat="server" Text="State/UT :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUpdateState" runat="server" CssClass="form-control" TabIndex="501" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateDistrict" runat="server" CssClass="form-control" TabIndex="502" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                            <asp:Label ID="lblDumpStateId" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6 radioer">
                                            <asp:RadioButton ID="rdbActiveYesDistrict" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                GroupName="District" TabIndex="503" />
                                            <asp:RadioButton ID="rdbActiveNoDistrict" runat="server" Text="No" CssClass="radio radio-inline"
                                                GroupName="District" TabIndex="503" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="506" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

