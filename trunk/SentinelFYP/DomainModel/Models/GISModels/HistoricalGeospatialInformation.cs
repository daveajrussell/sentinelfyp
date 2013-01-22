using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;

namespace DomainModel.Models.GISModels
{
    public class HistoricalGeospatialInformation
    {
        public Guid DriverKey { get; set; }
        public DateTime Period { get; set; }
        public IEnumerable<GeospatialInformation> PeriodGeographicData { get; set; }
    }
}
