<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmUserList.aspx.cs" Inherits="Admin_frmAdvatizmentReport" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
         $(function () {
             $('[id*=lstUsers]').multiselect({
                  includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                 enableFiltering: true
             });
         });
    </script>
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Search User</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblSearchName" runat="server" CssClass="col-sm-1 control-label" Text="Name"></asp:Label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtSearchName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2"><asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-info pull-right"
                                            CommandName="Save" OnClick="btn_Click" /></div>
                                       
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                        
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">User List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="false"
                                    CssClass="table table-bordered table-hover table-striped" EmptyDataText="No records Found">
                                    <RowStyle />
                                    
                                    <Columns>
                                        <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />
                                        <asp:TemplateField HeaderText="S.No:.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Register_Id") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:ImageField DataImageUrlField="Photo" ControlStyle-Width="100"
                                            ControlStyle-Height="100" HeaderText="Preview Image" />--%>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobile_No" runat="server" Text='<%#Eval("Mobile_No") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="EmailId">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Username" HeaderText="Username" ControlStyle-CssClass="form-control" ReadOnly />
                                        <asp:BoundField DataField="Password" HeaderText="Password" ControlStyle-CssClass="form-control" ReadOnly />
                                        <%--<asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <span onclick="return confirm('Are you sure you want to delete this record?')">
                                                    <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" CssClass="btn btn-danger btn-group-sm" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstUsers]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstUsers]').multiselect({
                         includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
    </script>
</asp:Content>

