<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="frmTitleRegister1.aspx.cs" Inherits="Admin_frmTitle" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <ol class="breadcrumb navbar-breadcrumb">
        <li class="active">Masters - Title Register</li>
    </ol>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-6">
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title">
                            New Title Entry</div>
                    </div>
                </div>
                <div class="card-body">
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
            <div class="card">
                <div class="card-header">
                    <div class="card-title">
                        <div class="title">
                            Title List</div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplTitle" runat="server">
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
                                <h4 id="myModalLabel" class="modal-title">
                                    Update Title</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateTitle" runat="server" CssClass="form-control" TabIndex="501" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesTitle" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Title" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoTitle" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Title" TabIndex="502" /></div>
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
