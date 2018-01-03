<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddAdvertisement.aspx.cs" Inherits="Admin_frmAddAdvertisement" EnableEventValidation="false" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap-multiselect-Advertisement.js"></script>
    <script type="text/javascript">
        function isSpecialChar(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                if (charCode == 46)
                    return true;
                else
                    return false;
            }
            return true;
            //if (charCode == 95) {
            //    return false;
            //}
            //return true;
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        $(function () {
            $('[id*=ddlSearchTitle]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>

    <script type="text/javascript">
        function divPopup() {
            showMessage('<strong>Upload Successful</strong>')
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstUpcomming]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstCountry]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstState]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstDistrict]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstCity]').multiselect({
                includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('#preff').tooltip({ title: "<p>If you select a particular film or multiple films your advertisement will only be displayed to the people who plays that or those films based on the locations you select below (1person = 1 view)</p><p> (or)</p><p>If you didn't select any film it would be taken as all and your advertisement will be displayed to the people in the locations you will select below (1person = 1 view)</p>", html: true, placement: "Right" });
            $('#Import').tooltip({ title: "<p>If you select VIEWS as option then your advertisement will be displayed in the website till the completion of the last view(based on the no. of views you had kept) </p><p> (or)</p><p>If you select Date as an option then you are asked to select start date and the end date where your advertisement will be displayed to the people in the website only between those days and if your budget still remains unused you can get back the remaining amount(PAYMENT GATEWAY charges may applicable)</p>", html: true, placement: "Right" });


        });
    </script>

    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Advertisement Entry</h3>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="upl" runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal">
                                <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                                    <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-xs-12">
                                                    <div id="progress-container" class="progress">
                                                        <div id="progress" class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar" aria-valuenow="46" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                            &nbsp;0%
           
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div id="results"></div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="col-xs-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" TabIndex="1" />
                                                            </div>

                                                            <asp:Label ID="lblNoofVisits" runat="server" Text="No. of Visits :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtNoofVisits" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" AutoPostBack="true" OnTextChanged="txtNoofVisits_TextChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" TabIndex="1" />
                                                            </div>

                                                            <asp:Label ID="lblBudget" runat="server" Text="Budget :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" onkeypress="return isSpecialChar(event)" AutoPostBack="true" OnTextChanged="txtBudget_TextChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="Label1" runat="server" Text="Description :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" />
                                                            </div>
                                                            <asp:Label ID="lblPromoCode" runat="server" Text="Promo Code :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtPromoCode" runat="server" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <legend>Preferences<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true" id="preff"></span></legend>
                                                        <div class="form-group">
                                                            <div class="form-horizontal">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label4" runat="server" Text="Short Film Title :" CssClass="col-sm-2 control-label" />
                                                                    <div class="col-sm-2">
                                                                        <%--<asp:DropDownList ID="ddlSearchTitle" runat="server" CssClass="form-control" />--%>
                                                                        <asp:ListBox ID="ddlSearchTitle" runat="server" SelectionMode="Single" class="multiselect multiselect-icon" role="multiselect"></asp:ListBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnDisplay" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnDisplay_Click" />
                                                                    </div>
                                                                    <asp:Label ID="Label6" runat="server" Text="Upcomming Short Film :" CssClass="col-sm-2 control-label" />
                                                                    <div class="col-sm-2">
                                                                        <%--<asp:DropDownList ID="ddlSearchTitle" runat="server" CssClass="form-control" />--%>
                                                                        <asp:ListBox ID="lstUpcomming" runat="server" SelectionMode="Single" class="multiselect multiselect-icon" role="multiselect"></asp:ListBox>
                                                                    </div>

                                                                    <div class="col-sm-1">
                                                                        <asp:Button ID="btnUpcomming" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnUpcomming_Click" />
                                                                    </div>
                                                                </div>

                                                                <hr />
                                                                <div class="form-group">
                                                                    <asp:Panel ID="Panel1" runat="server">
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="table-responsive">
                                                                                    <asp:Label ID="lblAddSht" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:GridView ID="gvSearchFilm" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="table table-bordered table-hover table-striped">
                                                                                        <Columns>
                                                                                            <%--<asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="rdbSelect" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="Remove">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="glyphicon glyphicon-remove" CommandName='<%#Eval("Lan_Short_film_Id") %>' OnClick="lnkDelete_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="S. No.">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Title">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                                                                    <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Lan_Short_film_Id") %>' Visible="false" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Tag">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Language">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Channel Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridChannel_Name" runat="server" Text='<%#Eval("Channel_Name") %>' />
                                                                                                    <asp:Label ID="lblGridChannel" runat="server" Text='<%#Eval("Channel") %>' Visible="false" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Views">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridVisits" runat="server" Text='<%#Eval("Visits") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <div class='<%#Eval("StatusClr") %>'>
                                                                                                        <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>' />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Is Active">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridStateIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </asp:Panel>
                                                                </div>
                                                                <hr />
                                                                <div class="form-group">
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="table-responsive">
                                                                                    <asp:Label ID="lblDumpUpComming" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:GridView ID="gdvUpComming" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                                        CssClass="table table-bordered table-hover table-striped">
                                                                                        <Columns>
                                                                                            <%--<asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="rdbSelect" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="Remove">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDeleteUp" runat="server" CssClass="glyphicon glyphicon-remove" CommandName='<%#Eval("Lan_Short_film_Id") %>' OnClick="lnkDeleteUp_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="S. No.">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Title">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                                                                    <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Lan_Short_film_Id") %>' Visible="false" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Tag">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Language">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Channel Name">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridChannel_Name" runat="server" Text='<%#Eval("Channel_Name") %>' />
                                                                                                    <asp:Label ID="lblGridChannel" runat="server" Text='<%#Eval("Channel") %>' Visible="false" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Views">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridVisits" runat="server" Text='<%#Eval("Visits") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Description">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Status">
                                                                                                <ItemTemplate>
                                                                                                    <div class='<%#Eval("StatusClr") %>'>
                                                                                                        <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>' />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Is Active">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridStateIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <%--<Columns>

                                                                                            <asp:TemplateField HeaderText="Remove">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDeleteUp" runat="server" CssClass="glyphicon glyphicon-remove" CommandName='<%#Eval("Title_Id") %>' OnClick="lnkDeleteUp_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="S. No.">
                                                                                                <ItemTemplate>
                                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Title">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title_Name") %>' />
                                                                                                    <asp:Label ID="lblGridTitle_Id" runat="server" Text='<%#Eval("Title_Id") %>' Visible="false" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Tag">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Language">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblGridLanguage" runat="server" Text='<%#Eval("Language_Name") %>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                        </Columns>--%>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </asp:Panel>
                                                                </div>
                                                                <hr />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="Label5" runat="server" Text="Importance :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:RadioButton ID="rdbDates" runat="server" CssClass="radio radio-inline" GroupName="importance" Checked="true" Text="Date" AutoPostBack="true" OnCheckedChanged="rdbDates_CheckedChanged" />
                                                                <asp:RadioButton ID="rdbViews" runat="server" CssClass="radio radio-inline" GroupName="importance" Text="Views" AutoPostBack="true" OnCheckedChanged="rdbDates_CheckedChanged" />

                                                            </div>
                                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true" id="Import"></span>
                                                        </div>

                                                        <div class="form-group">
                                                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <div class="btn-group" role="group" aria-label="...">
                                                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Day</asp:ListItem>
                                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Style="width: 100px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Month</asp:ListItem>
                                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Year</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <%--<asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="1" />--%>
                                                                <%--<ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate"
                                                                    Format="yyyy-MM-dd" />--%>
                                                            </div>

                                                            <asp:Label ID="lblEndDate" runat="server" Text="End Date :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <div class="btn-group" role="group" aria-label="...">
                                                                    <asp:DropDownList ID="ddlEndDay" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Day</asp:ListItem>
                                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="form-control" Style="width: 100px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Month</asp:ListItem>
                                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="form-control" Style="width: 80px; height: 34px; display: inline-block;">
                                                                        <asp:ListItem Value="0">Year</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                                <%--<asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TabIndex="1" />--%>
                                                                <%--<ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate"
                                                                    Format="yyyy-MM-dd" />--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCountry" runat="server" Text="Country :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:ListBox ID="lstCountry" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:ListBox>
                                                            </div>
                                                            <asp:Label ID="Label2" runat="server" Text="Gender :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:RadioButton ID="rdbAll" runat="server" CssClass="radio-inline" Text="All" GroupName="Gender" Checked="true" />
                                                                <asp:RadioButton ID="rdbMale" runat="server" CssClass="radio-inline" Text="Male" GroupName="Gender" />
                                                                <asp:RadioButton ID="rdbFemale" runat="server" CssClass="radio-inline" Text="Female" GroupName="Gender" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblState" runat="server" Text="State/UT :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:ListBox ID="lstState" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:ListBox>
                                                            </div>
                                                            <asp:Label ID="lblAge" runat="server" Text="Age :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-2">
                                                                <asp:DropDownList ID="ddlAgefrom" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>

                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>61</asp:ListItem>
                                                                    <asp:ListItem>62</asp:ListItem>
                                                                    <asp:ListItem>63</asp:ListItem>
                                                                    <asp:ListItem>64</asp:ListItem>
                                                                    <asp:ListItem>65</asp:ListItem>
                                                                    <asp:ListItem>66</asp:ListItem>
                                                                    <asp:ListItem>67</asp:ListItem>
                                                                    <asp:ListItem>68</asp:ListItem>
                                                                    <asp:ListItem>69</asp:ListItem>
                                                                    <asp:ListItem>70</asp:ListItem>
                                                                    <asp:ListItem>71</asp:ListItem>
                                                                    <asp:ListItem>72</asp:ListItem>
                                                                    <asp:ListItem>73</asp:ListItem>
                                                                    <asp:ListItem>74</asp:ListItem>
                                                                    <asp:ListItem>75</asp:ListItem>
                                                                    <asp:ListItem>76</asp:ListItem>
                                                                    <asp:ListItem>77</asp:ListItem>
                                                                    <asp:ListItem>78</asp:ListItem>
                                                                    <asp:ListItem>79</asp:ListItem>
                                                                    <asp:ListItem>80</asp:ListItem>
                                                                    <asp:ListItem>81</asp:ListItem>
                                                                    <asp:ListItem>82</asp:ListItem>
                                                                    <asp:ListItem>83</asp:ListItem>
                                                                    <asp:ListItem>84</asp:ListItem>
                                                                    <asp:ListItem>85</asp:ListItem>
                                                                    <asp:ListItem>86</asp:ListItem>
                                                                    <asp:ListItem>87</asp:ListItem>
                                                                    <asp:ListItem>88</asp:ListItem>
                                                                    <asp:ListItem>89</asp:ListItem>
                                                                    <asp:ListItem>90</asp:ListItem>

                                                                    <asp:ListItem>91</asp:ListItem>
                                                                    <asp:ListItem>92</asp:ListItem>
                                                                    <asp:ListItem>93</asp:ListItem>
                                                                    <asp:ListItem>94</asp:ListItem>
                                                                    <asp:ListItem>95</asp:ListItem>
                                                                    <asp:ListItem>96</asp:ListItem>
                                                                    <asp:ListItem>97</asp:ListItem>
                                                                    <asp:ListItem>98</asp:ListItem>
                                                                    <asp:ListItem>99</asp:ListItem>
                                                                    <asp:ListItem>100</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:DropDownList ID="ddlAgeTo" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                    <asp:ListItem>32</asp:ListItem>
                                                                    <asp:ListItem>33</asp:ListItem>
                                                                    <asp:ListItem>34</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>36</asp:ListItem>
                                                                    <asp:ListItem>37</asp:ListItem>
                                                                    <asp:ListItem>38</asp:ListItem>
                                                                    <asp:ListItem>39</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>

                                                                    <asp:ListItem>41</asp:ListItem>
                                                                    <asp:ListItem>42</asp:ListItem>
                                                                    <asp:ListItem>43</asp:ListItem>
                                                                    <asp:ListItem>44</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>46</asp:ListItem>
                                                                    <asp:ListItem>47</asp:ListItem>
                                                                    <asp:ListItem>48</asp:ListItem>
                                                                    <asp:ListItem>49</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>61</asp:ListItem>
                                                                    <asp:ListItem>62</asp:ListItem>
                                                                    <asp:ListItem>63</asp:ListItem>
                                                                    <asp:ListItem>64</asp:ListItem>
                                                                    <asp:ListItem>65</asp:ListItem>
                                                                    <asp:ListItem>66</asp:ListItem>
                                                                    <asp:ListItem>67</asp:ListItem>
                                                                    <asp:ListItem>68</asp:ListItem>
                                                                    <asp:ListItem>69</asp:ListItem>
                                                                    <asp:ListItem>70</asp:ListItem>
                                                                    <asp:ListItem>71</asp:ListItem>
                                                                    <asp:ListItem>72</asp:ListItem>
                                                                    <asp:ListItem>73</asp:ListItem>
                                                                    <asp:ListItem>74</asp:ListItem>
                                                                    <asp:ListItem>75</asp:ListItem>
                                                                    <asp:ListItem>76</asp:ListItem>
                                                                    <asp:ListItem>77</asp:ListItem>
                                                                    <asp:ListItem>78</asp:ListItem>
                                                                    <asp:ListItem>79</asp:ListItem>
                                                                    <asp:ListItem>80</asp:ListItem>
                                                                    <asp:ListItem>81</asp:ListItem>
                                                                    <asp:ListItem>82</asp:ListItem>
                                                                    <asp:ListItem>83</asp:ListItem>
                                                                    <asp:ListItem>84</asp:ListItem>
                                                                    <asp:ListItem>85</asp:ListItem>
                                                                    <asp:ListItem>86</asp:ListItem>
                                                                    <asp:ListItem>87</asp:ListItem>
                                                                    <asp:ListItem>88</asp:ListItem>
                                                                    <asp:ListItem>89</asp:ListItem>
                                                                    <asp:ListItem>90</asp:ListItem>

                                                                    <asp:ListItem>91</asp:ListItem>
                                                                    <asp:ListItem>92</asp:ListItem>
                                                                    <asp:ListItem>93</asp:ListItem>
                                                                    <asp:ListItem>94</asp:ListItem>
                                                                    <asp:ListItem>95</asp:ListItem>
                                                                    <asp:ListItem>96</asp:ListItem>
                                                                    <asp:ListItem>97</asp:ListItem>
                                                                    <asp:ListItem>98</asp:ListItem>
                                                                    <asp:ListItem>99</asp:ListItem>
                                                                    <asp:ListItem>100</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        <div class="form-group">

                                                            <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:ListBox ID="lstDistrict" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:ListBox>
                                                            </div>

                                                            <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:FileUpload ID="fupImage" runat="server" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-sm-4">
                                                                <asp:ListBox ID="lstCity" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                            </div>

                                                            <asp:Label ID="Label3" runat="server" Text="Upload Video :" CssClass="col-sm-2 control-label" />
                                                            <div class="col-md-4">

                                                                <div id="drop_zone">It should be (<=1.05 minutes) to get approved,<br /> Film Trailers are Exceptional</div>
                                                                <br />
                                                                <label class="btn btn-block btn-info">
                                                                    Browse&hellip;
                                                                    <input id="browse" type="file" style="display: none;">
                                                                </label>
                                                                <asp:TextBox ID="txtURL" runat="server" CssClass="hidden" />
                                                            </div>
                                                            <%--<asp:Label ID="lblUploadVideo" runat="server" Text="Upload Video :" CssClass="col-sm-4 control-label" />
                                                            <div class="col-sm-6">
                                                                <asp:FileUpload ID="fudUploadVideo" runat="server" />
                                                                <asp:Label ID="lblVideoLLink" runat="server" Visible="false" />
                                                            </div>--%>
                                                        </div>
                                                        <div class="form-group">
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="box-footer">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                                    CommandName="Clear" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                                    CommandName="Save" OnClick="btn_Click" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                            <hr />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                    Advertisement List
               
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvadvertisement" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvadvertisement_RowCommand">
                                            <Columns>
                                                <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />--%>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Title") %>' />
                                                        <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Advertisement_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tag">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No of Visits">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridNoofVisits" runat="server" Text='<%#Eval("NoofVisits") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridStartDate" runat="server" Text='<%#Eval("StartDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridEndDate" runat="server" Text='<%#Eval("EndDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridBudget" runat="server" Text='<%#Eval("Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promo Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromoCode" runat="server" Text='<%#Eval("PromoCode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Views">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridVisits" runat="server" Text='<%#Eval("Visits") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <div class='<%#Eval("StatusClr") %>'>
                                                            <asp:Label ID="lblGridStatus" runat="server" Text='<%#Eval("Status") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridStateIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
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

    <script type="text/javascript" src="vimeo-upload.js"></script>

    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=lstUpcomming]').multiselect({
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
                    $('[id*=lstCountry]').multiselect({
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
                    $('[id*=lstState]').multiselect({
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
                    $('[id*=lstDistrict]').multiselect({
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
                    $('[id*=lstCity]').multiselect({
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
                    $('[id*=ddlSearchTitle]').multiselect({
                        includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                        enableFiltering: true
                    });

                }
            });
        };
    </script>

    <script type="text/javascript">

        /**
         * Called when files are dropped on to the drop target or selected by the browse button.
         * For each file, uploads the content to Drive & displays the results when complete.
         */
        function handleFileSelect(evt) {
            evt.stopPropagation()
            evt.preventDefault()

            var files = evt.dataTransfer ? evt.dataTransfer.files : $(this).get(0).files
            var results = document.getElementById('results')

            /* Clear the results div */
            while (results.hasChildNodes()) results.removeChild(results.firstChild)

            /* Rest the progress bar and show it */
            updateProgress(0)
            document.getElementById('progress-container').style.display = 'block'

            /* Instantiate Vimeo Uploader */
            ; (new VimeoUpload({
                name: document.getElementById('<%= txtTitle.ClientID %>').value,//document.getElementById('videoName').value,
                description: document.getElementById('<%= txtDescription.ClientID %>').value, //document.getElementById('videoDescription').value,
                private: false,//document.getElementById('make_private').checked,
                file: files[0],
                token: "fbfdcdfadd16cee9207dcf04217c8545", //document.getElementById('accessToken').value,
                upgrade_to_1080: false,//document.getElementById('upgrade_to_1080').checked,
                onError: function (data) {
                    showMessage('<strong>Error</strong>: ' + JSON.parse(data).error, 'danger')
                },
                onProgress: function (data) {
                    updateProgress(data.loaded / data.total)
                },
                onComplete: function (videoId, index) {
                    var url = 'https://vimeo.com/' + videoId

                    if (index > -1) {
                        /* The metadata contains all of the uploaded video(s) details see: https://developer.vimeo.com/api/endpoints/videos#/{video_id} */
                        url = this.metadata[index].link //

                        /* add stringify the json object for displaying in a text area */
                        var pretty = JSON.stringify(this.metadata[index], null, 2)

                        console.log(pretty) /* echo server data */
                    }

                    document.getElementById('<%= txtURL.ClientID %>').value = url;

                    //showMessage('<strong>Upload Successful</strong>: check uploaded video @ <a href="' + url + '">' + url + '</a>. ')
                    showMessage('<strong>Upload Successful</strong>')
                }
            })).upload()

            /* local function: show a user message */
            function showMessage(html, type) {
                /* hide progress bar */
                document.getElementById('progress-container').style.display = 'none'

                /* display alert message */
                var element = document.createElement('div')
                element.setAttribute('class', 'alert alert-' + (type || 'success'))
                element.innerHTML = html
                results.appendChild(element)
            }
        }

        /**
         * Dragover handler to set the drop effect.
         */
        function handleDragOver(evt) {
            evt.stopPropagation()
            evt.preventDefault()
            evt.dataTransfer.dropEffect = 'copy'
        }

        /**
         * Updat progress bar.
         */
        function updateProgress(progress) {
            progress = Math.floor(progress * 100)
            var element = document.getElementById('progress')
            element.setAttribute('style', 'width:' + progress + '%')
            element.innerHTML = '&nbsp;' + progress + '%'
        }
        /**
         * Wire up drag & drop listeners once page loads
         */
        document.addEventListener('DOMContentLoaded', function () {
            var dropZone = document.getElementById('drop_zone')
            var browse = document.getElementById('browse')
            dropZone.addEventListener('dragover', handleDragOver, false)
            dropZone.addEventListener('drop', handleFileSelect, false)
            browse.addEventListener('change', handleFileSelect, false)
        })

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
            var dropZone = document.getElementById('drop_zone')
            var browse = document.getElementById('browse')
            dropZone.addEventListener('dragover', handleDragOver, false)
            dropZone.addEventListener('drop', handleFileSelect, false)
            browse.addEventListener('change', handleFileSelect, false)
        });

    </script>

    <script src="../js/bootstrap-select.min.js"></script>
    <script src="../js/tabcomplete.min.js"></script>
    <script src="../js/livefilter.min.js"></script>
    <script src="../js/bootstrap-select.min.js"></script>
    <script src="../js/filterlist.min.js"></script>
</asp:Content>

