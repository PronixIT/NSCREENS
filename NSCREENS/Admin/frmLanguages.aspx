<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmLanguages.aspx.cs" Inherits="Admin_frmLanguage" EnableEventValidation="false" %>

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
                    <h3 class="panel-title">New Language Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label ID="lblUserCode" runat="server" Text="User Code :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtUserCode" runat="server" TextMode="Password" CssClass="form-control"
                                                    TabIndex="2" />
                                            </div>
                                        </div>--%>
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
                    <h3 class="panel-title">Language List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplLanguage" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLanguage" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvLanguage_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Language">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language_Name") %>' />
                                                        <asp:Label ID="lblGridLanguageId" runat="server" Text='<%#Eval("Language_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridLanguageIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
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
                                <h4 id="myModalLabel" class="modal-title">Update Language</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateLanguage" runat="server" Text="Language :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateLanguage" runat="server" CssClass="form-control" TabIndex="501" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesLanguage" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Language" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoLanguage" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Language" TabIndex="502" />
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
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

