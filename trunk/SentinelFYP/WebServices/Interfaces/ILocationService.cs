﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebServices.Interfaces
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostGeospatialData", RequestFormat = WebMessageFormat.Json)]
        void PostGeospatialData(string strGeospatialDataJsonString);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/PostBufferedGeospatialDataSet", RequestFormat = WebMessageFormat.Json)]
        void PostBufferedGeospatialDataSet(string strBufferedGeospatialDataSetJsonString);
    }
}
