﻿using System;
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

        public void GeoTagDelivery(string strGeoTaggedDeliveryObject)
        {
            GeotaggedAssetDataContract oGeotaggedAssetContract = JsonR.JsonDeserializer<GeotaggedAssetDataContract>(strGeoTaggedDeliveryObject);
            GeoTaggedDeliveryItem oItem = new GeoTaggedDeliveryItem()
            {
                AssetKey = new Guid(oGeotaggedAssetContract.oAssetKey),
                DriverKey = new Guid(oGeotaggedAssetContract.oUserIdentification),
                SessionID = oGeotaggedAssetContract.iSessionID,
                TimeStamp = new DateTime(1970, 1, 1).AddMilliseconds(oGeotaggedAssetContract.lTimeStamp),
                Latitude = oGeotaggedAssetContract.dLatitude,
                Longitude = oGeotaggedAssetContract.dLongitude,
                Speed = oGeotaggedAssetContract.dSpeed,
                Orientation = oGeotaggedAssetContract.iOrientation
            };

            _service.SubmitGeoTaggedDeliveryItem(oItem);
        }
    }
}