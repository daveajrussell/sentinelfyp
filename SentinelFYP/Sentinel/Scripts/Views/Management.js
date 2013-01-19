var Account = {

    Init: function () {
        var self = this;

        $(function () {
            $('#btn-get-assigned-consignments').on('click', function () {
                self.GetAssignedConsignments();
            });

            $('#btn-get-unassigned-consignments').on('click', function () {
                self.GetUnAssignedConsignments();
            });

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
    
    GetAssignedConsignments: function () {
        $.post('../AssetManagement/GetAssignedConsignments', function (data) {
            $('#div-consignments-grid').replaceWith(data);
        });
    },

    GetUnAssignedConsignments: function () {
        $.post('../AssetManagement/GetUnAssignedConsignments', function (data) {
            $('#div-consignments-grid').replaceWith(data);
        });
    },

    GetUnAssignedDeliveryItems: function (itemKey) {
        $.post('../AssetManagement/GetUnAssignDeliveryItemPartial', { strDeliveryItemKey: itemKey }, function (data) {
            $('#div-consignments-grid').after(data);
        });
    },

    GetConsignmentDeliveryItemsGrid: function (self, consignmentKey) {
        if ($(self).hasClass('ui-icon-plus')) {
            $(self).removeClass('ui-icon-plus').addClass('ui-icon-minus');

            $.post('../AssetManagement/GetConsignmentDeliveryItem', { strConsignmentKey: consignmentKey }, function (data) {
                $(self).parents('tr:first').after('<tr><td colspan="4">' + data + '</td></tr>').slideDown();
            });
        } else {
            $(this).removeClass('ui-icon-minus').addClass('ui-icon-plus');
            $(this).parents('tr:first').next().remove().slideUp();
        }
    }
};

Account.Init();