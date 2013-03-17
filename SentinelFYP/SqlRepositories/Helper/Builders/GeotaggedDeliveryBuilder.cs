using DomainModel.Models.AssetModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class GeotaggedDeliveryBuilder
    {
        public static IEnumerable<GeoTaggedDeliveryItem> ToGeotaggedDeliveriesSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   select new GeoTaggedDeliveryItem()
                   {
                       AssetKey = item.Field<Guid>("DELIVERY_ITEM_KEY"),
                       DriverKey = item.Field<Guid>("USER_KEY"),
                       DriverFirstName = item.Field<string>("USER_FIRST_NAME"),
                       DriverLastName = item.Field<string>("USER_LAST_NAME"),
                       TimeStamp = item.Field<DateTime>("DELIVERY_MADE_DATE_TIME"),
                       Latitude = item.Field<decimal>("LATITUDE"),
                       Longitude = item.Field<decimal>("LONGITUDE")
                   };
        }
    }
}
