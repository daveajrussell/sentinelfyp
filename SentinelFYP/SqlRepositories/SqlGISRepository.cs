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
            var arrParams = new SqlParameter[] {
                new SqlParameter("@IP_SESSION_ID", oGeoInformation.SessionID),
                new SqlParameter("@IP_USER_KEY", oGeoInformation.DriverKey),
                new SqlParameter("@IP_TIME_STAMP", oGeoInformation.TimeStamp),
                new SqlParameter("@IP_LATITUDE", oGeoInformation.Latitude),
                new SqlParameter("@IP_LONGITUDE", oGeoInformation.Longitude),
                new SqlParameter("@IP_SPEED", oGeoInformation.Speed),
                new SqlParameter("@IP_ORIENTATION", oGeoInformation.Orientation)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, /*"[GIS].[ADD_GIS]"*/ "[GIS].[ADD_GEOSPATIAL_INFORMATION]", arrParams);
        }

        public void AddGeospatialInformationSet(IEnumerable<GeospatialInformation> oGeoInformationSet)
        {
            var oXmlString = GetXmlString(oGeoInformationSet);
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_SESSION_ID", oGeoInformationSet.First().SessionID),
                new SqlParameter("@IP_USER_KEY", oGeoInformationSet.First().DriverKey),
                new SqlParameter("@IP_XML", oXmlString)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_GEOSPATIAL_INFORMATION_SET]", arrParams);
        }

        private string GetXmlString(IEnumerable<GeospatialInformation> oGeoInformationSet)
        {
            return new XDocument(
                new XElement(
                    "GEOSPATIAL_INFORMATION",
                    from item in oGeoInformationSet
                    select new XElement("GEO_ITEM",
                        new XAttribute("TIMESTAMP", item.SessionID),
                        new XAttribute("LATITUDE", item.SessionID),
                        new XAttribute("LONGITUDE", item.SessionID),
                        new XAttribute("SPEED", item.SessionID),
                        new XAttribute("ORIENTATION", item.SessionID)))).ToString();
        }

        /*public GeospatialInformation GetGIS()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_GIS]"))
            {
                return oSet.ToGeospatialInformation();
            }
        }


        public void AddGIS(GeospatialInformation oGIS)
        {
            var arrParams = new SqlParameter[] {
                new SqlParameter("@IP_TIME_STAMP", oGIS.TimeStamp),
                new SqlParameter("@IP_LATITUDE", oGIS.Latitude),
                new SqlParameter("@IP_LONGITUDE", oGIS.Longitude),
                new SqlParameter("@IP_ORIENTATION", oGIS.Orientation),
                new SqlParameter("@IP_SPEED", oGIS.Speed)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[GIS].[ADD_GIS]", arrParams);
        }


        public IEnumerable<GeospatialInformation> GetAllGISData()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_ALL_GIS_DATA]"))
            {
                return oSet.ToGeospatialInformationSet();
            }
        }*/
    }
}
