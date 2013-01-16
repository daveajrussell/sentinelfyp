using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class ConsignmentBuilder
    {
        public static Consignment ToConsignment(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                   select new Consignment()
                   {
                       ConsignmentKey = consignment.Field<Guid>("CONSIGNMENT_KEY"),
                       AssignedDriverKey = consignment.Field<Guid>("CONSIGNMENT_ASSIGNED_DRIVER_KEY"),
                       ConsignmentDeliveryDate = consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME")
                   }).First();
        }

        public static IEnumerable<Consignment> ToConsignmentSet(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                    select new Consignment()
                    {
                        ConsignmentKey = consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        AssignedDriverKey = consignment.Field<Guid>("CONSIGNMENT_ASSIGNED_DRIVER_KEY"),
                        ConsignmentDeliveryDate = consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME") 
                    });
        }
    }
}
