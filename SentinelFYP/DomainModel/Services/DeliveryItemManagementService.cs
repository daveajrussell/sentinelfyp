using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;

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

        public IEnumerable<AssignedDeliveryItem> GetAllAssignedDeliveryItems()
        {
            return _repository.GetAllAssignedDeliveryItems();
        }

        public IEnumerable<AssignedDeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey)
        {
            return _repository.GetConsignmentDeliveryItems(oConsignmentKey);
        }

        public IEnumerable<DeliveryItem> GetAllUnassignedDeliveryItems()
        {
            return _repository.GetAllUnassignedDeliveryItems();
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
    }
}
