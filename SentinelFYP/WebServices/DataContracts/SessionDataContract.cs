using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class SessionDataContract
    {
        [DataMember]
        public string oUserIdentification { get; set; }
        [DataMember]
        public int iSessionID { get; set; }
    }
}