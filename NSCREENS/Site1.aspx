<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Site1.aspx.cs" Inherits="Site1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script>

        function Getdata()
        {

            document.getElementById("rechargeoffers").contentWindow.document.body.getElementById("ctl00_OnlineContent_tdRegnNo").innerHtml;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        

    <div>
        
        <iframe id="rechargeoffers" src="https://aptransport.in/APCFSTONLINE/Reports/VehicleRegistrationSearch.aspx" width="100%" height="900" frameborder="0" style="border: none; overflow: hidden;" allowtransparency="true" runat="server" scrolling="yes" ></iframe>
        <div><input type="button" title="Submit" value="Save" onclick="Getdata();"/></div>
    
    </div>
    
    </form>
</body>
</html>
