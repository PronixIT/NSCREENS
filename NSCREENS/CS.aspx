<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="CSharp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IP To Country Example</title>
</head>
<body style = "font-family:Arial;font-size:10pt">
    <form id="form1" runat="server">
    <asp:GridView ID="gvLocation" runat="server" AutoGenerateColumns = "false">
        <Columns>
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />
            <asp:BoundField DataField="CountryName" HeaderText="Country" />
            <asp:BoundField DataField="CityName" HeaderText="City" />
            <asp:BoundField DataField="RegionName" HeaderText="Region" />
            <asp:BoundField DataField="CountryCode" HeaderText="Country Code" />
            <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
            <asp:BoundField DataField="Longitude" HeaderText="Latitude" />
            <asp:BoundField DataField="Timezone" HeaderText="Timezone" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
