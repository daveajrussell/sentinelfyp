using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using DomainModel.Interfaces.Services;
using System.Net;
using DomainModel.Models;
using DomainModel.Abstracts;
using WebServices.DataContracts;
using WebServices.Interfaces;
using DomainModel.Models.GISModels;

namespace WebServices.Services
{
    public class LocationService : ILocationService
    {
        private IGISService _gisService;

        public LocationService(IGISService gisService)
        {
            if (gisService == null)
                throw new ArgumentNullException("gis service");

            _gisService = gisService;
        }

        public void PostGeospatialData(string strGeospatialDataJsonString)
        {
            GeospatialInformationDataContract oGeoInformationContract = JsonR.JsonDeserializer<GeospatialInformationDataContract>(strGeospatialDataJsonString);
            GeospatialInformation oGeoInformation = new GeospatialInformation
            {
                SessionID = oGeoInformationContract.iSessionID,
                DriverKey = new Guid(oGeoInformationContract.oUserIdentification),
                TimeStamp = new DateTime(oGeoInformationContract.lTimeStamp),//new DateTime(1970, 1, 1).AddMilliseconds(oGeoInformationContract.lTimeStamp),
                Latitude = oGeoInformationContract.dLatitude,
                Longitude = oGeoInformationContract.dLongitude,
                Speed = oGeoInformationContract.dSpeed,
                Orientation = oGeoInformationContract.iOrientation
            };

            _gisService.AddGeospatialInformation(oGeoInformation);

            Notify(strGeospatialDataJsonString);
        }

        public void PostBufferedGeospatialDataSet(string strBufferedGeospatialDataSetJsonString)
        {
            throw new NotImplementedException();
        }

        private void Notify(string strGISObject)
        {
            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                using (var stream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(GeospatialInformationDataContract));
                    data.WriteObject(stream, strGISObject);
                    //client.UploadData("http://fyp.daveajrussell.com/Services/NotifierService.svc/GISNotify", "POST", stream.ToArray());
                    client.UploadData("http://localhost/Sentinel/Services/NotifierService.svc/GISNotify", "POST", stream.ToArray());
                }
            }
        }
    }
}
