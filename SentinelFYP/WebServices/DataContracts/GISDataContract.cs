using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebServices.DataContracts
{
    [DataContract]
    public class GISDataContract
    {
        [DataMember]
        public long lngTimeStamp { get; set; }
        [DataMember]
        public string oUserIdentification { get; set; }
        [DataMember]
        public decimal dLatitude { get; set; }
        [DataMember]
        public decimal dLongitude { get; set; }
        [DataMember]
        public int intOrientation { get; set; }
        [DataMember]
        public decimal dSpeed { get; set; }
    }
}