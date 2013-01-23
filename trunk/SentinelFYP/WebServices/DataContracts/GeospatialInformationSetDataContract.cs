using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.DataContracts
{
    [DataContract]
    public class GeospatialInformationSetDataContract
    {
        [DataMember]
        public IEnumerable<GeospatialInformationDataContract> BufferedData { get; set; }
    }
}