using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Models.AssetModels;

namespace SqlRepositories
{
    public class SqlDeliveryItemManagementRepository : IDeliveryItemManagementRepository
    {
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
