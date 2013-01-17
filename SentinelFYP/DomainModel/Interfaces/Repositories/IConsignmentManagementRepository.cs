using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Interfaces.Repositories
{
    public interface IConsignmentManagementRepository
    {
        UnAssignedConsignment CreateConsignment(DateTime dtConsignmentDeliveryDate);
        AssignedConsignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey);
        AssignedConsignment ReAssignConsignment(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey);
        void UnAssignConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey);
        IEnumerable<AssignedConsignment> GetAssignedConsignments();
        IEnumerable<UnAssignedConsignment> GetUnAssignedConsignments();
        AssignedConsignment GetConsignmentByDriverKey(Guid oDriverKey);
        IEnumerable<CompletedConsignment> GetCompletedConsignments();
    }
}
