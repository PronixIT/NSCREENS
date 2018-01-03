<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAllAdvatizment.aspx.cs" Inherits="Admin_frmHome" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="bs-example bs-example-tabs" data-example-id="togglable-tabs">
        <ul class="nav nav-tabs" id="myTabs" role="tablist">
            <li role="presentation" class="active"><a href="#tab4" role="tab" id="Rtab4" data-toggle="tab" aria-controls="tab4" aria-expanded="false">Advertisements</a></li>
            <li role="presentation"><a href="#tab1" id="rtab1" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Recharge</a></li>
            <li role="presentation"><a href="#tab2" id="Rtab2" role="tab" data-toggle="tab" aria-controls="tab2" aria-expanded="true">Refill Wallet</a></li>
            <li role="presentation" class=""><a href="#tab3" role="tab" id="Rtab3" data-toggle="tab" aria-controls="tab3" aria-expanded="false">Recharge History</a></li>
            <li role="presentation" class=""><a href="#Transactions" role="tab" id="Transactions1" data-toggle="tab" aria-controls="tab3" aria-expanded="false">Transactions</a></li>

        </ul>
        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade" role="tabpanel" id="tab1" aria-labelledby="home-tab">
                <br />
                <br />
                <div class="col-xs-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Recharge</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:RadioButton ID="rdbprepaid" runat="server" CssClass="radio-inline" Text="Prepaid" GroupName="Recharge" Checked="true" OnCheckedChanged="rdbpaid_CheckedChanged" AutoPostBack="true" />
                                                        <asp:RadioButton ID="rdbPostpaid" runat="server" CssClass="radio-inline" Text="Postpaid" GroupName="Recharge" OnCheckedChanged="rdbpaid_CheckedChanged" AutoPostBack="true" />
                                                        <asp:RadioButton ID="rdbDTH" runat="server" CssClass="radio-inline" Text="DTH" GroupName="Recharge" OnCheckedChanged="rdbpaid_CheckedChanged" AutoPostBack="true" />
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblBank" runat="server" Text="Mobile Number :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" TabIndex="1" MaxLength="10" oninput="this.value = this.value.replace(/[^0-9.]/g, ''); this.value = this.value.replace(/(\..*)\./g, '$1');" onblur="phonenumber(this);" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Operator :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddloperator" runat="server" CssClass="form-control" OnSelectedIndexChanged="getoffers_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label10" runat="server" Text="Area :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control" OnSelectedIndexChanged="getoffers_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="Andhra Pradesh" Text="Andhra Pradesh"></asp:ListItem>
                                                            <asp:ListItem Value="Telangana">Telangana</asp:ListItem>
                                                            <asp:ListItem Value="Assam">Assam</asp:ListItem>
                                                            <asp:ListItem Value="Bihar Jharkhand">Bihar &amp; Jharkhand</asp:ListItem>
                                                            <asp:ListItem Value="Chennai">Chennai</asp:ListItem>
                                                            <asp:ListItem Value="Delhi NCR">Delhi NCR</asp:ListItem>
                                                            <asp:ListItem Value="Gujarat">Gujarat</asp:ListItem>
                                                            <asp:ListItem Value="Himachal Pradesh">Himachal Pradesh</asp:ListItem>
                                                            <asp:ListItem Value="Haryana">Haryana</asp:ListItem>
                                                            <asp:ListItem Value="Jammu Kashmir">Jammu &amp; Kashmir</asp:ListItem>
                                                            <asp:ListItem Value="Karnataka">Karnataka</asp:ListItem>
                                                            <asp:ListItem Value="Kerala">Kerala</asp:ListItem>
                                                            <asp:ListItem Value="Kolkata">Kolkata</asp:ListItem>
                                                            <asp:ListItem Value="Maharashtra Goa">Maharashtra &amp; Goa</asp:ListItem>
                                                            <asp:ListItem Value="Madhya Pradesh Chhattisgarh">Madhya Pradesh &amp; Chhattisgarh</asp:ListItem>
                                                            <asp:ListItem Value="Mumbai">Mumbai</asp:ListItem>
                                                            <asp:ListItem Value="North East">North East</asp:ListItem>
                                                            <asp:ListItem Value="North East 1">North East 1</asp:ListItem>
                                                            <asp:ListItem Value="North East 2">North East 2</asp:ListItem>
                                                            <asp:ListItem Value="Odisha">Odisha</asp:ListItem>
                                                            <asp:ListItem Value="Punjab">Punjab</asp:ListItem>
                                                            <asp:ListItem Value="Rajasthan">Rajasthan</asp:ListItem>
                                                            <asp:ListItem Value="Tamil Nadu">Tamil Nadu</asp:ListItem>
                                                            <asp:ListItem Value="RUttar Pradesh East">Uttar Pradesh East</asp:ListItem>
                                                            <asp:ListItem Value="Uttar Pradesh West & Uttarakhand">Uttar Pradesh West &amp; Uttarakhand</asp:ListItem>
                                                            <asp:ListItem Value="West Bengal">West Bengal</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" Text="Amount :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="3" oninput="this.value = this.value.replace(/[^0-9.]/g, ''); this.value = this.value.replace(/(\..*)\./g, '$1');" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="box-footer">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                                    CommandName="Clear" OnClick="btn_Click" />

                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info  pull-right"
                                                    CommandName="Prepaid" OnClick="btn_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-6">

                    <asp:GridView ID="gdvoffers" runat="server" AutoGenerateColumns="False" GridLines="None"
                        CssClass="table table-bordered table-hover table-striped" EmptyDataText="No Records Found">

                        <Columns>
                            <asp:TemplateField HeaderText="S. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operator">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridOperator" runat="server" Text='<%#Eval("OperatorName") %>' />
                                    <asp:Label ID="lblOffer_Id" runat="server" Text='<%#Eval("Offer_Id") %>' Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridArea" runat="server" Text='<%#Eval("Area") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Operator">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridOperator" runat="server" Text='<%#Eval("OperatorName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Recharge Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridRechargeType" runat="server" Text='<%#Eval("RechargeType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TT Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridTT_Type" runat="server" Text='<%#Eval("TT_Type") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridDays" runat="server" Text='<%#Eval("Days") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Is Active">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridDistrictIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="tab-pane fade" role="tabpanel" id="tab2" aria-labelledby="home-tab">
                <br />
                <br />
                <div class="col-xs-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <asp:UpdatePanel ID="uplr" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblType" runat="server" Text="Refill Wallet"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnlGrid" runat="server">
                                <asp:UpdatePanel ID="UplState" runat="server">
                                    <ContentTemplate>
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-sm-12">
                                                    <div id="gdvrefillwallet" runat="server">
                                                        <div class="form-horizontal">
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="form-horizontal">
                                                                            <div class="form-group">
                                                                                <asp:Label ID="lblEarningAmt" runat="server" CssClass="control-label col-sm-4" Text="Earning Amount : "></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtEarningAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                    <asp:Label ID="lblDumpEarningAmt" runat="server" Visible="false"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <asp:Label ID="lblTransferAmount" runat="server" CssClass="control-label col-sm-4" Text="Transfer Amount : "></asp:Label>
                                                                                <div class="col-sm-8">
                                                                                    <asp:TextBox ID="txtTransferAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" CssClass="btn btn-info" OnClick="btnTransfer_Click" />
                                                                            <%--<asp:Button ID="btnNextButton" runat="server" Text="Next" CssClass="btn btn-danger pull-right" OnClick="btnNextButton_Click" />--%>

                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" role="tabpanel" id="tab3" aria-labelledby="home-tab">
                <br />
                <br />
                <div class="row">
                    <asp:GridView ID="gdvHistory" runat="server"
                        AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered table-hover table-striped" EmptyDataText="No Records Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="MobileNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridMobileNo" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                    <asp:Label ID="lblGridID" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operator Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridOperatorName" runat="server" Text='<%#Eval("OperatorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prev Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridPrevBalance" runat="server" Text='<%#Eval("PrevBalance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridBalance" runat="server" Text='<%#Eval("Balance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="tab-pane fade  active in" role="tabpanel" id="tab4" aria-labelledby="home-tab">
                <br />
                <p><b>Note :</b> 2/- needed to watch a film</p>
                <br />
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <div class="latst-vid secondary-vid">
                            <div class="vid-heading overflow-hidden">
                                <span class="wow fadeInUp" data-wow-duration="0.8s">Advertisements</span>
                                <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                            </div>

                            <div class="vid-container">
                                <div class="form-horizontal">
                                    <asp:ListView ID="lstRecentVideos" runat="server">
                                        <ItemTemplate>
                                            <div class="blog-list-container listing-container" style="display: inline;">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <%--style="border: solid 1px; border-color: #bcc1c0;"--%>
                                                        <div class="col-sm-2">
                                                            <div class="vid-img">
                                                                <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="Add image" style="height: 130px; width: 142.5px;">
                                                                <asp:LinkButton class="play-icon play-small-icon" ID="play" runat="server" OnClick="play_Click">
                                                                    <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                                                    <asp:Label ID="lblURLPlay" runat="server" Visible="false" Text='<%#Eval("shortfilm") %>'></asp:Label>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-10">
                                                            <div class="blog-text">
                                                                <h1 class="sm-heading">
                                                                    <asp:LinkButton ID="lnkplay" runat="server" OnClick="play_Click">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </h1>
                                                                <p>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                                </p>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <%--<div class="vid-container">
                        <asp:ListView ID="lstRecentVideos" runat="server" GroupItemCount="1">
                            <ItemTemplate>
                                <div class="col-md-2 col-sm-3">
                                    <div class="latest-vid-img-container">
                                        <div class="vid-img">
                                            <img class="img-responsive" src='<%#Eval("Short_film_Image") %>' alt="video image" style="height:200px;width:142.5px;">
                                            <asp:LinkButton class="play-icon play-small-icon" id="play" runat="server" onclick="play_Click">
                                                <img class="img-responsive play-svg svg" src="../images/play-button.svg" alt="play" onerror="this.src='images/play-button.png'">
                                                <asp:Label ID="lblURLPlay" runat="server" Visible="false" Text='<%#Eval("shortfilm") %>'></asp:Label>
                                            </asp:LinkButton>
                                            <div class="overlay-div"></div>
                                        </div>
                                        <div class="vid-text" style="width:142.5px;">
                                            <h1><asp:LinkButton ID="lnkplay" runat="server" OnClick="play_Click">
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Title") %>'></asp:Label></asp:LinkButton></h1>
                                            <p class="vid-info-text">
                                                <span>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Visits") %>'></asp:Label>
                                                    views</span>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane fade" role="tabpanel" id="Transactions" aria-labelledby="home-tab">
                <br />
                <br />
                <div class="row">
                    <asp:GridView ID="gdvTran" runat="server"
                        AutoGenerateColumns="False" GridLines="None" CssClass="table table-bordered table-hover table-striped" EmptyDataText="No Records Found">
                        <Columns>
                            <asp:TemplateField HeaderText="S. No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Earnings">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridPage" runat="server" Text='<%#Eval("Page") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridPageType" runat="server" Text='<%#Eval("PageType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridTransaction_Id" runat="server" Text='<%#Eval("Transaction_Id") %>'></asp:Label>
                                    <asp:Label ID="lblGridID" runat="server" Text='<%#Eval("Id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridDebit" runat="server" Text='<%#Eval("Debit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridCredit" runat="server" Text='<%#Eval("Credit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblGridDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <hr />
    </div>
</asp:Content>

