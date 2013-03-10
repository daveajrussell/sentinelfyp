using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class AssignedConsignment : Consignment
    {
        public Guid AssignedDriverKey { get; set; }
        public string AssignedDriverFirstName { get; set; }
        public string AssignedDriverLastName { get; set; }
        public string AssignedDriverContactNumber { get; set; }
        public DateTime ConsignmentDateTime { get; set; }

        public AssignedConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey, string strAssignedDriverFirstName, string strAssignedDriverLastName, string strAssignedDriverContactNumber, DateTime dtConsignmentDateTime)
            : base(oConsignmentKey)
        {
            AssignedDriverKey = oAssignedDriverKey;
            AssignedDriverFirstName = strAssignedDriverFirstName;
            AssignedDriverLastName = strAssignedDriverLastName;
            AssignedDriverContactNumber = strAssignedDriverContactNumber;
            ConsignmentDateTime = dtConsignmentDateTime;
        }
    }
}
