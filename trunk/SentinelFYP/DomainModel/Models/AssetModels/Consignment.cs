using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class Consignment
    {
        public Guid ConsignmentKey { get; set; }

        public Consignment(Guid consignmentKey)
        {
            ConsignmentKey = consignmentKey;
        }
    }
}
