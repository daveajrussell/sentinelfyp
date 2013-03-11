var RealTimeMonitor = {

    MonitorVars: {
        ContactDetailsUrl:""
    },

    Init: function () {
        var self = this;
        $(function () {

            self.InitScrollBar();

            $('.dismiss').live('click', function () {
                var update = $(this).parents('div:first');
                self.RemoveUpdate(update);
            });

            $('.contact').live('click', function () {

                var key = $(this).attr('val');
                $.ajax({
                    url: self.MonitorVars.ContactDetailsUrl,
                    data: { strDriverKey: key },
                    success: function (data) {
                        $('#events-list').append(data);
                    }
                });
            });
        });
    },

    InitScrollBar: function () {
        $('#scrollbar1').tinyscrollbar();
    },

    WriteEvent: function (eventLog, logClass, driverKey) {
        var now = new Date();
        var nowStr = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();

        $('#events-list').prepend('<div class="update ui-state-error-' + logClass + ' ui-corner-all"> <span class="dismiss ui-icon ui-icon-close"></span> <p><span class="contact ui-icon ui-icon-contact" val=' + driverKey + '></span></p><p>' + eventLog + '</p></div>');

        this.InitScrollBar();
        this.DoAlert(logClass);
    },

    DoAlert: function (logClass) {
        if (logClass == 'caution') {
            for (var i = 0; i < 5; i++) {
                this.Flash('#FFFF00');
            }
        } else if (logClass == 'severe') {
            this.Flash('#CC0033');
        }
    },

    Flash: function (hexColour) {
        $("#events-list").stop().css("background-color", "transparent").animate({ backgroundColor: hexColour }, 500, function () {
            $("#events-list").stop().css("background-color", hexColour).animate({ backgroundColor: "transparent" }, 500, function () {
                $("#events-list").stop().css("background-color", "transparent").animate({ backgroundColor: hexColour }, 500, function () {
                    $("#events-list").stop().css("background-color", hexColour).animate({ backgroundColor: "transparent" }, 500, function () {
                        $("#events-list").stop().css("background-color", "transparent").animate({ backgroundColor: hexColour }, 500, function () {
                            $("#events-list").stop().css("background-color", hexColour).animate({ backgroundColor: "transparent" }, 500);
                        });
                    });
                });
            });
        });
    },

    RemoveUpdate: function (update) {
        $(update).slideUp("fast", function () {
            $(update).remove();
        });
        this.InitScrollBar();
    }
};