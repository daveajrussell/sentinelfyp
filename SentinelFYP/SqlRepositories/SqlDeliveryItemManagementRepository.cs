using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AssetModels;
using Sentinel.SqlDataAccess;
using SqlRepositories.Helper.Builders;

namespace SqlRepositories
{
    public class SqlDeliveryItemManagementRepository : IDeliveryItemManagementRepository
    {
        private string _connectionString;

        public SqlDeliveryItemManagementRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Connection String");

            _connectionString = connectionString;
        }

        public IEnumerable<AssignedDeliveryItem> GetDeliveryItemsByKey(IEnumerable<Guid> oDeliveryItemKeys)
        {
            var oXml = GetXDoc(oDeliveryItemKeys);

            var sqlParam = new SqlParameter("@IP_DELIVERY_ITEM_KEYS", oXml.ToString());

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_DELIVERY_ITEMS_BY_KEY]", sqlParam))
            {
                return oSet.ToAssignedDeliveryItemSet();
            }
        }

        public IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ALL_ASSIGNED_DELIVERY_ITEMS]"))
            {
                return oSet.ToAssignedDeliveryItemSet();
            }
        }

        public IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey)
        {
            var sqlParam = new SqlParameter("@IP_CONSIGNMENT_KEY", oConsignmentKey);

            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ALL_CONSIGNMENT_DELIVERY_ITEMS]", sqlParam))
            {
                return oSet.ToAssignedDeliveryItemSet();
            }
        }

        public IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems()
        {
            using (DataSet oSet = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "[ASSET].[GET_ALL_UNASSIGNED_DELIVERY_ITEMS]"))
            {
                return oSet.ToDeliveryItemSet();
            }
        }

        public void AssignDeliveryItemsToConsignment(IEnumerable<Guid> oDeliveryItemKeys, Guid oConsingmentKey)
        {
            var arrParams = new SqlParameter[]
            {
                //new SqlParameter("@IP_DELIVERY_ITEM_KEY", oDeliveryItemKeys), change to XML
                new SqlParameter("@IP_CONSIGNMENT_KEY", oConsingmentKey)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[ASSIGN_DELIVERY_ITEM_TO_CONSIGNMENT]", arrParams);
        }

        public void ReAssignDeliveryItems(IEnumerable<Guid> oDeliveryItemKeys, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey)
        {
            var arrParams = new SqlParameter[]
            {
                //new SqlParameter("@IP_DELIVERY_ITEM_KEY", oDeliveryItemKeys), change to XML
                new SqlParameter("@IP_PREVIOUS_CONSIGNMENT_KEY", oPreviousConsignmentKey),
                new SqlParameter("@IP_REASSIGNED_CONSIGNMENT_KEY", oReAssignedConsignmentKey)
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[REASSIGN_DELIVERY_ITEM]", arrParams);
        }

        public void UnAssignDeliveryItems(Guid oAssignmentKey, IEnumerable<Guid> oDeliveryItemKeys)
        {
            var oXml = GetXDoc(oDeliveryItemKeys);

            var arrParams = new SqlParameter[]
            {
                new SqlParameter("@IP_CONSIGNMENT_KEY", oAssignmentKey),
                new SqlParameter("@IP_DELIVERY_ITEM_KEYS", oXml.ToString())
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[UNASSIGN_DELIVERY_ITEMS]", arrParams);
        }

        private XDocument GetXDoc(IEnumerable<Guid> oKeys)
        {
            return new XDocument(new XElement("ITEM_KEYS", from key in oKeys
                                                           select new XElement("ITEM", new XAttribute("KEY", key))));
        }
    }
}
