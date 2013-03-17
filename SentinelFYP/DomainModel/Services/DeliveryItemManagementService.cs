using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;

namespace DomainModel.Services
{
    public class DeliveryItemManagementService : IDeliveryItemManagementService
    {
        private IDeliveryItemManagementRepository _repository;

        public DeliveryItemManagementService(IDeliveryItemManagementRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("Repository");

            _repository = repository;
        }

        public IEnumerable<AssignedDeliveryItem> GetDeliveryItemsByKey(IEnumerable<Guid> oDeliveryItemKeys)
        {
            return _repository.GetDeliveryItemsByKey(oDeliveryItemKeys);
        }

        public IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems(User oUser)
        {
            return _repository.GetAllAssignedDeliveryItems(oUser);
        }

        public IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey)
        {
            return _repository.GetConsignmentDeliveryItems(oConsignmentKey);
        }

        public IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems(User oUser)
        {
            return _repository.GetAllUnassignedDeliveryItems(oUser);
        }

        public void AssignDeliveryItemsToConsignment(IEnumerable<Guid> oDeliveryItemKeys, Guid oConsingmentKey)
        {
            _repository.AssignDeliveryItemsToConsignment(oDeliveryItemKeys, oConsingmentKey);
        }

        public void ReAssignDeliveryItems(IEnumerable<Guid> oDeliveryItemKeys, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey)
        {
            _repository.ReAssignDeliveryItems(oDeliveryItemKeys, oPreviousConsignmentKey, oReAssignedConsignmentKey);
        }

        public void UnAssignDeliveryItems(Guid oAssignmentKey, IEnumerable<Guid> oDeliveryItemKeys)
        {
            _repository.UnAssignDeliveryItems(oAssignmentKey, oDeliveryItemKeys);
        }

        public IEnumerable<GeoTaggedDeliveryItem> GetGeotaggedDeliveryItems(User oUser)
        {
            return _repository.GetGeotaggedDeliveryItems(oUser);
        }
    }
}
