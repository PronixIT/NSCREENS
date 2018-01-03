<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="frmRecharge.aspx.cs" Inherits="frmRecharge" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">

        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>

    <style type="text/css">
        .panel-body .btn:not(.btn-block) {
            width: 120px;
            margin-bottom: 10px;
        }

        .panel-body .prepaidwell {
            height: 365px;
        }
    </style>
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <script type="text/javascript">
        function RemoveExtraDiv() {
            //------------------------------------------
            //Hides the breadcrumb from the iframe]
            $("#rechargeoffers").contents().find(".newfooter").hide();
            //------------------------------------------

        }
        function Removeimage() {
            $('dthofferslist #image-container set-full-height').css('display', 'none');

        }
        function GetSelectedTextValue(ddloperator) {
            RemoveExtraDiv();

            $('rechargeoffers #GoogleActiveViewClass').css('display', 'none');





            var selectedText = ddloperator.options[ddloperator.selectedIndex].innerHTML;
            document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "";
            if (selectedText == "Aircel") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/aircel/andra-pradesh-telangana";
            }
            else if (selectedText == "Airtel") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/airtel/andra-pradesh-telangana";
            }
            else if (selectedText == "BSNL Topup") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/bsnl/andra-pradesh-telangana";
            }
            else if (selectedText == "BSNL Recharge") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/bsnl/andra-pradesh-telangana";
            }
            else if (selectedText == "Docomo Topup") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/tata-docomo/andra-pradesh-telangana";
            }
            else if (selectedText == "Docomo Recharge(Indicom)") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/tata-indicom/andra-pradesh-telangana";
            }

            else if (selectedText == "Idea") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/idea/andra-pradesh-telangana";
            }
            else if (selectedText == "JIO") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/jio/andra-pradesh-telangana";
            }
            else if (selectedText == "MTS") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/mts/andra-pradesh-telangana";
            }
            else if (selectedText == "Reliance (GSM/CDMA)") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/reliance-gsm/andra-pradesh-telangana";
            }

            else if (selectedText == "Uninor Recharge") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/uninor-telenor/andra-pradesh-telangana";
            }
            else if (selectedText == "Uninor STV") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/uninor-telenor/andra-pradesh-telangana";
            }
            else if (selectedText == "Vodafone") {
                document.getElementById("ContentPlaceHolder1_rechargeoffers").src = "https://www.ireff.in/plans/vodafone/andra-pradesh-telangana";
            }
            window.parent.frames[1].location.reload(true);
        }
    </script>


    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>




    <div class="row">
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
                                            <asp:Label ID="Label3" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:RadioButton ID="rdbprepaid" runat="server" CssClass="radio-inline" Text="Prepaid" GroupName="Recharge" Checked="true" OnCheckedChanged="rdbprepaid_CheckedChanged" AutoPostBack="true" />
                                                <asp:RadioButton ID="rdbPostpaid" runat="server" CssClass="radio-inline" Text="Postpaid" GroupName="Recharge" OnCheckedChanged="rdbPostpaid_CheckedChanged" AutoPostBack="true"/>
                                                <asp:RadioButton ID="rdbDTH" runat="server" CssClass="radio-inline" Text="DTH" GroupName="Recharge" OnCheckedChanged="rdbDTH_CheckedChanged" AutoPostBack="true"/>
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
                                                <asp:DropDownList ID="ddloperator" runat="server" CssClass="form-control" onchange="GetSelectedTextValue(this);">
                                                   
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Text="Area :" CssClass="col-sm-4 control-label" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" onchange="GetSelectedTextValue(this);">
                                                    <asp:ListItem Value="0" Text="Andhra Pradesh"></asp:ListItem>
                                                    <asp:ListItem Value="RA" Text="airtel">Telangana</asp:ListItem>
                                                    <asp:ListItem Value="RC" Text="aircel">Assam</asp:ListItem>
                                                    <asp:ListItem Value="TB" Text="bsnl">Bihar &amp; Jharkhand</asp:ListItem>
                                                    <asp:ListItem Value="RB" Text="bsnl">Chennai</asp:ListItem>
                                                    <asp:ListItem Value="TD" Text="tata-docomo">Delhi NCR</asp:ListItem>
                                                    <asp:ListItem Value="RD" Text="tata-indicom">Gujarat</asp:ListItem>
                                                    <asp:ListItem Value="RI" Text="idea">Himachal Pradesh</asp:ListItem>
                                                    <asp:ListItem Value="RJ" Text="jio">Haryana</asp:ListItem>
                                                    <asp:ListItem Value="RM" Text="mts">Jammu &amp; Kashmir</asp:ListItem>
                                                    <asp:ListItem Value="RR" Text="reliance-gsm">Karnataka</asp:ListItem>
                                                    <asp:ListItem Value="RU" Text="uninor-telenor">Kerala</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Kolkata</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Maharashtra &amp; Goa</asp:ListItem>
                                                    <asp:ListItem Value="RU" Text="uninor-telenor">Madhya Pradesh &amp; Chhattisgarh</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Mumbai</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">North East</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">North East 1</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">North East 2</asp:ListItem>
                                                    <asp:ListItem Value="TU" Text="uninor-telenor">Odisha</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Punjab</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Rajasthan</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Tamil Nadu</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Uttar Pradesh East</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">Uttar Pradesh West &amp; Uttarakhand</asp:ListItem>
                                                    <asp:ListItem Value="RV" Text="vodafone">West Bengal</asp:ListItem>

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

                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-warning  pull-right"
                                            CommandName="Prepaid" OnClick="btn_Click" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Offers</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <iframe id="rechargeoffers" src="" width="100%" height="500" frameborder="0" style="border: none; overflow: hidden;" allowtransparency="true" runat="server" scrolling="yes"></iframe>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="clearfix"></div>


    <div class="col-xs-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Rechrge History</h3>
            </div>
            <div class="panel-body">
                <div class="table-responsive">
                    <asp:Panel ID="Panel2" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView2" runat="server" OnRowDataBound="OnRowDataBound" CellPadding="5"
                                        AutoGenerateColumns="False" GridLines="None" CssClass="table table-responsive table-bordered table-hover table-striped" EmptyDataText="No Records Found"
                                        HeaderStyle-BackColor="#337ab7" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" OnRowCommand="GridView2_RowCommand">

                                        <Columns>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridID" runat="server" Text='<%#Eval("ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Operator
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCode" runat="server" Text='<%#Eval("Code") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Req.Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCreatedDate" runat="server" Text='<%#Eval("Date") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Res.Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridResponseDate" runat="server" Text='<%#Eval("CreatedDate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Mobile_No
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridMobileNo" runat="server" Text='<%#Eval("MobileNo") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Amount
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridAmount" runat="server" Text='<%#Eval("Amount") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    TID
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridTXTID" runat="server" Text='<%#Eval("ClientTXTID") %>' />
                                                    <asp:Label ID="lblGridClientTXT" runat="server" Text='<%#Eval("ClientTXT") %>' Visible="false" />
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Status
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>' CssClass='<%#Eval("clr") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Opening Bal
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridOB" runat="server" Text='<%#Eval("OB") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Commission
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridC" runat="server" Text='<%#Eval("C") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Closing Bal
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCB" runat="server" Text='<%#Eval("CB") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />

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
                                <h4 id="myModalLabel" class="modal-title">Raise Ticket </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdatetid" runat="server" Text="Transaction ID :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdatetid" runat="server" CssClass="form-control" TabIndex="1" disabled />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" Text="Mobile/Subscriber ID :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" TabIndex="2" disabled />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" Text="Deatils :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDeatils" runat="server" CssClass="form-control" TabIndex="2" TextMode="MultiLine" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="3" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Request" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Save" OnClick="btn_Click" TabIndex="4" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

