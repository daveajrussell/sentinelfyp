using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class AssetKeyDataContract
    {
        [DataMember]
        public string oAssetKey { get; set; }
    }
}