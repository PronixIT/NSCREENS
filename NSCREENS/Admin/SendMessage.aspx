<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="Admin_SendMessage" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Namespace="MyControls" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Send Message</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblUserName" runat="server" Text="User Name :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" TabIndex="1" />
                                                <ajax:AutoCompleteExtender ID="aceCustomerName" runat="server" ServiceMethod="SearchCustomerName"
                                                            TargetControlID="txtUserName" MinimumPrefixLength="1" CompletionInterval="100"
                                                            EnableCaching="false" CompletionSetCount="10" />
                                            </div>
                                        </div>
                                            <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Subject :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TabIndex="1" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblMessage" runat="server" Text="Message :" CssClass="col-sm-2 control-label" />
                                            <div class="col-sm-10">
                                                <%--<asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" Height="200px" />--%>
                                                <custom:CustomEditor ID="txtMessage" runat="server"
                                                                Height="300px"  TabIndex="11" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="Clear" OnClick="btn_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-info pull-right"
                                            CommandName="Save" OnClick="btn_Click" />
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

