<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddUser.aspx.cs" Inherits="Admin_frmAddUser" EnableEventValidation="false" %>

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

        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#ContentPlaceHolder1_imgPhoto').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New User Entry</h3>
                </div>
                <div class="panel-body">
                    
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="upl" runat="server">
                                <ContentTemplate>
                                    <div class="card-body">
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
                                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" TabIndex="102" MaxLength="10" onkeypress="return isNumber(event)" />
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
                                                    <asp:Label ID="lblGender" runat="server" Text="Gender :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:RadioButton ID="rdbMale" runat="server" CssClass="radio radio-inline" Checked="true" GroupName="Gender" Text="Male" />
                                                        <asp:RadioButton ID="rdbFemale" runat="server" CssClass="radio radio-inline" GroupName="Gender" Text="Female" />
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
                                                        <%--<asp:TextBox ID="txtDateofBirth" runat="server" TabIndex="102" CssClass="form-control" />
                                                        <ajax:CalendarExtender ID="ccl" runat="server" TargetControlID="txtDateofBirth"></ajax:CalendarExtender>--%>
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
                                    </div>
                                    <div class="box-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            CommandName="Clear" OnClick="btnCancel_Click" />
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
                    <h3 class="panel-title">User List</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUserlist" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridName" runat="server" Text='<%#Eval("Name") %>' />
                                                        <asp:Label ID="lblGridRegister_Id" runat="server" Text='<%#Eval("Register_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridMobile_No" runat="server" Text='<%#Eval("Mobile_No") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EmailId">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridEmailId" runat="server" Text='<%#Eval("EmailId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridCity_Name" runat="server" Text='<%#Eval("City_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridAddress" runat="server" Text='<%#Eval("Address") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DOB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDOB" runat="server" Text='<%#Eval("DOB") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Username">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridUsername" runat="server" Text='<%#Eval("Username") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Password">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPassword" runat="server" Text='<%#Eval("Password") %>' />
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

