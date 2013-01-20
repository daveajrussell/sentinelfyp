using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Services
{
    public interface IDeliveryItemManagementService
    {
        IEnumerable<AssignedDeliveryItem> GetDeliveryItemsByKey(IEnumerable<Guid> oDeliveryItemKeys);
        IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems();
        IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey);
        IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems();
        void AssignDeliveryItemsToConsignment(IEnumerable<Guid> oDeliveryItemKeys, Guid oConsingmentKey);
        void ReAssignDeliveryItems(IEnumerable<Guid> oDeliveryItemKeys, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey);
        void UnAssignDeliveryItems(Guid oAssignmentKey, IEnumerable<Guid> oDeliveryItemKeys);
    }
}
