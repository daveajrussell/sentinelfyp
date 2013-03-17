using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.SecurityModels
{
    public class Vehicle
    {
        public Guid VehicleKey { get; set; }
        public string VehicleRegistration { get; set; }
        public string VehicleManufacturer { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleYear { get; set; }
        public string VehicleColour { get; set; }
        public string VehicleWheelbase { get; set; }
        public string VehicleFuelType { get; set; }
    }
}
