<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Admin_Settings" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopupShort() {
            $('#myModalShort').modal('show');

            $('#myModalShort').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <div class="bs-example bs-example-tabs" data-example-id="togglable-tabs">
        <ul class="nav nav-tabs" id="myTabs" role="tablist">
            <li role="presentation" class="active"><a href="#Profile" id="Profile1" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Update Profile</a></li>
            <li role="presentation"><a href="#home" id="home-tab" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Advertisement Report</a></li>
            <li role="presentation" class=""><a href="#profile" role="tab" id="profile-tab" data-toggle="tab" aria-controls="profile" aria-expanded="false">Short Film Report</a></li>
            <li role="presentation" class=""><a href="#change" role="tab" id="profile-tabChg" data-toggle="tab" aria-controls="profile" aria-expanded="false">Change password</a></li>
        </ul>
        <div class="tab-content" id="myTabContent">
            <br />
            <div class="tab-pane fade active in" role="tabpanel" id="Profile" aria-labelledby="home-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Profile</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel4" runat="server" DefaultButton="btnSubmit">
                                        <asp:UpdatePanel ID="upl" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">
                                                    <div class="col-sm-12">
                                                        <div class="col-xs-9">
                                                            <div class="form-horizontal">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label2" runat="server" Text="Name :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="102" />
                                                                        <asp:Label ID="lblRegisterId" runat="server" Visible="false" Text="0" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label3" runat="server" Text="Mobile Number :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" TabIndex="102" MaxLength="10" onkeypress="return isNumber(event)" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label5" runat="server" Text="Email Id :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" TabIndex="102" />
                                                                        <asp:Label ID="lblDumpEmailId" runat="server" Visible="false" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblGender" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:RadioButton ID="rdbMale" runat="server" CssClass="radio radio-inline" Checked="true" GroupName="Gender" Text="Male"/>
                                                                        <asp:RadioButton ID="rdbFemale" runat="server" CssClass="radio radio-inline" GroupName="Gender" Text="Female" />
                                                                        <asp:RadioButton ID="rdbOthers" runat="server" CssClass="radio radio-inline" GroupName="Gender" Text="Others" />
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label6" runat="server" Text="State/UT :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDistrict" runat="server" Text="District :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" ></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblCity" runat="server" Text="City :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                                        <asp:Label ID="lblDumpCityId" runat="server" Visible="false"/>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label8" runat="server" Text="Pincode :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"
                                                                            TabIndex="102" />

                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDateofBirth" runat="server" Text="Date of Birth :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
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
                                                                        <%--<asp:TextBox ID="txtDateofBirth" runat="server" TabIndex="102" CssClass="form-control" />--%>
                                                                        <%--<ajax:CalendarExtender ID="ccl" runat="server" TargetControlID="txtDateofBirth"></ajax:CalendarExtender>--%>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label9" runat="server" Text="Photo :" CssClass="col-sm-4 control-label" />
                                                                    <div class="col-sm-6">
                                                                        <asp:FileUpload ID="fudPhoto" runat="server" onchange="showimagepreview(this)" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-3">
                                                            <div class="form-horizontal">
                                                                <div class="col-sm-12">
                                                                    <asp:Image ID="imgPhoto" runat="server" CssClass="thumbnail pull-right" Width="200px" Height="200px" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnUpdateProfile" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                                        CommandName="Save" OnClick="btnUpdateProfile_Click" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnUpdateProfile" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="tab-pane fade" role="tabpanel" id="home" aria-labelledby="home-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Search Advertisement</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                                        <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblStatus" runat="server" Text="Status :" CssClass="col-sm-2 control-label" />
                                                        <div class="col-sm-10">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Unapprove"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="Request"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-info pull-right"
                                                        CommandName="Save" OnClick="btn_Click" />
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
                                <h3 class="panel-title">Advertisement List</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="pnlGrid" runat="server">
                                        <asp:UpdatePanel ID="UplState" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvadvertisement" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        CssClass="table table-bordered table-hover table-striped">
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
                                                            <asp:TemplateField HeaderText="No of Views">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridNoofVisits" runat="server" Text='<%#Eval("NoofVisits") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Current Views">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridVisits" runat="server" Text='<%#Eval("Visits") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Promo Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGridPromoCode" runat="server" Text='<%#Eval("PromoCode") %>' />
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
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" CommandName='<%#Eval("Advertisement_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
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
            <div class="tab-pane fade" role="tabpanel" id="profile" aria-labelledby="profile-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Search Short Film</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnDisplay">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" runat="server" Text="Title :" CssClass="col-sm-2 control-label" />
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="ddlSearchTitle" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <asp:Label ID="Label7" runat="server" Text="Category :" CssClass="col-sm-2 control-label" />
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="ddlSearchCategory" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <asp:Label ID="Label1" runat="server" Text="Status :" CssClass="col-sm-2 control-label" />
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="ddlShortStatus" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Unapprove"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="Requested"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <asp:Button ID="btnDisplay" runat="server" Text="Search" CssClass="btn btn-info pull-right" OnClick="btnDisplay_Click" />
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
                                <h3 class="panel-title">Short Film List</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel2" runat="server" Height="250px" ScrollBars="Vertical">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvSearchFilm" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                        CssClass="table table-bordered table-hover table-striped">
                                                        <Columns>
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
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="fa fa-eye" CommandName='<%#Eval("Short_film_Id") %>' OnClick="lnkViewShort_Click"></asp:LinkButton>
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
            <div class="tab-pane fade" role="tabpanel" id="change" aria-labelledby="profile-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Change Password</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel13" runat="server" DefaultButton="btnSubmmit">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
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
                                                            <asp:Button ID="btnSubmmit" runat="server" Text="Submit" CssClass="btn btn-info"
                                                                OnClick="btnSubmit_Click" TabIndex="51" />
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
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="pnlUpdate" runat="server" DefaultButton="btnClose">
                    <asp:UpdatePanel ID="uplUpdate" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Views</h4>
                            </div>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvViews" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        CssClass="table table-bordered table-hover table-striped">
                                        <Columns>
                                            <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />--%>
                                            <asp:TemplateField HeaderText="S. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Username">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridUsername" runat="server" Text='<%#Eval("Username") %>' />
                                                    <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IPAddress">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridIPAddress" runat="server" Text='<%#Eval("IPAddress") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date & Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCity_Id" runat="server" Text='<%#Eval("City_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>List is Empty</EmptyDataTemplate>
                                    </asp:GridView>
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
    <div id="myModalShort" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:Panel ID="Panel3" runat="server" DefaultButton="Button1">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Views</h4>
                            </div>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvShortView" runat="server" AutoGenerateColumns="False" GridLines="None"
                                        CssClass="table table-bordered table-hover table-striped">
                                        <Columns>
                                            <%--<asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />--%>
                                            <asp:TemplateField HeaderText="S. No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Username">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridUsername" runat="server" Text='<%#Eval("Username") %>' />
                                                    <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IPAddress">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridIPAddress" runat="server" Text='<%#Eval("IPAddress") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date & Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCity_Id" runat="server" Text='<%#Eval("City_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Budget">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridUpload" runat="server" Text='<%#Eval("User_Budget") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sharing Budget">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGridCity_Id" runat="server" Text='<%#Eval("Video_Sharing") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>List is Empty</EmptyDataTemplate>
                                    </asp:GridView>
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

