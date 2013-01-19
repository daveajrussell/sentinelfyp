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

        public AssignedDeliveryItem GetDeliveryItemByKey(Guid oDeliveryItemKey)
        {
            return _repository.GetDeliveryItemByKey(oDeliveryItemKey);
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

        public void AssignDeliveryItemToConsignment(Guid oDeliveryItemKey, Guid oConsingmentKey)
        {
            _repository.AssignDeliveryItemToConsignment(oDeliveryItemKey, oConsingmentKey);
        }

        public void ReAssignDeliveryItem(Guid oDeliveryItemKey, Guid oPreviousConsignmentKey, Guid oReAssignedConsignmentKey)
        {
            _repository.ReAssignDeliveryItem(oDeliveryItemKey, oPreviousConsignmentKey, oReAssignedConsignmentKey);
        }

        public void UnAssignDeliveryItem(Guid oAssignmentKey, Guid oDeliveryItemKey)
        {
            _repository.UnAssignDeliveryItem(oAssignmentKey, oDeliveryItemKey);
        }
    }
}
