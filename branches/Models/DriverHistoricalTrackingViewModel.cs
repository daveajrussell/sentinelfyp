using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;

namespace Sentinel.Models
{
    public class DriverHistoricalTrackingViewModel
    {
        public User Driver { get; set; }
        public IEnumerable<GeospatialInformation> GeoInformation { get; set; }
    }
}