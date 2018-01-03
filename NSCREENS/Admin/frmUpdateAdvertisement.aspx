<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmUpdateAdvertisement.aspx.cs" EnableEventValidation="false" Inherits="Admin_frmUpdateAdvertisement" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master"%>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Update Advertisement</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Title :" CssClass="col-sm-1 control-label" />
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlSearchTitle" runat="server" CssClass="form-control" />
                            </div>
                            <%-- <asp:Label ID="Label5" runat="server" Text="Hero :" CssClass="col-sm-2 control-label" />
                            <div class="col-sm-2">
                                <asp:DropDownlist ID="ddlSearchHero" runat="server" CssClass="form-control" />
                            </div>
                            <asp:Label ID="Label6" runat="server" Text="Technician  :" CssClass="col-sm-2 control-label" />
                            <div class="col-sm-2">
                                <asp:DropDownlist ID="ddlSearchTechnician" runat="server" CssClass="form-control" />
                            </div>--%>
                            <asp:Label ID="Label7" runat="server" Text="Category :" CssClass="col-sm-1 control-label" />
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlSearchCategory" runat="server" CssClass="form-control" />
                            </div>
                            <asp:Label ID="Label8" runat="server" Text="Channel :" CssClass="col-sm-1 control-label" />
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlSearchChannel" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-info" OnClick="btnDisplay_Click" />
                            </div>
                        </div>
                        <hr />
                        <div class="form-group">
                            <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvSearchFilm" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                CssClass="table table-bordered table-hover table-striped">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdbSelect" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
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
                                                            <asp:Label ID="lblGridAdvertisement_Id" runat="server" Text='<%#Eval("Short_film_Id") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tag">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridTag" runat="server" Text='<%#Eval("Tag") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hero">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridHero" runat="server" Text='<%#Eval("Hero") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Heroine">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridHeroine" runat="server" Text='<%#Eval("Heroine") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Technician">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridTechnician" runat="server" Text='<%#Eval("Technician") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGridCategory_Name" runat="server" Text='<%#Eval("Category_Name") %>' />
                                                            <asp:Label ID="lblGridCategoryId" runat="server" Text='<%#Eval("Category") %>' Visible="false" />
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
                    </div>
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-xs-12">
                                            <div class="col-xs-6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblTitle" runat="server" Text="Title :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" TabIndex="1" />
                                                        <asp:Label ID="lblAdvertisementId" runat="server" Visible="false" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblTag" runat="server" Text="Tag :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" TabIndex="1" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="1" />
                                                        <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate"
                                                            Format="yyyy-MM-dd" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TabIndex="1" />
                                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate"
                                                            Format="yyyy-MM-dd" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                            <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                                            <div class="col-sm-6">
                                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblUploadVideo" runat="server" Text="Upload Video :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="fudUploadVideo" runat="server" />
                                                        <asp:Label ID="lblVideoLLink" runat="server" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-6">

                                                <%--<div class="form-group">
                                                <asp:Label ID="lblAddImg" runat="server" Text="Upload Img :" CssClass="col-sm-4 control-label" />
                                                <div class="col-sm-6">
                                                    <asp:FileUpload ID="fudAddImg" runat="server" />
                                                </div>
                                            </div>--%>

                                                <div class="form-group">
                                                    <asp:Label ID="lblNoofVisits" runat="server" Text="No. of Visits :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtNoofVisits" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblBudget" runat="server" Text="Budget :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblPromoCode" runat="server" Text="Promo Code :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtPromoCode" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblUploadImage" runat="server" Text="Upload Image :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="fupImage" runat="server" />
                                                    </div>
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
                </div>
            </div>
        </div>
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Advertisement List</h3>
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
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
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
</asp:Content>

