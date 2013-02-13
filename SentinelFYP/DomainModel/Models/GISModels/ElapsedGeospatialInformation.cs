using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.GISModels
{
    public class ElapsedGeospatialInformation
    {
        public int SessionID { get; set; }
        public Guid DriverKey { get; set; }
        public IEnumerable<GeospatialInformation> GeospatialInformation { get; set; }
    }
}
