var Account = {

    Init: function () {

        var self = this;

        $(function () {
            $('input[type=submit]').each(function () {
                $(this).button();
            });

            $('#btn-submit').on('click', function () {
                self.Validate();
            });

            $('#Username').watermark('Username');
            $('#Password').watermark('Password');
        });
    },

    Validate: function () {

        $('input[type=text],[type=password]').each(function () {
            if ($(this).val() == '') {
                $(this).addClass('input-error');
                $(this).parent().append('<span class="ui-icon ui-icon-alert"></span>');
            }
            else {
                $(this).removeClass('input-error');
                $(this).parent().find('.ui-icon').remove();
            }
        });
    }
};

Account.Init();

