using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models.GISModels;

namespace Sentinel.Services
{
    public static class SeverityHelper
    {
        private static string SEVERE = "severe";
        private static string CAUTION = "caution";
        private static string NORMAL = "normal";

        public static string Severity(GeospatialInformation oInformation)
        {
            if (oInformation.Speed <= 0)
                return CAUTION;

            if (oInformation.Orientation != 1 || oInformation.Speed >= 60)
                return SEVERE;

            else
                return NORMAL;
        }
    }
}