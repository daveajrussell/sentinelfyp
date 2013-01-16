using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Services
{
    public interface IDeliveryItemManagementService
    {
        IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems();
        DeliveryItem AssignDeliveryItemToConsignment(Guid oDeliveryItemKey, Guid oConsingmentKey);
        DeliveryItem ReAssignDeliveryItem(Guid oDeliveryItemKey, Guid oPreviousConsignmentKey, Guid ReAssignedConsignmentKey);
        DeliveryItem UnAssignDeliveryItem(Guid oDeliveryItemKey, Guid oAssignedConsignmentKey);
    }
}
