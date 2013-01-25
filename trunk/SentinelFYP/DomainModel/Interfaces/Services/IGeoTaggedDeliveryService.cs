using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Services
{
    public interface IGeoTaggedDeliveryService
    {
        void SubmitGeoTaggedDeliveryItem(GeoTaggedDeliveryItem oItem);
    }
}
