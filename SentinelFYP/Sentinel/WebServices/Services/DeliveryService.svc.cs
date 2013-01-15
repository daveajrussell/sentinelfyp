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
        public void SubmitGeoTaggedDelivery(string strGeoTaggedDeliveryObject)
        {
            if (string.IsNullOrEmpty(strGeoTaggedDeliveryObject))
                throw new ArgumentNullException("Delivery Object");
        }


        public string GetDeliveryInformation(string strItemID)
        {
            if (string.IsNullOrEmpty(strItemID))
                throw new ArgumentNullException("Item ID");

            return "{\"TestData\": \"Test\"}";
        }
    }
}
