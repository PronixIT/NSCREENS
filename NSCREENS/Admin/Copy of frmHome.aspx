<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="Copy of frmHome.aspx.cs" Inherits="Admin_frmHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopupvendor() {
            $('#myModalVendor').modal('show');

            $('#myModalVendor').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <div class="row">
        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
            <a href="#">
                <div class="card red summary-inline">
                    <div class="card-body">
                        <i class="icon fa fa-bus fa-4x"></i>
                        <div class="content">
                            <div class="title">
                                <asp:Label ID="lblTotalBuses" runat="server"></asp:Label>
                            </div>
                            <div class="sub-title">
                                Total Buses</div>
                        </div>
                        <div class="clear-both">
                        </div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
            <a href="#">
                <div class="card yellow summary-inline">
                    <div class="card-body">
                        <i class="icon fa fa-comments fa-4x"></i>
                        <div class="content">
                            <div class="title">
                                <asp:Label ID="lblRunningBuses" runat="server"></asp:Label></div>
                            <div class="sub-title">
                                Running Buses</div>
                        </div>
                        <div class="clear-both">
                        </div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
            <a href="#">
                <div class="card green summary-inline">
                    <div class="card-body">
                        <i class="icon fa fa-tags fa-4x"></i>
                        <div class="content">
                            <div class="title">
                                <asp:Label ID="lblVacantBuses" runat="server"></asp:Label></div>
                            <div class="sub-title">
                                Vacant Buses</div>
                        </div>
                        <div class="clear-both">
                        </div>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
            <a href="#">
                <div class="card blue summary-inline">
                    <div class="card-body">
                        <i class="icon glyphicon glyphicon-ban-circle fa-4x"></i>
                        <div class="content">
                            <div class="title">
                                <asp:Label ID="lblHoldedBuses" runat="server"></asp:Label></div>
                            <div class="sub-title">
                                Holded Buses</div>
                        </div>
                        <div class="clear-both">
                        </div>
                    </div>
                </div>
            </a>
        </div>
    </div>
    <div class="row  no-margin-bottom">
        <div class="col-sm-6 col-xs-12">
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel panel-warning">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Tomorrow releasing bus list</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnlRelesing" runat="server" Height="250px">
                                <asp:UpdatePanel ID="UplState" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvNewBus" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                CssClass="table table-bordered table-hover table-striped">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />
                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bus Depot Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridBusDepot_Name" runat="server" Text='<%#Eval("BusDepot_Name") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridBusModelType_Name" runat="server" Text='<%#Eval("Count1") %>' />
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
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Billing Details</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnlBilling" runat="server" Height="250px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                CssClass="table table-bordered table-hover table-striped">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridCompany_Name" runat="server" Text='<%#Eval("Company_Name") %>' />
                                                            <asp:Label ID="lblGridNew_Quotation_Id" runat="server" Text='<%#Eval("New_Quotation_Id") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblGridCustomerID" runat="server" Text='<%#Eval("Campaign_Id") %>'
                                                                Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridContactPerson" runat="server" Text='<%#Eval("Contact_Person") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridUnitPrice" runat="server" Text='<%#Eval("Grass") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("ToDate") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open"
                                                                CommandName='<%#Eval("New_Quotation_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
                                                            <asp:Label ID="lblApprove" runat="server" Text='<%#Eval("Approved") %>' Visible="false"></asp:Label>
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
        <div class="col-sm-6 col-xs-12">
            <div class="row">
                <div class="col-xs-12">
                    <div class="card primary">
                        <div role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#unallocatebuses" aria-controls="SearchBuses"
                                    role="tab" data-toggle="tab" aria-expanded="true">Unallocated Work</a></li>
                                <li role="presentation"><a href="#SearchBuses" aria-controls="SearchBuses" role="tab"
                                    data-toggle="tab" aria-expanded="true">On Going Work</a></li>
                                <li role="presentation" class=""><a href="#PrepareQuotation" aria-controls="PrepareQuotation"
                                    role="tab" data-toggle="tab" aria-expanded="false">Pending Work</a></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane active" id="unallocatebuses">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <asp:Panel ID="Panel2" runat="server" Height="240px">
                                                    <asp:GridView ID="gvunallocateQuotations" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-hover" GridLines="None">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblGridNew_Quotation_Id" runat="server" Text='<%#Eval("New_Quotation_Id") %>'
                                                                        Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quotation No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridQuotationNo" runat="server" Text='<%#Eval("QuotationNo") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Client Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridCompany_Name" runat="server" Text='<%#Eval("Company_Name") %>' />
                                                                    <asp:Label ID="lblGridNew_Quotation_Id" runat="server" Text='<%#Eval("New_Quotation_Id") %>'
                                                                        Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Campaign Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridCampaign_Name" runat="server" Text='<%#Eval("Campaign_Name") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridFromDate" runat="server" Text='<%#Eval("FromDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open"
                                                                        OnClick="lnkView_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="SearchBuses">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <asp:Panel ID="pnlGoing" runat="server" Height="240px">
                                                    <asp:GridView ID="gvVendors" runat="server" GridLines="None" CssClass="table table-hover"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblGridVendor_Id" runat="server" Text='<%#Eval("Vendor_Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGridVendor_Payment_Id" runat="server" Text='<%#Eval("Vendor_Payment_Id") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PO No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridPONo" runat="server" Text='<%#Eval("PONo") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridFirst_Name" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridMobile_No" runat="server" Text='<%#Eval("Mobile_No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nature of Job">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridAddNatureofJob_Name" runat="server" Text='<%#Eval("AddNatureofJob_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Campaign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridCampaign_Name" runat="server" Text='<%#Eval("Campaign_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="End Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridEnd_Date" runat="server" Text='<%#Eval("End_Date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open"
                                                                        OnClick="lnkViewVendor_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="PrepareQuotation">
                                    <asp:Panel ID="pnlPending" runat="server" Height="250px">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvPendingWork" runat="server" GridLines="None" CssClass="table table-hover"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                    <asp:Label ID="lblGridVendor_Id" runat="server" Text='<%#Eval("Vendor_Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGridVendor_Payment_Id" runat="server" Text='<%#Eval("Vendor_Payment_Id") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PO No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridPONo" runat="server" Text='<%#Eval("PONo") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridFirst_Name" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridMobile_No" runat="server" Text='<%#Eval("Mobile_No") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nature of Job">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridAddNatureofJob_Name" runat="server" Text='<%#Eval("AddNatureofJob_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Campaign">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridCampaign_Name" runat="server" Text='<%#Eval("Campaign_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="End Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridEnd_Date" runat="server" Text='<%#Eval("End_Date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open"
                                                                        OnClick="lnkViewVendor_Click"></asp:LinkButton>
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
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Pending Quotations</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnlPendingQuotations" runat="server" Height="250px">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvPendingQuotations" runat="server" AutoGenerateColumns="False"
                                                GridLines="None" CssClass="table table-bordered table-hover table-striped">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridCompany_Name" runat="server" Text='<%#Eval("Company_Name") %>' />
                                                            <asp:Label ID="lblGridNew_Quotation_Id" runat="server" Text='<%#Eval("New_Quotation_Id") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblGridCustomerID" runat="server" Text='<%#Eval("Campaign_Id") %>'
                                                                Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridContactPerson" runat="server" Text='<%#Eval("Contact_Person") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridUnitPrice" runat="server" Text='<%#Eval("Grass") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Purchase Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurchase_Order_No" runat="server" Text='<%#Eval("Purchase_Order_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" CssClass="glyphicon glyphicon-eye-open"
                                                                    CommandName='<%#Eval("New_Quotation_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
                                                                <asp:Label ID="lblApprove" runat="server" Text='<%#Eval("Approved") %>' Visible="false"></asp:Label>
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
    </div>
    <div id="myModal" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="uplUpdate" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">
                                    Quotation Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Image ID="imglogo" runat="server" />
                                    </div>
                                    <div class="col-md-4 pull-right">
                                        <dl class="dl-horizontal">
                                            <dt>Quotation Date:</dt>
                                            <dd>
                                                <asp:Label ID="lblQuotationDate" runat="server"></asp:Label></dd>
                                            <dt>Quotation Number:</dt>
                                            <dd>
                                                <asp:Label ID="lblQuotationNumber" runat="server"></asp:Label></dd>
                                            <dt>No.of Days:</dt>
                                            <dd>
                                                <asp:Label ID="lblNoofDays" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-md-12">
                                        <address>
                                            To,<br>
                                            <strong>
                                                <asp:Label ID="lblCompanyName" runat="server"></asp:Label></strong><br>
                                            <asp:Label ID="lblAddress" runat="server"></asp:Label><br>
                                            <abbr title="Phone">
                                                P:</abbr>
                                            <asp:Label ID="lblmobileno" runat="server"></asp:Label>
                                            <asp:Label ID="lblDumpQuotationId" runat="server" Visible="false"></asp:Label>
                                        </address>
                                    </div>
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView ID="gvBuses" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus Depot">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBusDepot_Name" runat="server" Text='<%#Eval("BusDepot_Name") %>' />
                                                        <asp:Label ID="lblGridRunning_Bus_Id" runat="server" Text='<%#Eval("Running_Bus_Id") %>'
                                                            Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus Model Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBusModelType_Name" runat="server" Text='<%#Eval("BusModelType_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridRequired_Qty" runat="server" Text='<%#Eval("Required_Qty") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Mounting">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridMounting_Chrg" runat="server" Text='<%#Eval("Mounting_Chrg") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Printing & Mounting">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPrinting_Chrg" runat="server" Text='<%#Eval("PMCharges") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTotalAmount" runat="server" Text='<%#Eval("TotalAmount") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-4 pull-right">
                                        <dl class="dl-horizontal">
                                            <dt>Gross Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblGrossAmount" runat="server"></asp:Label></dd>
                                            <dt>Tax Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblTaxAmount" runat="server"></asp:Label></dd>
                                            <dt>Net Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblNetAmount" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="myModalVendor" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="H1" class="modal-title">
                                    PO Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:Image ID="ImgComapny" runat="server" />
                                    </div>
                                    <div class="col-md-4 pull-right">
                                        <dl class="dl-horizontal">
                                            <dt>PO Date:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorPODate" runat="server"></asp:Label></dd>
                                            <dt>PO Number:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorPONumber" runat="server"></asp:Label></dd>
                                                 <dt>Campaign:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorCampaign" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                    <div class="col-md-12">
                                        <address>
                                            To,<br>
                                            <strong>
                                                <asp:Label ID="lblVendorName" runat="server"></asp:Label></strong><br>
                                            <asp:Label ID="lblVendorCity" runat="server"></asp:Label><br>
                                            <abbr title="Phone">
                                                P:</abbr>
                                            <asp:Label ID="lblVendorPhoneNo" runat="server"></asp:Label>
                                        </address>
                                    </div>
                                    <div class="col-md-12 table-responsive">
                                        <asp:GridView ID="gvDisplayBuses" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus Depot Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBusDepot_Name" runat="server" Text='<%#Eval("BusDepot_Name") %>' />
                                                        <asp:Label ID="lblGridNew_Bus_Id" runat="server" Text='<%#Eval("New_Bus_Id") %>'
                                                            Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus Model Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBusModelType_Name" runat="server" Text='<%#Eval("BusModelType_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bus Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBusNumber" runat="server" Text='<%#Eval("BusNumber") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route From">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridRouteFrom" runat="server" Text='<%#Eval("RouteFrom") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridRouteTo" runat="server" Text='<%#Eval("RouteTo") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-md-12">
                                          <dl class="dl-horizontal">
                                            <dt>Description:</dt>
                                            <dd>
                                                <asp:Label ID="lblDescription" runat="server"></asp:Label></dd>
                                                </dl>
                                    </div>
                                    <div class="col-md-4 pull-right">
                                        <dl class="dl-horizontal">
                                            <dt>Gross Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorGrossAmount" runat="server"></asp:Label></dd>
                                            <dt>TDS Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorTDSAmount" runat="server"></asp:Label></dd>
                                            <dt>Net Amount:</dt>
                                            <dd>
                                                <asp:Label ID="lblVendorNetAmount" runat="server"></asp:Label></dd>
                                        </dl>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
