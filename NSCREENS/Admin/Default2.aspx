<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Admin_Default2" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function Add_Budget() {
            
                var ulr=window.location.href;
                var str=ulr.split('Admin');
                //alert(str.length);
                PageMethods.GetCurrentTime(document.getElementById("<%=txt.ClientID%>").value, 1, OnSuccess);

            
        }

        function OnSuccess(response, userContext, methodName) {
            //alert(response);
            document.getElementById('<%=(Master.FindControl("lblAmount")).ClientID%>').innerText = response;
        }
    </script>
    <asp:UpdatePanel ID="dff" runat="server">
        <ContentTemplate>
            <div id="add" runat="server" visible="true"></div>
            <video id='Video' height='376px' width='100%' autoplay runat="server" onended='Add_Budget()'><source id='add123' src="../Videos/Shortfilm_6.mp4" type='video/mp4'></video>
            <asp:TextBox ID="txt" runat="server" CssClass="hidden"></asp:TextBox>
            <%--<button onclick="ShowCurrentTime()">sf sdfs</button>
            <asp:Button ID="btn" runat="server" click="ShowCurrentTime()" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

