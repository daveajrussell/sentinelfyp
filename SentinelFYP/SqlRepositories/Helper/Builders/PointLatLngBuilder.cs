using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using System.Data;

namespace SqlRepositories.Helper.Builders
{
    public static class PointLatLngBuilder
    {
        public static PointLatLng ToPointLatLng(this DataRow row)
        {
            double _lat = double.Parse(row.Field<decimal>("LATITUDE").ToString());
            double _lng = double.Parse(row.Field<decimal>("LONGITUDE").ToString());
            return new PointLatLng(_lat, _lng);
        }
    }
}
