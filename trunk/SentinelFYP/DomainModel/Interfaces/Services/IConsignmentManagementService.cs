using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;

namespace DomainModel.Interfaces.Services
{
    public interface IConsignmentManagementService
    {
        UnAssignedConsignment CreateConsignment(DateTime dtConsignmentDeliveryDate);
        AssignedConsignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey);
        void UnAssignConsignment(Guid oConsignmentKey);
        IEnumerable<AssignedConsignment> GetAssignedConsignments();
        IEnumerable<UnAssignedConsignment> GetUnAssignedConsignments();
        AssignedConsignment GetConsignmentByDriverKey(Guid oDriverKey);
        IEnumerable<CompletedConsignment> GetCompletedConsignments();
        IEnumerable<User> GetUsersForConsignmentAssigning();
    }
}
