using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IGeoTaggedDeliveryRepository
    {
        void SubmitGeoTaggedDeliveryItem(GeoTaggedDeliveryItem oItem);
    }
}
