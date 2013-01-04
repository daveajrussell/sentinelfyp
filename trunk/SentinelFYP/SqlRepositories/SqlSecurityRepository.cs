using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.SecurityModels;
using System.Data.SqlClient;
using Sentinel.SqlDataAccess;
using System.Data;
using SqlRepositories.Helper.Builders;
using SqlRepositories.Helper;
using System.Net;

namespace SqlRepositories
{
    public class SqlSecurityRepository : ISecurityRepository
    {
        private readonly string _connectionString;

        public SqlSecurityRepository(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connection string");

            _connectionString = connectionString;
        }

        public User LogIn(string strUserName, string strPassword, string strUserAgent, string strIPAddress)
        {
            string strUserSalt;
            string strUserHash;
            Guid oUserKey;

            using (SqlDataReader oReader = SqlHelper.ExecuteReader(_connectionString, CommandType.StoredProcedure, "[SECURITY].[ATTEMPT_LOGIN]", new SqlParameter("@IP_USERNAME", strUserName)))
            {
                if (oReader.Read())
                {
                    strUserSalt = (string)oReader[oReader.GetOrdinal("USER_SALT")];
                    strUserHash = (string)oReader[oReader.GetOrdinal("USER_HASH")];
                    oUserKey = (Guid)oReader[oReader.GetOrdinal("USER_KEY")];

                    return LoginToSystem(oUserKey, strUserSalt, strUserHash, strPassword, strUserAgent, strIPAddress);
                }
            }
            return null;
        }

        private User LoginToSystem(Guid oUserKey, string strUserSalt, string strUserHash, string strUserPassword, string strUserAgent, string strIPAddress)
        {
            if (SecurityHelper.ValidatePassword(strUserPassword, strUserSalt, strUserHash))
            {
                var arrParams = new SqlParameter[] {
                        new SqlParameter("@IP_USER_KEY", oUserKey),
                        new SqlParameter("@IP_SALT", strUserSalt),
                        new SqlParameter("@IP_HASH", strUserHash),
                        new SqlParameter("@IP_USER_AGENT", strUserAgent),
                        new SqlParameter("@IP_IP_ADDRESS", strIPAddress)
                };

                using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[SECURITY].[LOGIN_TO_SYSTEM]", arrParams))
                {
                    State.Session = oSet.ToSession();
                    return oSet.ToUser();
                }
            }
            return null;
        }

        public void Logout(int iSessionID)
        {
            var arrParams = new SqlParameter[] {
                new SqlParameter("@IP_SESSION_ID", iSessionID),
                new SqlParameter("@IP_EXPIRY_REASON", "LOGOUT")
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[AUDIT].[END_SESSION]", arrParams);
        }
    }
}
