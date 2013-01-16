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

        public Consignment CreateConsignment(DateTime dtConsignmentDeliveryDate)
        {
            var sqlParam = new SqlParameter("@IP_CONSIGNMENT_DELIVERY_DATE_TIME", dtConsignmentDeliveryDate);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[CREATE_CONSIGNMENT]", sqlParam))
            {
                return oSet.ToConsignment();
            }
        }

        public Consignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey)
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

        public Consignment ReAssignConsignment(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey)
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

        public void UnAssignConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey)
        {
            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey),
                new SqlParameter("@IP_ASSIGNED_DRIVER_KEY", oAssignedDriverKey)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[UNASSIGN_CONSIGNMENT]", arrParams);
        }

        public IEnumerable<Consignment> GetAllConsignments()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ALL_CONSIGNMENTS]"))
            {
                return oSet.ToConsignmentSet();
            }
        }

        public IEnumerable<Consignment> GetAssignedConsignments()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ASSIGNED_CONSIGNMENTS]"))
            {
                return oSet.ToConsignmentSet();
            }
        }

        public IEnumerable<Consignment> GetUnAssignedConsignments()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_UNASSIGNED_CONSIGNMENTS]"))
            {
                return oSet.ToConsignmentSet();
            }
        }

        public Consignment GetConsignmentByDriverKey(Guid oDriverKey)
        {
            var sqlParam = new SqlParameter("@IP_DRIVER_KEY", oDriverKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_CONSIGNMENT_BY_DRIVER_KEY]", sqlParam))
            {
                return oSet.ToConsignment();
            }
        }
        
        public IEnumerable<Consignment> GetCompletedConsignments()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_COMPLETED_CONSIGNMENTS]"))
            {
                return oSet.ToConsignmentSet();
            }
        }
    }
}
