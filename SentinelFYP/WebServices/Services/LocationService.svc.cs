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
using SentinelExceptionManagement;

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

        public void PostGeospatialData(GeospatialInformationDataContract oGeoInformationContract)
        {
            try
            {
                //GeospatialInformationDataContract oGeoInformationContract = JsonR.JsonDeserializer<GeospatialInformationDataContract>(strGeospatialDataJsonString);
                GeospatialInformation oGeoInformation = new GeospatialInformation
                {
                    SessionID = oGeoInformationContract.iSessionID,
                    DriverKey = new Guid(oGeoInformationContract.oUserIdentification),
                    TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(oGeoInformationContract.lTimeStamp),
                    Latitude = oGeoInformationContract.dLatitude,
                    Longitude = oGeoInformationContract.dLongitude,
                    Speed = oGeoInformationContract.dSpeed,
                    Orientation = oGeoInformationContract.iOrientation
                };

                _gisService.AddGeospatialInformation(oGeoInformation);

                Notify(oGeoInformationContract);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                throw ex;
            }
        }

        public void PostBufferedGeospatialDataSet(GeospatialInformationSetDataContract oGeoInformationSetContract)
        {
            try
            {
                //GeospatialInformationSetDataContract oGeoInformationSetContract = JsonR.JsonDeserializer<GeospatialInformationSetDataContract>(strBufferedGeospatialDataSetJsonString);
                var data = from geoInfo in oGeoInformationSetContract.BufferedData
                           select new GeospatialInformation()
                           {
                               SessionID = geoInfo.iSessionID,
                               DriverKey = new Guid(geoInfo.oUserIdentification),
                               TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(geoInfo.lTimeStamp),
                               Latitude = geoInfo.dLatitude,
                               Longitude = geoInfo.dLongitude,
                               Speed = geoInfo.dSpeed,
                               Orientation = geoInfo.iOrientation
                           };

                _gisService.AddGeospatialInformationSet(data);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                throw ex;
            }
        }

        public void PostHistoricalData(GeospatialInformationDataContract oGeoInformationContract)
        {
            try
            {
                GeospatialInformation oGeoInformation = new GeospatialInformation
                {
                    SessionID = oGeoInformationContract.iSessionID,
                    DriverKey = new Guid(oGeoInformationContract.oUserIdentification),
                    TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(oGeoInformationContract.lTimeStamp),
                    Latitude = oGeoInformationContract.dLatitude,
                    Longitude = oGeoInformationContract.dLongitude,
                    Speed = oGeoInformationContract.dSpeed,
                    Orientation = oGeoInformationContract.iOrientation
                };

                _gisService.AddHistoricalInformation(oGeoInformation);

                Notify(oGeoInformationContract);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                throw ex;
            }
        }

        public void PostBufferedHistoricalData(GeospatialInformationSetDataContract oGeoInformationSetContract)
        {
            try
            {
                //GeospatialInformationSetDataContract oGeoInformationSetContract = JsonR.JsonDeserializer<GeospatialInformationSetDataContract>(strBufferedHistoricalDataJsonString);
                var data = from geoInfo in oGeoInformationSetContract.BufferedData
                           select new GeospatialInformation()
                           {
                               SessionID = geoInfo.iSessionID,
                               DriverKey = new Guid(geoInfo.oUserIdentification),
                               TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(geoInfo.lTimeStamp),
                               Latitude = geoInfo.dLatitude,
                               Longitude = geoInfo.dLongitude,
                               Speed = geoInfo.dSpeed,
                               Orientation = geoInfo.iOrientation
                           };

                _gisService.AddHistoricalGeospatialInformationSet(data);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                throw ex;
            }
        }

        private void Notify(GeospatialInformationDataContract oGeoInformationContract)
        {
            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                using (var stream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(GeospatialInformationDataContract));
                    data.WriteObject(stream, oGeoInformationContract);
                    client.UploadData("http://fyp.daveajrussell.com/Services/NotifierService.svc/GISNotify", "POST", stream.ToArray());
                }
            }
        }
    }
}
