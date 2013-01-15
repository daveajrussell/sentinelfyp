using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMap.NET;
using System.Data;

namespace SqlRepositories.Helper.Extensions
{
    public static class DataSetExtensions
    {
        public static IEnumerable<DataRow> FirstDataTableAsEnumerable(this DataSet set)
        {
            return set.Tables[0].AsEnumerable();
        }

        public static IEnumerable<DataRow> SecondDataTableAsEnumerable(this DataSet set)
        {
            return set.Tables[1].AsEnumerable();
        }
    }
}