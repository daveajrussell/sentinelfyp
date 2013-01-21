using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.GISModels;
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
    }
}
