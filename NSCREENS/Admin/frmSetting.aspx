<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmSetting.aspx.cs" Inherits="Admin_frmSetting" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Settings</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                         <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-3">
                                                <b>Email</b>
                                            </div>
                                             <div class="col-sm-3">
                                                <b>SMS</b>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblCountry" runat="server" Text="Register :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkRegisterEmail" runat="server" CssClass="checkbox"/>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkRegisterSMS" runat="server" CssClass="checkbox" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Change Password :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkChangePasswordEmail" runat="server" CssClass="checkbox"/>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkChangePasswordSMS" runat="server" CssClass="checkbox" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Forget Password :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkForgetPasswordEmail" runat="server" CssClass="checkbox"/>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkForgetPasswordSMS" runat="server" CssClass="checkbox" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Wallet Amount :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkWalletAmountEmail" runat="server" CssClass="checkbox"/>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:CheckBox ID="chkWalletAmountSMS" runat="server" CssClass="checkbox" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
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
</asp:Content>

