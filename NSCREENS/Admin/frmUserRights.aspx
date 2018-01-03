<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="frmUserRights.aspx.cs" Inherits="Admin_frmUserRights" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript">
        //Below javascript function is used to checked all child nodes if parent checked and check parent node atlease one child node is checked otherwise unchecked      
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            var t = GetParentByTagName("table", src);
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1) {
                    if (nxtSibling.tagName.toLowerCase() == "div") {
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked) {
                    checkUncheckSwitch = true;
                }
                else {
                    checkUncheckSwitch = false;
                }
                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;

                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            var k = 0;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            //add each selected node one value
                            k = k + 1;
                        }
                    }
                }
            }

            //Finally check any one of child node is select if selected yes then return ture parent node check

            if (k > 0) {
                return true;
            }
            else {
                return false;
            }
        }
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }

    </script>
    <div class="row">
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">User Details</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnl" runat="server" DefaultButton="btnSubmit">
                            <asp:UpdatePanel ID="updAreaEntryModal" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-6 control-label" Text="Select User to Give access :" />
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPerson" runat="server" AutoPostBack="True" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlPerson_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblId" Visible="false" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lbl1" runat="server" CssClass="col-sm-3 control-label" Text="Username :" />
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtName" runat="server" Enabled="false" CssClass="form-control" />
                                            </div>
                                            <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 control-label" Text="Mobile No :" />
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtDesignation" runat="server" Enabled="false" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                           <%-- <asp:Label ID="lblUserCode" runat="server" CssClass="col-sm-3 control-label" Text="User Code :"></asp:Label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtUSerCode" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                            </div>--%>
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info pull-right"
                                                    OnClick="btnSubmit_Click" TabIndex="8" />
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
        <div class="col-xs-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">User Permissions</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:Panel ID="pnlGrid" runat="server">
                            <asp:UpdatePanel ID="UplState" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <asp:TreeView ID="tvsample" runat="server" ImageSet="Custom" ShowCheckBoxes="All"
                                            BorderColor="#336699">
                                            <RootNodeStyle Font-Bold="true" />
                                            <ParentNodeStyle Font-Bold="true" />
                                            <%--<HoverNodeStyle ForeColor="Red" />--%>
                                            <%--<SelectedNodeStyle Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />--%>
                                            <NodeStyle HorizontalPadding="0px" NodeSpacing="2px" VerticalPadding="0px" />
                                            <LeafNodeStyle />
                                        </asp:TreeView>
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

