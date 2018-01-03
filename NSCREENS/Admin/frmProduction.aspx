<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmProduction.aspx.cs" Inherits="Admin_frmProduction" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function divPopup() {
            $('#myModal').modal('show');

            $('#myModal').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function divPopup1() {
            $('#myModal1').modal('show');

            $('#myModal1').modal({
                backdrop: true,
                keyboard: true
            })
        }

        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#ContentPlaceHolder1_Thumbnail').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }

        function showimagepreviewupdate(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#ContentPlaceHolder1_imgupdate').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">New Production Entry</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:Label ID="lblProduction" runat="server" Text="Production :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtProduction" runat="server" CssClass="form-control" TabIndex="1" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblDescription" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                            TabIndex="2" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblProductionImage" runat="server" Text="Production Image :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:FileUpload ID="fupProductionImage" runat="server" onchange="showimagepreview(this)" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" Text="Password :" CssClass="col-sm-4 control-label" />
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtPassword1" runat="server" TextMode="Password" CssClass="form-control" TabIndex="501" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="img-thumbnail">
                                                    <asp:Image ID="Thumbnail" runat="server" Height="100px" Width="100px" Visible="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="box-footer">
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                                CommandName="Clear" OnClick="btn_Click" />
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                                CommandName="Save" OnClick="btn_Click" />
                                        </div>
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
        <div class="col-md-12 col-sm-12">
            <asp:UpdatePanel ID="upl" runat="server">
                <ContentTemplate>
                    <div class="latst-vid secondary-vid">
                        <div class="vid-heading overflow-hidden">
                            <div class="col-sm-6">
                                <span class="wow" data-wow-duration="0.8s">Production List
                                </span>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control" placeholder="Search Name" AutoPostBack="true" OnTextChanged="txtSearch1_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:LinkButton ID="lnkSearch" runat="server" OnClick="txtSearch1_TextChanged"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>

                            <div class="hding-bottm-line wow zoomIn" data-wow-duration="0.8s"></div>
                        </div>
                        <div class="row auto-clear">
                            <div class="vid-container">

                                <asp:ListView ID="lstOther" runat="server" GroupItemCount="1">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-3">
                                            <div class="latest-vid-img-container">
                                                <div class="vid-img">
                                                    <img class="img-responsive" src='<%#Eval("Img") %>' alt="video image" style="height: 171px; width: 180px;">
                                                    <asp:LinkButton ID="lnkMore" runat="server" CssClass="play-icon play-small-icon" OnClick="lnkMore_Click" CommandName='<%#Eval("Channel_Id") %>'>
                                                        <asp:Image ID="imgView" runat="server" CssClass="img-responsive play-svg svg" ImageUrl="~/images/eye.png" />
                                                    </asp:LinkButton>
                                                    <div class="overlay-div"></div>
                                                </div>
                                                <div class="vid-text">
                                                    <h1>
                                                        <asp:LinkButton ID="lnk" runat="server" OnClick="lnkMore_Click" CommandName='<%#Eval("Channel_Id") %>'>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("Channel_Name") %>'></asp:Label>
                                                        </asp:LinkButton></h1>
                                                    <%--<p class="vid-info-text">
                                                        <span>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                                        </span>
                                                    </p>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p>List is empty</p>
                                    </EmptyDataTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-xs-12  hide">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Production List - (<asp:Label ID="lblCityListList" runat="server"></asp:Label>)</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplProduction" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvProduction" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped" OnRowCommand="gvProduction_RowCommand">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="fa fa-edit" />
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Production">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridProduction" runat="server" Text='<%#Eval("Channel_Name") %>' />
                                                        <asp:Label ID="lblGridProductionId" runat="server" Text='<%#Eval("Channel_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridProductionIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
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
                                <h4 id="myModalLabel" class="modal-title">Update Production</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateProduction" runat="server" Text="Production :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateProduction" runat="server" CssClass="form-control" TabIndex="501" />
                                            <asp:Label ID="lblDName" runat="server" Visible="false" />
                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Description :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtUpdateDescription" runat="server" CssClass="form-control" TabIndex="501" TextMode="MultiLine" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblUpdateIsactive" runat="server" Text="Isactive :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <div class="radioer">
                                                <asp:RadioButton ID="rdbActiveYesProduction" runat="server" Text="Yes" CssClass="radio radio-inline"
                                                    GroupName="Production" TabIndex="502" />
                                                <asp:RadioButton ID="rdbActiveNoProduction" runat="server" Text="No" CssClass="radio radio-inline"
                                                    GroupName="Production" TabIndex="502" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Production Image :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:FileUpload ID="fudUpdateImg" runat="server" onchange="showimagepreviewupdate(this)" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-8">
                                            <div class="img-thumbnail pull-right">
                                                <asp:Image ID="imgupdate" runat="server" Height="100px" Width="100px" Visible="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Update" OnClick="btn_Click" TabIndex="504" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div id="myModal1" tabindex="-1" role="dialog" class="modal fade" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true" tabindex="500">
                                    &times;</button>
                                <h4 id="myModalLabel" class="modal-title">Enter Password</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password :" CssClass="col-sm-4 control-label" />
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" TabIndex="501" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default pull-left"
                                    data-dismiss="modal" aria-hidden="true" TabIndex="505" />
                                <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btn btn-info" ValidationState="Update"
                                    CommandName="Save" OnClick="btn_Click" TabIndex="504" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Button2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

