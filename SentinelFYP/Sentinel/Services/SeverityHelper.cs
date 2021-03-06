﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models.GISModels;

namespace Sentinel.Services
{
    public static class SeverityHelper
    {
        public static string SEVERE = "severe";
        public static string CAUTION = "caution";
        public static string NORMAL = "normal";

        public static string Severity(GeospatialInformation oInformation)
        {

            if (oInformation.Orientation != 1 && oInformation.Speed <= 0 || oInformation.Speed >= 60)
                return SEVERE;

            if (oInformation.Speed <= 0 || oInformation.Orientation != 1)
                return CAUTION;

            else
                return NORMAL;
        }
    }
}