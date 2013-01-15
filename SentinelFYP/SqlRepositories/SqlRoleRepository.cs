using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using Sentinel.SqlDataAccess;
using SqlRepositories.Helper.Builders;

namespace SqlRepositories
{
    public class SqlRoleRepository : IRoleRepository
    {
        private string _connectionString;

        public SqlRoleRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("conenction string");

            _connectionString = connectionString;
        }

        public string[] GetRolesForUser(string strUsername)
        {
            var sqlParam = new SqlParameter("@IP_USER_NAME", SqlDbType.VarChar).Value = strUsername;

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, "[SECURITY].[GET_ROLES_FOR_USER]", sqlParam))
            {
                return oSet.ToRoleSet();
            }
        }

        public bool IsUserInRole(string strUsername, string strRoleName)
        {
            var arrParams = new SqlParameter[] 
            {
                new SqlParameter("@IP_USER_NAME", strUsername),
                new SqlParameter("@IP_ROLE_NAME", strRoleName)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, "[SECURITY].[IS_USER_IN_ROLE]", arrParams))
            {
                if (oSet != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
