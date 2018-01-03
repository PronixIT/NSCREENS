<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmBudgetSetting1.aspx.cs" Inherits="Admin_frmBudgetSetting" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Budget Settings</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblAddBudget" runat="server" Text="Add Budget :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAddBudget" runat="server" CssClass="form-control" TabIndex="1" Text="2" Enabled="false" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblShortFilm" runat="server" Text="Short Film % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtShortFilm" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="1.2" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblAdmin" runat="server" Text="Admin % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAdmin" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0.8" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Video Sharing % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtVideoSharing" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblPromoter" runat="server" Text="Promoter % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoter" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="Clear" OnClick="btn_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                            CommandName="Save" OnClick="btn_Click" />
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-danger pull-right"
                                            CommandName="Edit" OnClick="btn_Click" />
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
                    <h3 class="panel-title">Budget Settings With Promoter</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Add Budget :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoterAddBudget" runat="server" CssClass="form-control" TabIndex="1" Text="2" Enabled="false" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Text="Short Film % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoterShortFilm" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="1.2" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Text="Admin % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoterAdmin" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0.8" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Text="Video Sharing % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtpromoterVideoSharing" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Text="Promoter % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoterPromoter" runat="server" CssClass="form-control" TabIndex="1" Enabled="false" Text="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="PromoterClear" OnClick="btn_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                            CommandName="PromoterSave" OnClick="btn_Click" />
                                        <asp:Button ID="btnPromoterEdit" runat="server" Text="Edit" CssClass="btn btn-danger pull-right"
                                            CommandName="PromoterEdit" OnClick="btn_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

