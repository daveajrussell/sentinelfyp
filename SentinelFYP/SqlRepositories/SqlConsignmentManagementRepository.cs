using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AssetModels;
using Sentinel.SqlDataAccess;
using SqlRepositories.Helper.Builders;
using DomainModel.SecurityModels;

namespace SqlRepositories
{
    public class SqlConsignmentManagementRepository : IConsignmentManagementRepository
    {
        private string _connectionString;

        public SqlConsignmentManagementRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connection string");

            _connectionString = connectionString;
        }

        public UnAssignedConsignment CreateConsignment(User oUser, DateTime dtConsignmentDeliveryDate)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey),
                new SqlParameter("@IP_CONSIGNMENT_DELIVERY_DATE_TIME", dtConsignmentDeliveryDate)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[CREATE_CONSIGNMENT]", arrParams))
            {
                return oSet.ToUnAssignedConsignment();
            }
        }

        public AssignedConsignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey),
                new SqlParameter("@IP_DRIVER_KEY", oDriverKey)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[ASSIGN_CONSIGNMENT_TO_DRIVER]", arrParams))
            {
                return oSet.ToConsignment();
            }
        }

        public AssignedConsignment ReAssignConsignment(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey),
                new SqlParameter("@IP_PREVIOUS_DRIVER_KEY", oPreviousDriverKey),
                new SqlParameter("@IP_REASSIGNED_DRIVER_KEY", oReAssignedDriverKey)
            };

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[REASSIGN_CONSIGNMENT]", arrParams))
            {
                return oSet.ToConsignment();
            }
        }

        public void UnAssignConsignment(Guid oConsignmentKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[UNASSIGN_CONSIGNMENT]", arrParams);
        }

        public IEnumerable<AssignedConsignment> GetAssignedConsignments(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ASSIGNED_CONSIGNMENTS]", sqlParam))
            {
                return oSet.ToConsignmentSet();
            }
        }

        public IEnumerable<UnAssignedConsignment> GetUnAssignedConsignments(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_UNASSIGNED_CONSIGNMENTS]", sqlParam))
            {
                return oSet.ToUnAssignedConsignmentSet();
            }
        }

        public AssignedConsignment GetConsignmentByDriverKey(Guid oDriverKey)
        {
            var sqlParam = new SqlParameter("@IP_DRIVER_KEY", oDriverKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_CONSIGNMENT_BY_DRIVER_KEY]", sqlParam))
            {
                return oSet.ToConsignment(); 
            }
        }

        public IEnumerable<CompletedConsignment> GetCompletedConsignments(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_COMPLETED_CONSIGNMENTS]", sqlParam))
            {
                return oSet.ToCompletedConsignmentSet();
            }
        }

        public IEnumerable<User> GetUsersForConsignmentAssigning(User oUser)
        {
            var sqlParam = new SqlParameter("@IP_COMPANY_KEY", oUser.UserCompanyKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_DRIVERS_FOR_ASSIGNING_CONSIGNMENT]", sqlParam))
            {
                return oSet.ToUserSet();
            }
        }
    }
}
