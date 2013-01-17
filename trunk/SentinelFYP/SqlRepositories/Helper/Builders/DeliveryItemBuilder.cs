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
        public static IEnumerable<DeliveryItem> ToDeliveryItemSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   select new DeliveryItem()
                   {
                       DeliveryItemKey = item.Field<Guid>("DELIVERY_ITEM_KEY")
                   };
        }
    }
}
