<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmTechHome.aspx.cs" Inherits="Admin_Default5" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <asp:ListView ID="lstTech" runat="server">
                <ItemTemplate>
                    <div class="col-sm-2" style="margin-bottom:15px;">
                    <a href='<%#Eval("NavigateUrl") %>' runat="server" class="btn btn-default btn-lg col-sm-12" role="button"><span class="glyphicon glyphicon-user"></span>
                            <br/>
                        <asp:Label ID="lblText" runat="server" Text='<%#Eval("Text") %>'></asp:Label></a>
                         
                        </div>
            
                </ItemTemplate>

            </asp:ListView>
               
        </div>
    </div>
</asp:Content>

