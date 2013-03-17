using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class GeoTaggedDeliveryItem
    {
        public Guid AssetKey { get; set; }
        public Guid DriverKey { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
