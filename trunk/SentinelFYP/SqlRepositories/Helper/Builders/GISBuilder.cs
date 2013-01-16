using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models;
using System.Data;
using DomainModel.Models.GISModels;

namespace SqlRepositories.Helper.Builders
{
    public static class GISBuilder
    {
        public static GIS ToGis(this DataRow oRow)
        {
            DateTime _timestamp = oRow.Field<DateTime>("UPDATE_DATE_TIME");
            decimal _latitude = oRow.Field<decimal>("LATITUDE");
            decimal _longitude = oRow.Field<decimal>("LONGITUDE");
            decimal _speed = oRow.Field<decimal>("SPEED");
            int _orientation = oRow.Field<int>("ORIENTATION");
            return new GIS(_timestamp, _latitude, _longitude, _speed, _orientation);
        }
    }
}
