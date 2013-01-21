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
using DomainModel.Models.GISModels;

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

        public GeographicInformation GetGIS()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_GIS]"))
            {
                return (from data in oSet.FirstDataTableAsEnumerable()
                        select data.ToGeographicInformation()).First();
            }
        }


        public void AddGIS(GeographicInformation oGIS)
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


        public IEnumerable<GeographicInformation> GetAllGISData()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_ALL_GIS_DATA]"))
            {
                return from data in oSet.FirstDataTableAsEnumerable()
                       select data.ToGeographicInformation();
            }
        }
    }
}
