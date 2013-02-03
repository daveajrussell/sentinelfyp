using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;
using Sentinel.SqlDataAccess;
using SqlRepositories.Helper.Builders;

namespace SqlRepositories
{
    public class SqlHistoricalTrackingRepository : IHistoricalTrackingRepository
    {
        private string _connectionString;

        public SqlHistoricalTrackingRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connection string");

            _connectionString = connectionString;
        }

        public IEnumerable<HistoricalGeospatialInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey)
        {
            var sqlParam = new SqlParameter("@IP_DRIVER_KEY", oDriverKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [daveajru].[GIS].[HISTORICAL_GEOSPATIAL_INFORMATION] WHERE [USER_KEY] = @IP_DRIVER_KEY", sqlParam))
            {
                return oSet.ToHistoricGeospatialInformationSet();
            }
        }

        public HistoricalGeospatialInformation GetFilteredHistoricalDataByDriverKey(Guid oDriverKey, int iSessionID)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_DRIVER_KEY", oDriverKey),
                new SqlParameter("@IP_SESSION_ID", iSessionID)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [daveajru].[GIS].[HISTORICAL_GEOSPATIAL_INFORMATION] WHERE [USER_KEY] = @IP_DRIVER_KEY AND HISTORICAL_SESSION_ID = @IP_SESSION_ID ORDER BY TIMESTAMP ASC", arrParams))
            {
                return oSet.ToHistoricGeospatialInformation();
            }
        }

        public IEnumerable<User> GetDrivers()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [SECURITY].[USER]"))
            {
                return oSet.ToUserSet();
            }
        }
    }
}
