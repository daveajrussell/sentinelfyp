using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class Consignment
    {
        public Guid ConsignmentKey { get; set; }
        public Guid AssignedDriverKey { get; set; }
        public DateTime ConsignmentDeliveryDate { get; set; }

        public Consignment()
        {

        }

        public Consignment(Guid oConsignmentKey, DateTime dtConsignmentDeliveryDate)
        {
            ConsignmentKey = oConsignmentKey;
            ConsignmentDeliveryDate = dtConsignmentDeliveryDate;
        }

        public Consignment(DateTime dtConsignmentDeliveryDate)
        {
            ConsignmentDeliveryDate = dtConsignmentDeliveryDate;
        }
    }
}
