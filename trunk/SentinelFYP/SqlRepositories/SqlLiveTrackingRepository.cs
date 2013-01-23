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
    public class SqlLiveTrackingRepository : ILiveTrackingRepository
    {
        private string _connectionString;

        public SqlLiveTrackingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetLiveDrivers()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [SECURITY].[USER]"))
            {
                return oSet.ToUserSet();
            }
        }

        public GeospatialInformation GetLiveUpdate(Guid oUserKey, int iSessionID)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_KEY", oUserKey),
                new SqlParameter("@IP_SESSION_ID", iSessionID)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT TOP 1 FROM [AUDIT].[GEOSPATIAL_INFORMATION] ORDER BY [TIMESTAMP] DESC WHERE [SESSION_ID] = @IP_SESSION_ID AND [USER_KEY] = @IP_USER_KEY", arrParams))
            {
                return oSet.ToGeospatialInformation();
            }
        }

        public IEnumerable<GeospatialInformation> GetLiveElapsedRoute(Guid oUserKey, int iSessionID)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_KEY", oUserKey),
                new SqlParameter("@IP_SESSION_ID", iSessionID)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [AUDIT].[GEOSPATIAL_INFORMATION] WHERE [SESSION_ID] = @IP_SESSION_ID AND [USER_KEY] = @IP_USER_KEY", arrParams))
            {
                return oSet.ToGeospatialInformationSet();
            }
        }
    }
}
