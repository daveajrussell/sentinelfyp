using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class DeliveryItemBuilder
    {
        public static AssignedDeliveryItem ToAssignedDeliveryItem(this DataSet oSet)
        {
            return (from item in oSet.FirstDataTableAsEnumerable()
                   select new AssignedDeliveryItem(item.Field<Guid>("CONSIGNMENT_KEY"), item.Field<Guid>("DELIVERY_ITEM_KEY"), item.Field<Guid>("RECIPIENT_KEY"))
                   {
                       RecipientFirstName = item.Field<string>("RECIPIENT_FIRST_NAME"),
                       RecipientLastName = item.Field<string>("RECIPIENT_LAST_NAME"),
                       RecipientAddress = item.Field<string>("RECIPIENT_ADDRESS_LINE_ONE"),
                       RecipientTown = item.Field<string>("RECIPIENT_TOWN"),
                       RecipientPostCode = item.Field<string>("RECIPIENT_POST_CODE")
                   }).First();
        }

        public static IEnumerable<AssignedDeliveryItem> ToAssignedDeliveryItemSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   select new AssignedDeliveryItem(item.Field<Guid>("CONSIGNMENT_KEY"), item.Field<Guid>("DELIVERY_ITEM_KEY"), item.Field<Guid>("RECIPIENT_KEY"))
                   {
                       RecipientFirstName = item.Field<string>("RECIPIENT_FIRST_NAME"),
                       RecipientLastName = item.Field<string>("RECIPIENT_LAST_NAME"),
                       RecipientAddress = item.Field<string>("RECIPIENT_ADDRESS_LINE_ONE"),
                       RecipientTown = item.Field<string>("RECIPIENT_TOWN"),
                       RecipientPostCode = item.Field<string>("RECIPIENT_POST_CODE")
                   };
        }

        public static IEnumerable<DeliveryItem> ToDeliveryItemSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   select new DeliveryItem(item.Field<Guid>("DELIVERY_ITEM_KEY"), item.Field<Guid>("RECIPIENT_KEY"))
                   {
                       RecipientFirstName = item.Field<string>("RECIPIENT_FIRST_NAME"),
                       RecipientLastName = item.Field<string>("RECIPIENT_LAST_NAME"),
                       RecipientAddress = item.Field<string>("RECIPIENT_ADDRESS_LINE_ONE"),
                       RecipientTown = item.Field<string>("RECIPIENT_TOWN"),
                       RecipientPostCode = item.Field<string>("RECIPIENT_POST_CODE")
                   };
        }
    }
}
