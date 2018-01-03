<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.12.3.min.js"></script>
    <script>

        $(function () {
            var lat = 44.88623409320778,
                lng = -87.86480712897173,
                latlng = new google.maps.LatLng(lat, lng),
                image = 'http://www.google.com/intl/en_us/mapfiles/ms/micons/blue-dot.png';

            //zoomControl: true,
            //zoomControlOptions: google.maps.ZoomControlStyle.LARGE,

            var mapOptions = {
                center: new google.maps.LatLng(lat, lng),
                zoom: 13,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                panControl: true,
                panControlOptions: {
                    position: google.maps.ControlPosition.TOP_RIGHT
                },
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.LARGE,
                    position: google.maps.ControlPosition.TOP_left
                }
            },
            map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions),
                marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    icon: image
                });

            var input = document.getElementById('searchTextField');
            var autocomplete = new google.maps.places.Autocomplete(input, {
                types: ["geocode"]
            });

            autocomplete.bindTo('bounds', map);
            var infowindow = new google.maps.InfoWindow();

            google.maps.event.addListener(autocomplete, 'place_changed', function (event) {
                infowindow.close();
                var place = autocomplete.getPlace();
                if (place.geometry.viewport) {
                    map.fitBounds(place.geometry.viewport);
                } else {
                    map.setCenter(place.geometry.location);
                    map.setZoom(17);
                }

                moveMarker(place.name, place.geometry.location);
                alert(place.name);
                $('.MapLat').val(place.geometry.location.lat());
                $('.MapLon').val(place.geometry.location.lng());
            });
            google.maps.event.addListener(map, 'click', function (event) {
                $('.MapLat').val(event.latLng.lat());
                $('.MapLon').val(event.latLng.lng());
                alert(event.latLng.place.name)
            });
            $("#searchTextField").focusin(function () {
                $(document).keypress(function (e) {
                    if (e.which == 13) {
                        return false;
                        infowindow.close();
                        var firstResult = $(".pac-container .pac-item:first").text();
                        var geocoder = new google.maps.Geocoder();
                        geocoder.geocode({
                            "address": firstResult
                        }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK) {
                                var lat = results[0].geometry.location.lat(),
                                    lng = results[0].geometry.location.lng(),
                                    placeName = results[0].address_components[0].long_name,
                                    latlng = new google.maps.LatLng(lat, lng);

                                moveMarker(placeName, latlng);
                                $("input").val(firstResult);
                                alert(firstResult)
                            }
                        });
                    }
                });
            });

            function moveMarker(placeName, latlng) {
                marker.setIcon(image);
                marker.setPosition(latlng);
                infowindow.setContent(placeName);
                //infowindow.open(map, marker);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <script src="http://maps.google.com/maps/api/js?libraries=places&region=uk&language=en&sensor=true"></script>

            Address:
            <input id="searchTextField" type="text" size="50" style="text-align: left; width: 357px; direction: ltr;">
            <br>
            latitude:<input name="latitude" class="MapLat" value="" type="text" placeholder="Latitude" style="width: 161px;" disabled>
            longitude:<input name="longitude" class="MapLon" value="" type="text" placeholder="Longitude" style="width: 161px;" disabled>

            <div id="map_canvas" style="height: 350px; width: 500px; margin: 0.6em;"></div>
        </div>
    </form>
</body>
</html>
