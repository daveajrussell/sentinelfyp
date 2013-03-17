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
using Sentinel.Services.DataContracts;
using DomainModel.Models.AssetModels;

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

            string strSeverity = SeverityHelper.Severity(oGeoInformation);

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SentinelHub>();
            context.Clients.Group(oGeoInformation.DriverKey.ToString(), null).getGISMessage(oGeoInformation.DriverKey.ToString(), oGeoInformation.TimeStamp.ToShortDateString(), oGeoInformation.Latitude, oGeoInformation.Longitude, oGeoInformation.Speed, oGeoInformation.Orientation, strSeverity);
            context.Clients.Group(oGeoInformation.DriverKey.ToString(), null).getUpdatedMap();
            context.Clients.All.allLocationUpdates(oGeoInformation.DriverKey.ToString(), oGeoInformation.TimeStamp.ToShortDateString(), oGeoInformation.Latitude, oGeoInformation.Longitude, oGeoInformation.Speed, oGeoInformation.Orientation, strSeverity);
        }

        public void DeliveryNotify(string message)
        {
            GeotaggedAssetDataContract oGeotaggedAssetContract = JsonR.JsonDeserializer<GeotaggedAssetDataContract>(message);
            GeoTaggedDeliveryItem oItem = new GeoTaggedDeliveryItem()
            {
                AssetKey = new Guid(oGeotaggedAssetContract.oAssetKey),
                DriverKey = new Guid(oGeotaggedAssetContract.oUserIdentification),
                TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(oGeotaggedAssetContract.lTimeStamp),
                Latitude = oGeotaggedAssetContract.dLatitude,
                Longitude = oGeotaggedAssetContract.dLongitude,
            };

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SentinelHub>();
            context.Clients.All.deliveryUpdate(oItem.DriverKey, oItem.TimeStamp.ToShortDateString(), SeverityHelper.NORMAL);
        }
    }
}
