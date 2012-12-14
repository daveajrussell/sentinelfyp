using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using System.Data;
using Sentinel.SqlDataAccess;
using System.Data.SqlClient;
using SqlRepositories.Helper.Builders;

namespace SqlRepositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connection string");
            
            _connectionString = connectionString;

        }
    }
}
