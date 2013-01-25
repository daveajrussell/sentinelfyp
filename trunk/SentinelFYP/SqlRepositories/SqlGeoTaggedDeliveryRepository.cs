using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using Sentinel.SqlDataAccess;

namespace SqlRepositories
{
    public class SqlGeoTaggedDeliveryRepository : IGeoTaggedDeliveryRepository
    {
        private string _connectionString;

        public SqlGeoTaggedDeliveryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SubmitGeoTaggedDeliveryItem(DomainModel.Models.AssetModels.GeoTaggedDeliveryItem oItem)
        {
            var arrParams = new SqlParameter[]
            {
                //new SqlParameter("@IP_DRIVER_KEY", oItem.DriverKey),
                new SqlParameter("@IP_ASSET_KEY", "051CD6B1-DE50-465A-8427-04EA267ED442"),
                new SqlParameter("@IP_CONSIGNMENT_KEY", "D394FB4F-E0D7-45B4-8843-004F13CEA99B"),
                //new SqlParameter("@IP_SESSION_ID", oItem.SessionID),
                new SqlParameter("@IP_DELIVERY_DATE_TIME", oItem.TimeStamp),
                new SqlParameter("@IP_LATITUDE", oItem.Latitude),
                new SqlParameter("@IP_LONGITUDE", oItem.Longitude),
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.Text, "INSERT INTO [ASSET].[GEOTAGGED_DELIVERY] ([DELIVERY_ITEM_KEY], [CONSIGNMENT_KEY], [DELIVERY_MADE_DATE_TIME], [LATITUDE], [LONGITUDE]) VALUES (@IP_ASSET_KEY, @IP_CONSIGNMENT_KEY, @IP_DELIVERY_DATE_TIME, @IP_LATITUDE, @IP_LONGITUDE)", arrParams);
        }
    }
}
