using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;

namespace DomainModel.Services
{
    public class GeoTaggedDeliveryService : IGeoTaggedDeliveryService
    {
        private IGeoTaggedDeliveryRepository _repository;

        public GeoTaggedDeliveryService(IGeoTaggedDeliveryRepository repository)
        {
            if (null == repository)
                throw new ArgumentNullException("Repository");

            _repository = repository;
        }

        public void SubmitGeoTaggedDeliveryItem(GeoTaggedDeliveryItem oItem)
        {
            _repository.SubmitGeoTaggedDeliveryItem(oItem);
        }

        public void UnTagDelivery(Guid oAssetKey)
        {
            _repository.UnTagDelivery(oAssetKey);
        }
    }
}
