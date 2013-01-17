using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class DeliveryItem
    {
        public Guid DeliveryItemKey { get; set; }
        public Guid RecipientKey { get; set; }

        public DeliveryItem()
        {

        }
    }
}
