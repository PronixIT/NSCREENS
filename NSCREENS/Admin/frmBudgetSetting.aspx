<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmBudgetSetting.aspx.cs" Inherits="Admin_frmBudgetSetting" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:UpdatePanel ID="ipld" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTitle" runat="server" Text="Budget Settings"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h3>
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
                                                <asp:TextBox ID="txtAddBudget" runat="server" CssClass="form-control" TabIndex="1" />
                                                <asp:Label ID="lblDumpBudId" runat="server" Text="0" Visible="false"/>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblShortFilm" runat="server" Text="Short Film % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtShortFilm" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblAdmin" runat="server" Text="Admin % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtAdmin" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Video Sharing % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtVideoSharing" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblPromoter" runat="server" Text="Promoter % :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtPromoter" runat="server" CssClass="form-control" TabIndex="1" />
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
                    <h3 class="panel-title">Budget Settings</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <asp:GridView ID="gvSettings" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvSettings_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBudget" runat="server" Text='<%#Eval("Budget") %>' />
                                                        <asp:Label ID="lblGridBudget_Settings_Id" runat="server" Text='<%#Eval("Budget_Settings_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Short Film(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridShort_Film" runat="server" Text='<%#Eval("Short_Film") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Admin(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridAdmin" runat="server" Text='<%#Eval("Admin") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promoter(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromoter" runat="server" Text='<%#Eval("Promoter") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Video Sharing(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridVideo_Sharing" runat="server" Text='<%#Eval("Video_Sharing") %>' />
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
</asp:Content>

