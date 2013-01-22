using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
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
    }
}
