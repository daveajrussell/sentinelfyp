using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class UserDataContract
    {
        [DataMember]
        public Guid oUserIdentification { get; set; }
    }
}