using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IDeliveryItemManagementRepository
    {
        AssignedDeliveryItem GetDeliveryItemByKey(Guid oDeliveryItemKey);
        IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems();
        IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey);
        IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems();
        void AssignDeliveryItemToConsignment(Guid oDeliveryItemKey, Guid oConsingmentKey);
        void ReAssignDeliveryItem(Guid oDeliveryItemKey, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey);
        void UnAssignDeliveryItem(Guid oAssignmentKey, Guid oDeliveryItemKey);
    }
}
