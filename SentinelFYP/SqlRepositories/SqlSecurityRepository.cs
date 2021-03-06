﻿using System;
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
using DomainModel.Models.AuditModels;
using SqlRepositories.Helper.Extensions;
using DomainModel.Models.SecurityModels;

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

        public void LogIn(string strUserName, string strPassword, out User oUser, out Session oSession)
        {
            oUser = null;
            oSession = null;

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

                    LoginToSystem(oUserKey, strUserSalt, strUserHash, strPassword, out oUser, out oSession);
                }
            }
        }

        private void LoginToSystem(Guid oUserKey, string strUserSalt, string strUserHash, string strUserPassword, out User oUser, out Session oSession)
        {
            oUser = null;
            oSession = null;

            if (SecurityHelper.ValidatePassword(strUserPassword, strUserSalt, strUserHash))
            {
                var arrParams = new SqlParameter[] {
                        new SqlParameter("@IP_USER_KEY", oUserKey),
                        new SqlParameter("@IP_SALT", strUserSalt),
                        new SqlParameter("@IP_HASH", strUserHash)
                };

                using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[SECURITY].[LOGIN_TO_SYSTEM]", arrParams))
                {
                    oSession = oSet.ToSession();
                    oUser = oSet.ToUser();
                }
            }
        }

        public void Logout(Guid oUserKey, int iSessionID)
        {
            var arrParams = new SqlParameter[] 
            {
                new SqlParameter("@IP_USER_KEY", oUserKey),
                new SqlParameter("@IP_SESSION_ID", iSessionID),
                new SqlParameter("@IP_EXPIRY_REASON", "LOGOUT")
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[AUDIT].[END_SESSION]", arrParams);
        }

        public User GetUserByUserKey(Guid oUserKey)
        {
            var sqlParam = new SqlParameter("@IP_USER_KEY", oUserKey);

            using (var oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [SECURITY].[USER] WHERE [USER_KEY] = @IP_USER_KEY", sqlParam))
            {
                return oSet.ToUser();
            }
        }

        public IEnumerable<User> GetUsers(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey);

            using (var oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_DRIVERS]", sqlParam))
            {
                return oSet.ToUserSet();
            }
        }


        public bool ResetPassword(Guid oUserKey, string strSalt, string strHash)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_KEY", oUserKey),
                new SqlParameter("@IP_NEW_SALT", strSalt),
                new SqlParameter("@IP_NEW_HASH", strHash)
            };

            try
            {
                SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[SECURITY].[RESET_USER_PASSWORD]", arrParams);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public Vehicle GetUserVehicle(Guid oUserKey)
        {
            var sqlParam = new SqlParameter("@IP_USER_KEY", oUserKey);

            using (var oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[SECURITY].[GET_USER_ASSIGNED_VEHICLE]", sqlParam))
            {
                return oSet.ToVehicle();
            }
        }

        public IEnumerable<Role> GetRolesForUser(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_USER_KEY", oUser.UserKey);

            using (var oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[SECURITY].[GET_ROLES_FOR_USER]", sqlParam))
            {
                return oSet.ToRoleSet();
            }
        }

        public Guid CreateUser(string strUsername, Guid oCompanyKey, string strRoles, string strFirstName, string strLastName, string strNumber, string strEmail, string strSalt, string strHash)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_USER_NAME", strUsername),
                new SqlParameter("@IP_USER_COMPANY_KEY", oCompanyKey),
                new SqlParameter("@IP_ROLES", strRoles),
                new SqlParameter("@IP_FIRST_NAME", strFirstName),
                new SqlParameter("@IP_LAST_NAME", strLastName),
                new SqlParameter("@IP_NUMBER", strNumber),
                new SqlParameter("@IP_EMAIL", strEmail),
                new SqlParameter("@IP_SALT", strSalt),
                new SqlParameter("@IP_HASH", strHash)
            };

            using (var oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[SECURITY].[CREATE_USER]", arrParams))
            {
                return (from key in oSet.FirstDataTableAsEnumerable()
                        select key.Field<Guid>("USER_KEY")).First();
            }
        }
    }
}
