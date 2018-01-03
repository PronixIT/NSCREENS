<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmProfile.aspx.cs" Inherits="Admin_frmProfile" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Profile</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server">
                            <div class="col-xs-3">
                                <div class="form-horizontal">
                                    <div class="col-sm-12">
                                        <asp:Image ID="imgPhoto" runat="server" CssClass="thumbnail pull-right" Width="200px" Height="200px" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-9">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Name :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="102" />
                                            <asp:Label ID="lblRegisterId" runat="server" Visible="false" Text="0" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Mobile Number :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" TabIndex="102" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Email Id :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" TabIndex="102" />
                                            <asp:Label ID="lblDumpEmailId" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" Text="State/UT :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="Pincode :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"
                                                TabIndex="102" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblDateofBirth" runat="server" Text="Date of Birth :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDateofBirth" runat="server" class="dtpSelData form-control" TabIndex="102" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Photo :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:FileUpload ID="fudPhoto" runat="server" onchange="showimagepreview(this)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

