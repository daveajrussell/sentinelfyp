using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using WebServices.DataContracts;

namespace WebServices.Interfaces
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostGeospatialData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void PostGeospatialData(GeospatialInformationDataContract oGeoInformationContract);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostBufferedGeospatialDataSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void PostBufferedGeospatialDataSet(GeospatialInformationSetDataContract oGeoInformationSetContract);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostHistoricalData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void PostHistoricalData(GeospatialInformationDataContract oHistoricalGeoInformation);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostBufferedHistoricalData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void PostBufferedHistoricalData(GeospatialInformationSetDataContract oGeoInformationSetContract);
    }
}
