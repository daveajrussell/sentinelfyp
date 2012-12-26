var Monitor = {

    Init: function (lat, lng, arrGISInfo) {

        var self = this;
        var map;
        var pos;
        var arrOverlay = new Array();

        $(function () {
            $('input[type=button]').each(function () {
                $(this).button();
            });

            $('#btn-show-live').on('click', function () {
                $.post('Monitor/GetUpdate', function (data) {
                    $('#map-container').replaceWith(data);
                });
            });

            $('#btn-show-route').on('click', function () {
                $.post('Monitor/GetRoute', function (data) {
                    $('#map-container').replaceWith(data);
                });
            });

            $('#btn-show-heatmap').on('click', function () {
                Heatmap.InitOverlayForMonitor(map);
                self.ToggleButtons();
            });

            $('#btn-hide-heatmap').on('click', function () {
                self.Init(lat, lng, arrGISInfo);
                self.ToggleButtons();
            });

            // Create a LatLng object to set to the center
            pos = new google.maps.LatLng(lat, lng);

            // Init map
            map = self.InitMap(pos);

            // Init the display
            self.InitDisplay(self, map, arrGISInfo);
        });
    },

    InitMap: function (pos) {

        // Create an array of Map options
        var mapOpts = {
            zoom: 7,
            center: pos,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        //Create a Map
        return new google.maps.Map(document.getElementById('map'), mapOpts); ;
    },

    InitPolyline: function (map, path) {

        // Setup Polylines
        poly = new google.maps.Polyline({
            path: path,
            strokeColor: '#000000',
            strokeOpacity: 1.0,
            strokeWeight: 3
        });

        poly.setMap(map);
    },

    InitMarkerListener: function (map, marker, GISInfoWindow) {
        // Create a listener
        google.maps.event.addListener(marker, 'click', function () {
            GISInfoWindow.open(map, marker);
        });
    },

    InitDisplay: function (self, map, arrGISInfo) {
        var path = [];
        $.each(arrGISInfo, function (index, value) {
            var latlng = new google.maps.LatLng(value[0],
                                                value[1]);
            path.push(latlng);

            self.CreateGISMarker(self, map, latlng, value);
        });

        // Init polyline
        self.InitPolyline(map, path);
    },

    CreateGISMarker: function (self, map, position, GISInfo) {

        var marker = new google.maps.Marker({
            position: position,
            map: map,
            title: "GIS Information"
        });

        var GISInfoWindow = self.CreateGISInfoWindow(GISInfo)
        self.InitMarkerListener(map, marker, GISInfoWindow);
    },

    CreateGISInfoWindow: function (arrInfo) {
        var gisInfo = '<div id="gis-info">' +
                          '<span>Date: ' + arrInfo[2] + '</span> <br/>' +
                          '<span>Time: ' + arrInfo[3] + '<br />' +
                          '<span>Speed: ' + arrInfo[4] + '</span> <br />' +
                          '<span>Orientation: ' + arrInfo[5] + '</span>' +
                      '</div>';

        return new google.maps.InfoWindow({
            content: gisInfo
        });
    },

    ToggleButtons: function () {
        $('#btn-show-heatmap').toggle();
        $('#btn-hide-heatmap').toggle();
    }

};