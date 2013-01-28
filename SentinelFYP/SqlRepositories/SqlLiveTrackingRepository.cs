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
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT T1.* FROM [SECURITY].[USER] T1 INNER JOIN [AUDIT].[SESSION] T2 ON T1.USER_KEY = T2.USER_KEY"))
            {
                return oSet.ToUserSet();
            }
        }

        public GeospatialInformation GetLiveUpdate(Guid oUserKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_KEY", oUserKey)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT TOP 1 * FROM [GIS].[GEOSPATIAL_INFORMATION] WHERE [USER_KEY] = @IP_USER_KEY ORDER BY [TIMESTAMP] DESC", arrParams))
            {
                return oSet.ToGeospatialInformation();
            }
        }

        public IEnumerable<GeospatialInformation> GetLiveElapsedRoute(Guid oUserKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_KEY", oUserKey)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [GIS].[GEOSPATIAL_INFORMATION] WHERE [USER_KEY] = @IP_USER_KEY ORDER BY [TIMESTAMP] DESC", arrParams))
            {
                return oSet.ToGeospatialInformationSet();
            }
        }
    }
}
