var arrOverlay = [];

var Heatmap = {

    Init: function () {
        var self = this;
        var map;

        $(function () {
            map = self.InitMap();
            arrOverlay = self.InitOverlay(map, arrOverlay);
            self.InitEventBinding(arrOverlay);
            map.overlayMapTypes.push(arrOverlay);
        });
    },

    InitOverlayForMonitor: function (map) {
        var self = this;

        $(function () {
            arrOverlay = self.InitOverlay(map, arrOverlay);
            self.InitEventBinding(arrOverlay);
            map.overlayMapTypes.push(arrOverlay);
        });
    },

    DestroyOverlayForMonitor: function () {
        arrOverlay.unbindAll();
    },

    InitMap: function () {

        var pos = new google.maps.LatLng(52, -1);

        // Create an array of Map options
        var mapOpts = {
            zoom: 3,
            center: pos,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        //Create a Map
        return new google.maps.Map(document.getElementById('map'), mapOpts); ;
    },

    InitOverlay: function (map, arrOverlay) {
        arrOverlay = new google.maps.ImageMapType({
            getTileUrl: function (tile, zoom) {

                var url = "Heatmap/Tile?colorScheme=classic&zoom=" + zoom + "&x=" + tile.x + "&y=" + tile.y + "&ran=" + Math.random();

                $(arrOverlay).trigger("overlay-busy");
                return url;
            },
            tileSize: new google.maps.Size(256, 256),
            isPng: true
        });

        return arrOverlay;
    },

    InitEventBinding: function (arrOverlay) {

        $(arrOverlay).bind("overlay-idle", function () {
            $.unblockUI();
        });

        $(arrOverlay).bind("overlay-busy", function () {
            $.blockUI({ css: {
                border: 'none',
                padding: '1.5em',
                backgroundColor: '#000',
                '-webkit-border-radius': '.5em',
                'border-radius': '.5em',
                opacity: .5,
                color: '#fff'
            },
                message: "<div class='overlay'><img class='overlay-img' src='Content/images/map_loader.gif' /></div>"
            });
        });

        google.maps.event.addListener(arrOverlay, 'tilesloaded', function () {
            $(arrOverlay).trigger("overlay-idle");
        });
    }
};

