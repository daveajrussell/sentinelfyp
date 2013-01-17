using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Services
{
    public interface IConsignmentManagementService
    {
        Consignment CreateConsignment(DateTime dtConsignmentDeliveryDate);
        Consignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey);
        Consignment ReAssignConsignment(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey);
        void UnAssignConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey);
        IEnumerable<Consignment> GetAllConsignments();
        IEnumerable<Consignment> GetAssignedConsignments();
        IEnumerable<Consignment> GetUnAssignedConsignments();
        Consignment GetConsignmentByDriverKey(Guid oDriverKey);
        IEnumerable<Consignment> GetCompletedConsignments();
    }
}
