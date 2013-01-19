using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class AssignedDeliveryItem : DeliveryItem
    {
        public Guid ConsignmentKey { get; set; }

        public AssignedDeliveryItem(Guid oConsignmentKey, Guid oDeliveryItemKey, Guid oRecipientKey)
            : base(oDeliveryItemKey, oRecipientKey)
        {
            ConsignmentKey = oConsignmentKey;
        }
    }
}
