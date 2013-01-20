var Account = {

    Init: function () {
        var self = this;

        $(function () {
            /*$('#GetAssignedConsignments').on('click', function () {
                self.GetAssignedConsignments();
            });

            $('#GetUnAssignedConsignments').on('click', function () {
                self.GetUnAssignedConsignments();
            });*/

            $('.btn-unassign').on('click', function () {
                var itemKey = $(this).attr('value');
                self.GetUnAssignedDeliveryItems(itemKey);
            });

            $('.show').on('click', function () {
                var consignmentKey = $(this).attr('value');
                self.GetConsignmentDeliveryItemsGrid(this, consignmentKey);
            });
        });

    },
    /*
    GetAssignedConsignments: function () {
        $.post('../AssetManagement/GetAssignedConsignments', function (data) {
            $('.consignment-management').replaceWith(data);
        });
    },

    GetUnAssignedConsignments: function () {
        $.post('../AssetManagement/GetUnAssignedConsignments', function (data) {
            $('.consignment-management').replaceWith(data);
        });
    },*/

    GetUnAssignedDeliveryItems: function (itemKey) {
        $.post('../AssetManagement/GetUnAssignDeliveryItemPartial', { strDeliveryItemKey: itemKey }, function (data) {
            $('.consignment-management').after(data);
        });
    },

    GetConsignmentDeliveryItemsGrid: function (self, consignmentKey) {
        if ($(self).hasClass('ui-icon-plus')) {
            $(self).removeClass('ui-icon-plus').addClass('ui-icon-minus');

            $.post('../AssetManagement/GetConsignmentDeliveryItem', { strConsignmentKey: consignmentKey }, function (data) {
                alert('yeahno');
                $(self).parents('tr:first').after('<tr class="child"><td colspan="4">' + data + '</td></tr>').slideDown();
            });
        } else {
            $(this).removeClass('ui-icon-minus').addClass('ui-icon-plus');
            //$(this).parents('tr:first').next().remove().slideUp();
            $(this).find('.child').remove().slideUp();
        }
    }
};

Account.Init();