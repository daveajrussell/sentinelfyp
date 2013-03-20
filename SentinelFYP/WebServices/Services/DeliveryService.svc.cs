using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using Ninject;
using WebServices.DataContracts;
using WebServices.Interfaces;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WebServices.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IGeoTaggedDeliveryService _service;

        public DeliveryService(IGeoTaggedDeliveryService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;
        }

        public void GeoTagDelivery(GeotaggedAssetDataContract oGeotaggedAssetContract)
        {
            //GeotaggedAssetDataContract oGeotaggedAssetContract = JsonR.JsonDeserializer<GeotaggedAssetDataContract>(strGeoTaggedDeliveryObject);
            GeoTaggedDeliveryItem oItem = new GeoTaggedDeliveryItem()
            {
                AssetKey = new Guid(oGeotaggedAssetContract.oAssetKey),
                DriverKey = new Guid(oGeotaggedAssetContract.oUserIdentification),
                TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(oGeotaggedAssetContract.lTimeStamp),
                Latitude = oGeotaggedAssetContract.dLatitude,
                Longitude = oGeotaggedAssetContract.dLongitude,
            };

            _service.SubmitGeoTaggedDeliveryItem(oItem);

            Notify(oGeotaggedAssetContract);
        }

        public void UnTagDelivery(string strAssetKey)
        {
            AssetKeyDataContract oAssetKey = JsonR.JsonDeserializer<AssetKeyDataContract>(strAssetKey);
            _service.UnTagDelivery(Guid.Parse(oAssetKey.oAssetKey));
        }

        private void Notify(GeotaggedAssetDataContract oGeotaggedAssetContract)
        {
            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                using (var stream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(GeotaggedAssetDataContract));
                    data.WriteObject(stream, oGeotaggedAssetContract);
                    client.UploadData("http://fyp.daveajrussell.com/Services/NotifierService.svc/DeliveryNotify", "POST", stream.ToArray());
                    //client.UploadData("http://localhost/Sentinel/Services/NotifierService.svc/DeliveryNotify", "POST", stream.ToArray());
                }
            }
        }
    }
}
