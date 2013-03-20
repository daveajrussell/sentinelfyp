using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models;
using System.Data;
using Sentinel.SqlDataAccess;
using SqlRepositories.Helper.Builders;
using System.Data.SqlClient;
using SqlRepositories.Helper.Extensions;
using SqlRepositories.Helper.Builders;
using DomainModel.Models.GISModels;
using System.Xml.Linq;
using SentinelExceptionManagement;

namespace SqlRepositories
{
    public class SqlGISRepository : IGISRepository
    {
        private readonly string _connectionString;

        public SqlGISRepository(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connection string");

            _connectionString = connectionString;
        }

        public void AddGeospatialInformation(GeospatialInformation oGeoInformation)
        {
            var oXmlString = GetXmlString(oGeoInformation);
            var sqlParam = new SqlParameter("@IP_XML", oXmlString);

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_GEOSPATIAL_INFORMATION]", sqlParam);
        }

        public void AddGeospatialInformationSet(IEnumerable<GeospatialInformation> oGeoInformationSet)
        {
            var oXmlString = GetXmlString(oGeoInformationSet);
            var sqlParam = new SqlParameter("@IP_XML", oXmlString);

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_GEOSPATIAL_INFORMATION_SET]", sqlParam);
        }

        public void AddHistoricalInformation(GeospatialInformation oHistoricalGeoInformation)
        {
            var oXmlString = GetXmlString(oHistoricalGeoInformation);
            var sqlParam = new SqlParameter("@IP_XML", oXmlString);

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_HISTORICAL_GEOSPATIAL_INFORMATION]", sqlParam);
        }

        public void AddHistoricalGeospatialInformationSet(IEnumerable<GeospatialInformation> oHistoricalGeoInformationSet)
        {
            var oXmlString = GetXmlString(oHistoricalGeoInformationSet);
            var sqlParam = new SqlParameter("@IP_XML", oXmlString);

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_HISTORICAL_GEOSPATIAL_INFORMATION]", sqlParam);
        }

        private string GetXmlString(GeospatialInformation oGeoInformation)
        {
            return new XDocument(
                new XElement("GEOSPATIAL_INFORMATION",
                    new XElement("GEO_ITEM",
                        new XAttribute("SESSION_ID", oGeoInformation.SessionID),
                        new XAttribute("DRIVER_KEY", oGeoInformation.DriverKey),
                        new XAttribute("TIMESTAMP", oGeoInformation.TimeStamp),
                        new XAttribute("LATITUDE", oGeoInformation.Latitude),
                        new XAttribute("LONGITUDE", oGeoInformation.Longitude),
                        new XAttribute("SPEED", oGeoInformation.Speed),
                        new XAttribute("ORIENTATION", oGeoInformation.Orientation)))).ToString();
        }

        private string GetXmlString(IEnumerable<GeospatialInformation> oGeoInformationSet)
        {
            return new XDocument(
                new XElement(
                    "GEOSPATIAL_INFORMATION",
                    from item in oGeoInformationSet
                    select new XElement("GEO_ITEM",
                        new XAttribute("SESSION_ID", item.SessionID),
                        new XAttribute("DRIVER_KEY", item.DriverKey),
                        new XAttribute("TIMESTAMP", item.TimeStamp),
                        new XAttribute("LATITUDE", item.Latitude),
                        new XAttribute("LONGITUDE", item.Longitude),
                        new XAttribute("SPEED", item.Speed),
                        new XAttribute("ORIENTATION", item.Orientation)))).ToString();
        }
    }
}
