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
        public static AssignedConsignment ToConsignment(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                    select new AssignedConsignment(
                        consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        consignment.Field<Guid>("CONSIGNMENT_ASSIGNED_DRIVER_KEY"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<long>("USER_CONTACT_NUMBER"),
                        consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME")
                    )).First();
        }

        public static IEnumerable<AssignedConsignment> ToConsignmentSet(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                    select new AssignedConsignment(
                        consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        consignment.Field<Guid>("CONSIGNMENT_ASSIGNED_DRIVER_KEY"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<long>("USER_CONTACT_NUMBER"),
                        consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME")
                    ));
        }

        public static UnAssignedConsignment ToUnAssignedConsignment(this DataSet oSet)
        {
            return (from consignment in oSet.SecondDataTableAsEnumerable()
                    select new UnAssignedConsignment(
                        consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME")
                    )).First();
        }

        public static IEnumerable<UnAssignedConsignment> ToUnAssignedConsignmentSet(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                    select new UnAssignedConsignment(
                        consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME")
                    ));
        }

        public static IEnumerable<CompletedConsignment> ToCompletedConsignmentSet(this DataSet oSet)
        {
            return (from consignment in oSet.FirstDataTableAsEnumerable()
                    select new CompletedConsignment(
                        consignment.Field<Guid>("CONSIGNMENT_KEY"),
                        consignment.Field<Guid>("CONSIGNMENT_ASSIGNED_DRIVER_KEY"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<string>("USER_FIRST_NAME"),
                        consignment.Field<long>("USER_CONTACT_NUMBER"),
                        consignment.Field<DateTime>("CONSIGNMENT_DATE_TIME"),
                        consignment.Field<DateTime>("CONSIGNMENT_COMPLETED_DATE_TIME")
                    ));
        }
    }
}
