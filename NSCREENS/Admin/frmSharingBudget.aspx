<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmSharingBudget.aspx.cs" Inherits="Admin_frmTitleRegister" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=lstLanguage]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });

        $(function () {
            $('[id*=lstUpdateLanguage]').multiselect({
                 includeSelectAllOption: true, enableCaseInsensitiveFiltering: true,
                enableFiltering: true
            });
        });
    </script>
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }
    </script>
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <div class="bs-example bs-example-tabs" data-example-id="togglable-tabs">
        <ul class="nav nav-tabs" id="myTabs" role="tablist">
            <li role="presentation" class="active"><a href="#Budget1" id="Budget" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Budget</a></li>
            <li role="presentation"><a href="#home" id="home-tab" role="tab" data-toggle="tab" aria-controls="home" aria-expanded="true">Advertisement Earning</a></li>
            <li role="presentation" class=""><a href="#profile" role="tab" id="profile-tab" data-toggle="tab" aria-controls="profile" aria-expanded="false">Short Film Earning</a></li>
            <li role="presentation" class=""><a href="#change" role="tab" id="profile-tabChg" data-toggle="tab" aria-controls="profile" aria-expanded="false">Short Film Share Earning</a></li>
            <%--<li role="presentation" class=""><a href="#Amount" role="tab" id="profile-Amount" data-toggle="tab" aria-controls="profile" aria-expanded="false">Amount Transfer to Wallet</a></li>--%>
            <li role="presentation" class=""><a href="#Transactions" role="tab" id="Transactions-tabChg" data-toggle="tab" aria-controls="profile" aria-expanded="false">Transactions</a></li>
            <li class="pull-right">
                <asp:UpdatePanel ID="uplEarning" runat="server"><ContentTemplate>
                <asp:Label ID="lblEarning" runat="server" CssClass="btn btn-danger" Text="0"></asp:Label></ContentTemplate></asp:UpdatePanel></li>
        </ul>
        <div class="tab-content" id="myTabContent">
            <br />
            <div class="tab-pane fade active in" role="tabpanel" id="Budget1" aria-labelledby="home-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Budget</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <asp:UpdatePanel ID="upl" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvSettings" runat="server" AutoGenerateColumns="False" GridLines="None"
                                                                CssClass="table table-bordered table-hover table-striped">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S. No.">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Budget">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridBudget" runat="server" Text='<%#Eval("Budget") %>' />
                                                                            <asp:Label ID="lblGridBudget_Settings_Id" runat="server" Text='<%#Eval("Budget_Settings_Id") %>' Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Short Film(%)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridShort_Film" runat="server" Text='<%#Eval("Short_Film") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="Admin(%)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridAdmin" runat="server" Text='<%#Eval("Admin") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Promoter(%)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridPromoter" runat="server" Text='<%#Eval("Promoter") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Video Sharing(%)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGridVideo_Sharing" runat="server" Text='<%#Eval("Video_Sharing") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
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
            <div class="tab-pane fade" role="tabpanel" id="home" aria-labelledby="home-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Advertisement Earning</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <%--<asp:GridView ID="gvAdd" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Short_Film_Id") %>' />
                                                        <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Uploader">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridUser_Sharing" runat="server" Text='<%#Eval("User_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promoter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromo_Budget" runat="server" Text='<%#Eval("Promoter_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>

                                                    <asp:GridView ID="gvAdd" runat="server" AutoGenerateColumns="true" GridLines="None" ShowFooter="true"
                                                        CssClass="table table-bordered table-hover table-striped">
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
                                <h3 class="panel-title">Short Film Earning</h3>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel3" runat="server" Height="250px" ScrollBars="Vertical">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <%--<asp:GridView ID="gvShort" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Short_Film_Id") %>' />
                                                        <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Uploader">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridUser_Budget" runat="server" Text='<%#Eval("User_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Promoter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromo_Sharing" runat="server" Text='<%#Eval("Promoter_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Video Share">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridVideo_Sharing" runat="server" Text='<%#Eval("Video_Sharing") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                                    <asp:GridView ID="gvShort" runat="server" AutoGenerateColumns="true" GridLines="None" ShowFooter="true"
                                                        CssClass="table table-bordered table-hover table-striped">
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
                                <h3 class="panel-title">Short Film Share Earning</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel13" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                    <%--<asp:GridView ID="gvShare" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Short_Film_Id") %>' />
                                                        <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Visits_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDate_Time" runat="server" Text='<%#Eval("Date_Time")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Uploader">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridUser_Budget" runat="server" Text='<%#Eval("User_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Promoter">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPromo_Sharing" runat="server" Text='<%#Eval("Promoter_Budget") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Video Share">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridVideo_Sharing" runat="server" Text='<%#Eval("Video_Sharing") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>

                                                    <asp:GridView ID="gvShare" runat="server" AutoGenerateColumns="true" GridLines="None" ShowFooter="true"
                                                        CssClass="table table-bordered table-hover table-striped">
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
            <div class="tab-pane fade" role="tabpanel" id="Amount" aria-labelledby="profile-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Short Film Share Earning</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblEarningAmt" runat="server" CssClass="control-label col-sm-2" Text="Earning Amount : "></asp:Label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtEarningAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="lblDumpEarningAmt" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </div>
                                                          <div class="form-group">
                                                            <asp:Label ID="lblTransferAmount" runat="server" CssClass="control-label col-sm-2" Text="Transfer Amount : "></asp:Label>
                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtTransferAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="btnTransfer" runat="server" Text="Transfer" CssClass="btn btn-info" OnClick="btnTransfer_Click" />
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
            <div class="tab-pane fade" role="tabpanel" id="Transactions" aria-labelledby="profile-tab">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Transactions</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <asp:Panel ID="Panel5" runat="server">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                          <asp:GridView ID="gvTrans" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transaction Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridTitle" runat="server" Text='<%#Eval("Transaction_Id") %>' />
                                                        <asp:Label ID="lblGridVisits_Id" runat="server" Text='<%#Eval("Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridCredit" runat="server" Text='<%#Eval("Credit")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridCreatedDate" runat="server" Text='<%#Eval("CreatedDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Payment Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridPage" runat="server" Text='<%#Eval("Page") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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

</asp:Content>

