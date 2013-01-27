using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DomainModel.Abstracts;
using DomainModel.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Sentinel.Hubs;
using DomainModel.Models.GISModels;

namespace Sentinel.Services
{
    public class NotifierService : INotifierService
    {
        public void GISNotify(string message)
        {
            GeospatialInformationDataContract oGeoInformationContract = JsonR.JsonDeserializer<GeospatialInformationDataContract>(message);
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

            string strSeverity;

            if (oGeoInformation.Speed <= 0)
                strSeverity = "caution";
            else if (oGeoInformation.Orientation != 1)
                strSeverity = "severe";
            else
                strSeverity = "normal";

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SentinelHub>();
            context.Clients.Group(oGeoInformation.DriverKey.ToString(), null).getGISMessage(oGeoInformation.DriverKey.ToString(), oGeoInformation.TimeStamp.ToShortDateString(), oGeoInformation.Latitude, oGeoInformation.Longitude, oGeoInformation.Speed, oGeoInformation.Orientation, strSeverity);
            context.Clients.Group(oGeoInformation.DriverKey.ToString(), null).getUpdatedMap();
            context.Clients.All.allLocationUpdates(oGeoInformation.DriverKey.ToString(), oGeoInformation.TimeStamp.ToShortDateString(), oGeoInformation.Latitude, oGeoInformation.Longitude, oGeoInformation.Speed, oGeoInformation.Orientation, strSeverity);
        }
    }
}
