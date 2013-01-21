using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.GISModels
{
    public class GeographicInformation
    {
        public DateTime TimeStamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Speed { get; set; }
        public int Bearing { get; set; }
        public int Altitude { get; set; }
        public int Orientation { get; set; }

        public GeographicInformation()
        {

        }

        public GeographicInformation(DateTime dtStamp, decimal dLatitude, decimal dLongitude, decimal dSpeed, int iOrientation)
        {
            TimeStamp = dtStamp;
            Latitude = dLatitude;
            Longitude = dLongitude;
            Speed = dSpeed;
            Orientation = iOrientation;
        }
    }
}
