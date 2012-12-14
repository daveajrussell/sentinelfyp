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

        public GIS GetGIS()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_GIS]"))
            {
                return (from data in oSet.FirstDataTableAsEnumerable()
                        select data.ToGis()).First();
            }
        }


        public void AddGIS(GIS oGIS)
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


        public IEnumerable<GIS> GetAllGISData()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[GIS].[GET_ALL_GIS_DATA]"))
            {
                return from data in oSet.FirstDataTableAsEnumerable()
                       select data.ToGis();
            }
        }
    }
}
