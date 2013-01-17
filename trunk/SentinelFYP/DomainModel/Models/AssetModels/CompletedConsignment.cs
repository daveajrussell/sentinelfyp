using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Models.AssetModels
{
    public class CompletedConsignment : Consignment
    {
        public Guid AssignedDriverKey { get; set; }
        public string AssignedDriverFirstName { get; set; }
        public string AssignedDriverLastName { get; set; }
        public long AssignedDriverContactNumber { get; set; }
        public DateTime ConsignmentDateTime { get; set; }
        public DateTime ConsignmentCompletedDateTime { get; set; }

        public CompletedConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey, string strAssignedDriverFirstName, string strAssignedDriverLastName, long lngAssignedDriverContactNumber, DateTime dtConsignmentDateTime, DateTime dtConsignmentCompletedDateTime)
            : base(oConsignmentKey)
        {
            AssignedDriverKey = oAssignedDriverKey;
            AssignedDriverFirstName = strAssignedDriverFirstName;
            AssignedDriverLastName = strAssignedDriverLastName;
            AssignedDriverContactNumber = lngAssignedDriverContactNumber;
            ConsignmentDateTime = dtConsignmentDateTime;
            ConsignmentCompletedDateTime = dtConsignmentCompletedDateTime;
        }
    }
}
