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

        public IEnumerable<DeliveryItem> GetAllAssignedDeliveryItems()
        {
            return _repository.GetAllAssignedDeliveryItems();
        }

        public IEnumerable<DeliveryItem> GetConsignmentDeliveryItems(Guid oConsignmentKey)
        {
            return _repository.GetConsignmentDeliveryItems(oConsignmentKey);
        }

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
