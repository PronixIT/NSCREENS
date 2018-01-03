<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Admin_Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script type="text/javascript">
        function geolocate() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var geolocation = new google.maps.LatLng(
                    position.coords.latitude, position.coords.longitude);
                    var location = "<b>Latitude</b>: " + geolocation.A + "<br/>";
                    location += "<b>Longitude</b>: " + geolocation.F;
                    document.getElementById('lblResult').innerHTML = location
                });
            }
        }
    </script>
    <div style="margin-top: 200px; margin-left: 200px">
        <span>Location:</span>
        <input type="button" id="btnGet" value="Get Latitude & Longitude" onclick="geolocate()" /><br />
        <br />
        Current Location Latitude & Longitude:<br />
        <br />
        <label id="lblResult" />
    </div>
</asp:Content>

