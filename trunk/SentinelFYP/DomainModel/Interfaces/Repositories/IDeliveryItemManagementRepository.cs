using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IDeliveryItemManagementRepository
    {
        IEnumerable<AssignedDeliveryItem> GetDeliveryItemsByKey(IEnumerable<Guid> oDeliveryItemKeys);
        IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems(User oUser);
        IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey);
        IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems(User oUser);
        void AssignDeliveryItemsToConsignment(IEnumerable<Guid> oDeliveryItemKeys, Guid oConsignmentKey);
        void ReAssignDeliveryItems(IEnumerable<Guid> oDeliveryItemKeys, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey);
        void UnAssignDeliveryItems(Guid oAssignmentKey, IEnumerable<Guid> oDeliveryItemKeys);
        IEnumerable<GeoTaggedDeliveryItem> GetGeotaggedDeliveryItems(User oUser);
    }
}
