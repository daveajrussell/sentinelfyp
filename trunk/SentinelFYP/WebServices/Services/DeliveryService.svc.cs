using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebServices.Interfaces;

namespace WebServices.Services
{
    public class DeliveryService : IDeliveryService
    {
        public void SubmitGeoTaggedDelivery(string srGeoTaggedDeliveryObject)
        {
            throw new NotImplementedException();
        }


        public string GetDeliveryInformation(string strItemID)
        {
            throw new NotImplementedException();
        }
    }
}
