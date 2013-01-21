using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models.AssetModels;

namespace Sentinel.Models
{
    public class AssignDeliveryItemViewModel
    {
        public Guid ConsignmentKey { get; set; }
        public DeliveryItem Item { get; set; }
    }
}