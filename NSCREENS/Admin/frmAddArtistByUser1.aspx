<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmAddArtistByUser1.aspx.cs" Inherits="Admin_frmAddArtistByUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Artist List -
                        <asp:Label ID="lblArtistType" runat="server"></asp:Label>
                        <a id="lnkAdd" runat="server" class="fa fa-plus pull-right" href="frmArtistDetails.aspx" /></h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplArtist" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvArtist" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Link" CommandName="detail" ControlStyle-CssClass="glyphicon glyphicon-edit" />
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Artist">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridArtist" runat="server" Text='<%#Eval("Name") %>' />
                                                        <asp:Label ID="lblGridArtistId" runat="server" Text='<%#Eval("Artist_Details_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridDescription" runat="server" Text='<%#Eval("Description") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gender">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridGender" runat="server" Text='<%#Eval("Gender") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Interest Areas">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridInterest_Areas" runat="server" Text='<%#Eval("Artist_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Active">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridArtistIsactive" runat="server" Text='<%# Eval("Isactive").ToString().Equals("True") ? " Active " : " Inactive " %>' />
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
    </a>
</asp:Content>

