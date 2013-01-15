using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class CredentialsDataContract
    {
        [DataMember]
        public string strUsername { get; set; }
        [DataMember]
        public string strPassword { get; set; }
    }
}