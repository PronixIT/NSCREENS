<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Inbox.aspx.cs" Inherits="Admin_Inbox" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-xs-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Inbox</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                        <asp:ListView ID="lstInbox" runat="server">
                                            <ItemTemplate>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="headingOne">
                                                        <h4 class="panel-title">
                                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href='<%#Eval("Id") %>' aria-expanded="true" aria-controls="collapseOne">
                                                                <asp:Label ID="lblSub" runat="server" Text='<%#Eval("Subject") %>' CssClass="SimpleText"></asp:Label>
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id='<%#Eval("Send_Message_Id") %>' class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                                        <div class="panel-body">
                                                            <asp:Label ID="lblGridMessage" runat="server" Text='<%#Eval("Message") %>' />
                                                            <asp:Label ID="lblGridSend_Message_Id" runat="server" Text='<%#Eval("Send_Message_Id") %>' Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="table-responsive">
                                        <%--<asp:GridView ID="gvInbox" runat="server" AutoGenerateColumns="False" GridLines="None"
                                            CssClass="table table-bordered table-hover table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S. No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Message">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGridMessage" runat="server" Text='<%#Eval("Message") %>' />
                                                        <asp:Label ID="lblGridSend_Message_Id" runat="server" Text='<%#Eval("Send_Message_Id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
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

