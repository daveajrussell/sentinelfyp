using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using System.Data;
using DomainModel.Models.GISModels;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class GISBuilder
    {
        public static GeospatialInformation ToGeographicInformation(this DataRow oRow)
        {
            DateTime _timestamp = oRow.Field<DateTime>("UPDATE_DATE_TIME");
            decimal _latitude = oRow.Field<decimal>("LATITUDE");
            decimal _longitude = oRow.Field<decimal>("LONGITUDE");
            decimal _speed = oRow.Field<decimal>("SPEED");
            int _orientation = oRow.Field<int>("ORIENTATION");
            return new GeospatialInformation(_timestamp, _latitude, _longitude, _speed, _orientation);
        }

        public static IEnumerable<HistoricalGeospatialInformation> ToGeographicInformationSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   orderby item.Field<DateTime>("TIMESTAMP") descending
                   group item by new { Period = item.Field<DateTime>("TIMESTAMP").Date, DriverKey = item.Field<Guid>("USER_KEY") } into g
                   select new HistoricalGeospatialInformation()
                   {
                       DriverKey = g.Key.DriverKey,
                       Period = g.Key.Period,
                       PeriodGeographicData = from item in g
                                              select new GeospatialInformation()
                                              {
                                                  TimeStamp = item.Field<DateTime>("TIMESTAMP"),
                                                  Latitude = item.Field<decimal>("LATITUDE"),
                                                  Longitude = item.Field<decimal>("LONGITUDE"),
                                                  Speed = item.Field<decimal>("SPEED"),
                                                  Orientation = item.Field<int>("ORIENTATION")
                                              }
                   };

        }

        public static HistoricalGeospatialInformation ToGeographicInformation(this DataSet oSet)
        {
            return (from item in oSet.FirstDataTableAsEnumerable()
                   orderby item.Field<DateTime>("TIMESTAMP") descending
                   group item by new { Period = item.Field<DateTime>("TIMESTAMP").Date, DriverKey = item.Field<Guid>("USER_KEY") } into g
                   select new HistoricalGeospatialInformation()
                   {
                       DriverKey = g.Key.DriverKey,
                       Period = g.Key.Period,
                       PeriodGeographicData = from item in g
                                              select new GeospatialInformation()
                                              {
                                                  TimeStamp = item.Field<DateTime>("TIMESTAMP"),
                                                  Latitude = item.Field<decimal>("LATITUDE"),
                                                  Longitude = item.Field<decimal>("LONGITUDE"),
                                                  Speed = item.Field<decimal>("SPEED"),
                                                  Orientation = item.Field<int>("ORIENTATION")
                                              }
                   }).First();
        }
    }
}
