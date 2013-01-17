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
    public class SqlDeliveryItemManagementRepository : IDeliveryItemManagementRepository
    {
        private string _connectionString;

        public IEnumerable<DeliveryItem> GetAllAssignedDeliveryItems()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [ASSET].[CONSIGNMENT_ITEMS]"))
            {
                return oSet.ToDeliveryItemSet();
            }
        }

        public IEnumerable<DeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey)
        {
            var sqlParam = new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.Text, "SELECT * FROM [ASSET].[CONSIGNMENT_ITEMS] WHERE CONSIGNMENT_KEY = @IP_CONSIGNMENT_KEY", sqlParam))
            {
                return oSet.ToDeliveryItemSet();
            }
        }

        public SqlDeliveryItemManagementRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Connection String");

            _connectionString = connectionString;
        }

        public IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems()
        {
            throw new NotImplementedException();
        }

        public DeliveryItem AssignDeliveryItemToConsignment(Guid oDeliveryItemKey, Guid oConsingmentKey)
        {
            throw new NotImplementedException();
        }

        public DeliveryItem ReAssignDeliveryItem(Guid oDeliveryItemKey, Guid oPreviousConsignmentKey, Guid ReAssignedConsignmentKey)
        {
            throw new NotImplementedException();
        }

        public DeliveryItem UnAssignDeliveryItem(Guid oDeliveryItemKey, Guid oAssignedConsignmentKey)
        {
            throw new NotImplementedException();
        }
    }
}
