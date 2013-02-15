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
                new SqlParameter("@IP_DELIVERY_ITEM_KEY", oItem.AssetKey),
                new SqlParameter("@IP_DELIVERY_ITEM_DRIVER_KEY", oItem.DriverKey),
                new SqlParameter("@IP_DELIVERY_DATE_TIME", oItem.TimeStamp),
                new SqlParameter("@IP_LATITUDE", oItem.Latitude),
                new SqlParameter("@IP_LONGITUDE", oItem.Longitude),
            };

            SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "[ASSET].[GEOTAG_DELIVERY]", arrParams);
        }
    }
}
