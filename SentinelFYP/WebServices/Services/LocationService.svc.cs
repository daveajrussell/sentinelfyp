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
        private IPointService _pointService;
        private IGISService _gisService;

        public LocationService(IPointService pointService, IGISService gisService)
        {
            if (pointService == null)
                throw new ArgumentNullException("point service");

            _pointService = pointService;

            if (gisService == null)
                throw new ArgumentNullException("gis service");

            _gisService = gisService;
        }

        public void PostGISData(string strGISObject)
        {
            GISDataContract obj = JsonR.JsonDeserializer<GISDataContract>(strGISObject);
            GIS oGIS = new GIS
            {
                TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(obj.lngTimeStamp),
                Latitude = obj.dLatitude,
                Longitude = obj.dLongitude,
                Speed = obj.dSpeed,
                Orientation = obj.intOrientation
            };

            try
            {
                _gisService.AddGIS(oGIS);
            }
            catch (Exception e)
            {
                // log
            }

            Notify(strGISObject);
        }

        private void Notify(string strGISObject)
        {
            using (var client = new WebClient())
            {
                client.Headers["Content-Type"] = "application/json";
                using (var stream = new MemoryStream())
                {
                    var data = new DataContractJsonSerializer(typeof(GISDataContract));
                    data.WriteObject(stream, strGISObject);
                    client.UploadData("http://fyp.daveajrussell.com/Services/NotifierService.svc/GISNotify", "POST", stream.ToArray());
                }
            }
        }
    }
}
