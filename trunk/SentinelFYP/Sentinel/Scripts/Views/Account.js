var Account = {

    Init: function () {

        var self = this;

        $(function () {

            self.HandleErrorMessageDisplay();

            $('input[type=submit]').each(function () {
                $(this).button();
            });
            
            $('#btn-submit').on('click', function (e) {
                $('#container').find('.test').remove();
                self.ValidateUsername(e);
                self.ValidatePassword(e);
            });

            $('#Username').watermark('Username');
            $('#Password').watermark('Password');
        });
    },

    ValidateUsername: function (e) {
        if ($('#Username').val() == '') {
            if ($('#Username').hasClass('input-error')) {
                e.preventDefault();
            } else {
                $('#Username').addClass('input-error');
                $('#Username').parent().append('<span id="username-error" class="ui-icon ui-icon-alert"></span>');

                $('#username-error').qtip({
                    content: 'Username Is Required',
                    style: {
                        classes: 'qtip-red'
                    }
                });

                e.preventDefault();
            }
        }
        else {
            $('#Username').removeClass('input-error');
            $('#Username').parent().find('.ui-icon').remove();
        }
    },

    ValidatePassword: function (e) {
        if ($('#Password').val() == '') {
            if ($('#Password').hasClass('input-error')) {
                e.preventDefault();
            } else {
                $('#Password').addClass('input-error');
                $('#Password').parent().append('<span id="password-error" class="ui-icon ui-icon-alert"></span>');

                $('#password-error').qtip({
                    content: 'Password Is Required',
                    style: {
                        classes: 'qtip-red'
                    }
                });

                e.preventDefault();
            }
        }
        else {
            $('#Password').removeClass('input-error');
            $('#Password').parent().find('.ui-icon').remove();
        }
    },

    HandleErrorMessageDisplay: function () {

        var message = $('.validation-summary-errors').html();

        alert(message);

        if (message != undefined) {
            $('.validation-summary-errors').hide();
            $('#container').append('<span id="val-error" class="ui-icon ui-icon-alert test"></span>');

            alert($('#container').html());

            $('#val-error').qtip({
                content: message,
                style: {
                    classes: 'qtip-red'
                }
            });
        }
    }
};

Account.Init();

