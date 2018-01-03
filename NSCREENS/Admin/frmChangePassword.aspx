<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="frmChangePassword.aspx.cs" Inherits="Password_frmChangePassword" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="container">
        <div class="well col-lg-12">
            <legend>Change Password</legend>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="lblOldPassword" runat="server" Text="Old Password:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TabIndex="1"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblNewPassword" runat="server" Text="New Password:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TabIndex="2"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TabIndex="3"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-4 col-sm-6 text-right">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info"
                                        OnClick="btnSubmit_Click" TabIndex="51" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <%--<div class="well col-lg-5 col-sm-offset-2">
            <legend>Change Usercode</legend>
            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSubmit">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Old Usercode:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtOldUsercode" runat="server" CssClass="form-control" TabIndex="1"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="New Usercode:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNewUsercode" runat="server" CssClass="form-control" TabIndex="2"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Confirm Usercode:" CssClass="col-sm-4 control-label" />
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtConfirmUsercode" runat="server" CssClass="form-control" TabIndex="3"
                                        TextMode="Password" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-4 col-sm-6 text-right">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-info"
                                        OnClick="btnSubmitUsercode_Click" TabIndex="51" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>--%>
    </div>
</asp:Content>
