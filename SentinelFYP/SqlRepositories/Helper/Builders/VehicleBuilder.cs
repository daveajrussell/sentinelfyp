using DomainModel.Models.SecurityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class VehicleBuilder
    {
        public static Vehicle ToVehicle(this DataSet oSet)
        {
            return (from item in oSet.FirstDataTableAsEnumerable()
                    select new Vehicle()
                    {
                        VehicleKey = item.Field<Guid>("VEHICLE_KEY"),
                        VehicleRegistration = item.Field<string>("VEHICLE_REGISTRATION"),
                        VehicleManufacturer = item.Field<string>("VEHICLE_MANUFACTURER"),
                        VehicleModel = item.Field<string>("VEHICLE_MODEL"),
                        VehicleYear = item.Field<int>("VEHICLE_YEAR"),
                        VehicleColour = item.Field<string>("VEHICLE_COLOUR"),
                        VehicleWheelbase = item.Field<string>("VEHICLE_WHEELBASE"),
                        VehicleFuelType = item.Field<string>("VEHICLE_FUEL_TYPE")
                    }).First();
        }
    }
}
