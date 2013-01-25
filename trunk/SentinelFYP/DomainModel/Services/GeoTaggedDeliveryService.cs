using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;

namespace DomainModel.Services
{
    public class GeoTaggedDeliveryService : IGeoTaggedDeliveryService
    {
        private IGeoTaggedDeliveryRepository _repository;

        public GeoTaggedDeliveryService(IGeoTaggedDeliveryRepository repository)
        {
            _repository = repository;
        }

        public void SubmitGeoTaggedDeliveryItem(Models.AssetModels.GeoTaggedDeliveryItem oItem)
        {
            _repository.SubmitGeoTaggedDeliveryItem(oItem);
        }
    }
}
