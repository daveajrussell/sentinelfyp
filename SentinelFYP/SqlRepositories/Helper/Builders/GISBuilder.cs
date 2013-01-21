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
        public static GeographicInformation ToGeographicInformation(this DataRow oRow)
        {
            DateTime _timestamp = oRow.Field<DateTime>("UPDATE_DATE_TIME");
            decimal _latitude = oRow.Field<decimal>("LATITUDE");
            decimal _longitude = oRow.Field<decimal>("LONGITUDE");
            decimal _speed = oRow.Field<decimal>("SPEED");
            int _orientation = oRow.Field<int>("ORIENTATION");
            return new GeographicInformation(_timestamp, _latitude, _longitude, _speed, _orientation);
        }

        public static IEnumerable<HistoricalGeographicInformation> ToGeographicInformationSet(this DataSet oSet)
        {
            return from item in oSet.FirstDataTableAsEnumerable()
                   orderby item.Field<DateTime>("TIMESTAMP") descending
                   group item by item.Field<DateTime>("TIMESTAMP").Date into g
                   select new HistoricalGeographicInformation()
                   {
                       Period = g.Key,
                       Items =  from item in g.AsEnumerable()
                                select new GeographicInformation()
                                {
                                    TimeStamp = item.Field<DateTime>("TIMESTAMP"),
                                    Latitude = item.Field<decimal>("LATITUDE"),
                                    Longitude = item.Field<decimal>("LONGITUDE"),
                                    Speed = item.Field<decimal>("SPEED"),
                                    Bearing = item.Field<int>("BEARING"),
                                    Altitude = item.Field<int>("ALTITUDE"),
                                    Orientation = item.Field<int>("ORIENTATION")
                                }
                   };
                   
        }
    }
}
