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
        UnAssignedConsignment CreateConsignment(User oUser, DateTime dtConsignmentDeliveryDate);
        AssignedConsignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey);
        void UnAssignConsignment(Guid oConsignmentKey);
        IEnumerable<AssignedConsignment> GetAssignedConsignments(User oUser);
        IEnumerable<UnAssignedConsignment> GetUnAssignedConsignments(User oUser);
        AssignedConsignment GetConsignmentByDriverKey(Guid oDriverKey);
        IEnumerable<CompletedConsignment> GetCompletedConsignments(User oUser);
        IEnumerable<User> GetUsersForConsignmentAssigning(User oUser);
    }
}
