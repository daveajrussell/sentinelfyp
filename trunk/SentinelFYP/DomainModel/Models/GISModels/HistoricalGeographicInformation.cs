using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.GISModels
{
    public class HistoricalGeographicInformation
    {
        public DateTime Period { get; set; }
        public IEnumerable<GeographicInformation> PeriodGeographicData { get; set; }
    }
}
