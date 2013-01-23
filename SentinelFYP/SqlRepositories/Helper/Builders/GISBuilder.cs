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
        public static IEnumerable<HistoricalGeospatialInformation> ToHistoricGeospatialInformationSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   orderby item.Field<DateTime>("TIMESTAMP") descending
                   group item by new { HistoricalSessionID = item.Field<int>("HISTORICAL_SESSION_ID"), Period = item.Field<DateTime>("TIMESTAMP").Date, DriverKey = item.Field<Guid>("USER_KEY") } into g
                   select new HistoricalGeospatialInformation()
                   {
                       HistoricalSessionID = g.Key.HistoricalSessionID,
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

        public static HistoricalGeospatialInformation ToHistoricGeospatialInformation(this DataSet oSet)
        {
            return (from item in oSet.FirstDataTableAsEnumerable()
                    orderby item.Field<DateTime>("TIMESTAMP") descending
                    group item by new { HistoricalSessionID = item.Field<int>("HISTORICAL_SESSION_ID"), Period = item.Field<DateTime>("TIMESTAMP").Date, DriverKey = item.Field<Guid>("USER_KEY") } into g
                    select new HistoricalGeospatialInformation()
                    {
                        HistoricalSessionID = g.Key.HistoricalSessionID,
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

        public static IEnumerable<GeospatialInformation> ToGeospatialInformationSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   select new GeospatialInformation()
                   {
                       SessionID = item.Field<int>("SESSION_ID"),
                       DriverKey = item.Field<Guid>("USER_KEY"),
                       TimeStamp = item.Field<DateTime>("TIMESTAMP"),
                       Latitude = item.Field<decimal>("LATITUDE"),
                       Longitude = item.Field<decimal>("LONGITUDE"),
                       Speed = item.Field<decimal>("SPEED"),
                       Orientation = item.Field<int>("ORIENTATION")
                   };
        }

        public static GeospatialInformation ToGeospatialInformation(this DataSet oSet)
        {
            return (from item in oSet.FirstDataTableAsEnumerable()
                    select new GeospatialInformation()
                    {
                        SessionID = item.Field<int>("SESSION_ID"),
                        DriverKey = item.Field<Guid>("USER_KEY"),
                        TimeStamp = item.Field<DateTime>("TIMESTAMP"),
                        Latitude = item.Field<decimal>("LATITUDE"),
                        Longitude = item.Field<decimal>("LONGITUDE"),
                        Speed = item.Field<decimal>("SPEED"),
                        Orientation = item.Field<int>("ORIENTATION")
                    }).First();
        }

    }
}
