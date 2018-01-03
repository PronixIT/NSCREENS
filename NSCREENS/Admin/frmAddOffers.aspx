<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddOffers.aspx.cs" Inherits="Admin_frmAddOffers" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        $(function () {
            $('[id*=lstRechargeType]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>
    <div class="row">
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New Offer Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblArea" runat="server" Text="Area :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" onchange="GetSelectedTextValue(this);">
                                                    <asp:ListItem Value="0" Text="Andhra Pradesh"></asp:ListItem>
                                                    <asp:ListItem Value="RA" Text="airtel">Telangana</asp:ListItem>
                                                    <asp:ListItem Value="RC" Text="aircel">Assam</asp:ListItem>
                                                    <asp:ListItem Value="TB" Text="bsnl">Bihar &amp; Jharkhand</asp:ListItem>
                                                    <asp:ListItem Value="RB" Text="bsnl">Chennai</asp:ListItem>
                                                    <asp:ListItem Value="TD" Text="tata-docomo">Delhi NCR</asp:ListItem>
                                                    <asp:ListItem Value="RD" Text="tata-indicom">Gujarat</asp:ListItem>
                                                    <asp:ListItem Value="RI" Text="idea">Himachal Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="RJ" Text="jio">Haryana</asp:ListItem>
                                                    <asp:ListItem Value="RM" Text="mts">Jammu &amp; Kashmir</asp:ListItem>
                                                    <asp:ListItem Value="RR" Text="reliance-gsm">Karnataka</asp:ListItem>
                                                    <asp:ListItem Value="RU" Text="uninor-telenor">Kerala</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Kolkata</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Maharashtra &amp; Goa</asp:ListItem>
                                                    <asp:ListItem Value="RU" Text="uninor-telenor">Madhya Pradesh &amp; Chhattisgarh</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Mumbai</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">North East</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">North East 1</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">North East 2</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Odisha</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Punjab</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Rajasthan</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Tamil Nadu</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Uttar Pradesh East</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Uttar Pradesh West &amp; Uttarakhand</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">West Bengal</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblOperator" runat="server" Text="Operator :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblRechargeType" runat="server" Text="Recharge Type :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:ListBox ID="lstRechargeType" runat="server" SelectionMode="Multiple">
                                                    <asp:ListItem Value="1">Topup</asp:ListItem>
                                                    <asp:ListItem Value="2">SMS</asp:ListItem>
                                                    <asp:ListItem Value="3">2G</asp:ListItem>
                                                    <asp:ListItem Value="4">3G</asp:ListItem>
                                                    <asp:ListItem Value="5">4G</asp:ListItem>
                                                    <asp:ListItem Value="6">Local</asp:ListItem>
                                                    <asp:ListItem Value="7">STD</asp:ListItem>
                                                    <asp:ListItem Value="8">ISD</asp:ListItem>
                                                    <asp:ListItem Value="9">Other</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblAmount" runat="server" Text="Amount :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblTTType" runat="server" Text="TT Type :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlTTType" runat="server" CssClass="form-control" TabIndex="1">
                                                    <asp:ListItem>-- Select --</asp:ListItem>
                                                    <asp:ListItem>FULL TT</asp:ListItem>
                                                    <asp:ListItem>FULL TT+</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDay" runat="server" Text="Day :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDay" runat="server" CssClass="form-control" TabIndex="2" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TabIndex="2" />
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
                    <h3 class="panel-title">Offer List</h3>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlGrid" runat="server">
                        <asp:UpdatePanel ID="uplSearchDistrict" runat="server">
                            <ContentTemplate>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Search Operator Wise :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlSearchOperator" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSearchOperator_SelectedIndexChanged" />
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
                                        <asp:TemplateField HeaderText="Operator">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridOperator" runat="server" Text='<%#Eval("OperatorName") %>' />
                                                <asp:Label ID="lblOffer_Id" runat="server" Text='<%#Eval("Offer_Id") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Area">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridArea" runat="server" Text='<%#Eval("Area") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <%--<asp:TemplateField HeaderText="Operator">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridOperator" runat="server" Text='<%#Eval("OperatorName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                          <asp:TemplateField HeaderText="Recharge Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridRechargeType" runat="server" Text='<%#Eval("RechargeType") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="TT Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridTT_Type" runat="server" Text='<%#Eval("TT_Type") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDays" runat="server" Text='<%#Eval("Days") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <%--<asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDistrictIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
                                        <asp:Label ID="lblUpdateState" runat="server" Text="Operator :" CssClass="col-sm-4 control-label" />
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
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstRechargeType]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
    </script>
</asp:Content>

