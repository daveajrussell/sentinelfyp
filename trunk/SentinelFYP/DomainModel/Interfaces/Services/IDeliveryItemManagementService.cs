using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface IDeliveryItemManagementService
    {
        IEnumerable<AssignedDeliveryItem> GetDeliveryItemsByKey(IEnumerable<Guid> oDeliveryItemKeys);
        IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems(User oUser);
        IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey);
        IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems(User oUser);
        void AssignDeliveryItemsToConsignment(IEnumerable<Guid> oDeliveryItemKeys, Guid oConsingmentKey);
        void UnAssignDeliveryItems(Guid oAssignmentKey, IEnumerable<Guid> oDeliveryItemKeys);
        IEnumerable<GeoTaggedDeliveryItem> GetGeotaggedDeliveryItems(User oUser);
    }
}
