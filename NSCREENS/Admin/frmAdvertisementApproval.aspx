<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAdvertisementApproval.aspx.cs" Inherits="Admin_frmAdvertisementApproval" EnableEventValidation="false" %>

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

        function divPopupView() {
            $('#myModalView').modal('show');

            $('#myModalView').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Advertisement List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvadvertisement" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvadvertisement_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" HeaderText="Edit" ControlStyle-CssClass="glyphicon glyphicon-edit" />
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                        <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Advertisement_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tag">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Visits">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridNoofVisits" runat="server" Text='<%#Eval("NoofVisits") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridStartDate" runat="server" Text='<%#Eval("StartDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridEndDate" runat="server" Text='<%#Eval("EndDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBudget" runat="server" Text='<%#Eval("Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promo Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromoCode" runat="server" Text='<%#Eval("PromoCode") %>' />
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
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open" OnClick="lnkView_Click"></asp:LinkButton>
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
                                <h4 id="myModalLabel" class="modal-title">Approval Advertisement</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtAppTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblAppStatus" runat="server" Text="Status :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlAppStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Approve"></asp:ListItem>
                                                <asp:ListItem Text="Unapprove"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAdvertisementId" runat="server" Visible="false" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Submit" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btnApproval_Click" TabIndex="504" />
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
    <div id="myModalView" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">View</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Title :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="lblNoofVisits" runat="server" Text="No.of Visits :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtNoofVisits" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="lblEndDate" runat="server" Text="End Date :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <asp:Label ID="lblGender" runat="server" Text="Gender :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblAge" runat="server" Text="Age :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="hide">
                                        <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                            </div>
                                        <asp:Label ID="Label2" runat="server" Text="Email :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" TextMode="SingleLine"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="form-group">
                                     <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="col-sm-2 control-label" />
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                           <div id="add" runat="server"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

