using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class DeliveryItem
    {
        public Guid DeliveryItemKey { get; set; }
        public Guid RecipientKey { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientTown { get; set; }
        public string RecipientPostCode { get; set; }

        public DeliveryItem(Guid deliveryItemKey, Guid recipientKey)
        {
            DeliveryItemKey = deliveryItemKey;
            RecipientKey = recipientKey;
        }
    }
}
