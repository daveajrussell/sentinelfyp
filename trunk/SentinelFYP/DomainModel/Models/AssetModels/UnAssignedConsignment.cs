using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class UnAssignedConsignment : Consignment
    {
        public DateTime ConsignmentDateTime { get; set; }

        public UnAssignedConsignment(Guid oConsignmentKey, DateTime dtConsignmentDateTime)
            : base(oConsignmentKey)
        {
            ConsignmentDateTime = dtConsignmentDateTime;
        }
    }
}
