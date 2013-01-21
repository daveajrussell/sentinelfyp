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

        public IEnumerable<HistoricalGeographicInformation> GetAllHistoricalTrackingDataByDriverKey(Guid oDriverKey)
        {
            var sqlParam = new SqlParameter("@IP_DRIVER_KEY", oDriverKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [daveajru].[GIS].[GEOSPATIAL_INFORMATION]", sqlParam))
            {
                return oSet.ToGeographicInformationSet();
            }
        }

        public HistoricalGeographicInformation GetFilteredHistoricalDataByDriverKey(Guid oDriverKey, DateTime oRange)
        {
            var dateFrom = oRange.Date.ToString("yyyy-MM-dd HH:mm:ss");
            var dateTo = oRange.AddDays(1).Date.ToString("yyyy-MM-dd HH:mm:ss");

            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_DRIVER_KEY", oDriverKey),
                new SqlParameter("@IP_DATE_RANGE_FROM", dateFrom),
                new SqlParameter("@IP_DATE_RANGE_TO", dateTo)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [daveajru].[GIS].[GEOSPATIAL_INFORMATION] WHERE TIMESTAMP BETWEEN @IP_DATE_RANGE_FROM AND @IP_DATE_RANGE_TO", arrParams))
            {
                return oSet.ToGeographicInformation();
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
