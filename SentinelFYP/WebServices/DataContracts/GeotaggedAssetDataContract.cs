using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class GeotaggedAssetDataContract
    {
        [DataMember]
        public string oAssetKey { get; set; }
        [DataMember]
        public int iSessionID { get; set; }
        [DataMember]
        public string oUserIdentification { get; set; }
        [DataMember]
        public long lTimeStamp { get; set; }
        [DataMember]
        public decimal dLatitude { get; set; }
        [DataMember]
        public decimal dLongitude { get; set; }
        [DataMember]
        public decimal dSpeed { get; set; }
        [DataMember]
        public int iOrientation { get; set; }
    }
}