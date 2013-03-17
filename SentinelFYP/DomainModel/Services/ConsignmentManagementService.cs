using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;

namespace DomainModel.Services
{
    public class ConsignmentManagementService : IConsignmentManagementService
    {
        private IConsignmentManagementRepository _consignmentManagementRepository;

        public ConsignmentManagementService(IConsignmentManagementRepository consignmentManagementRepository)
        {
            if (consignmentManagementRepository == null)
                throw new ArgumentNullException("repository");

            _consignmentManagementRepository = consignmentManagementRepository;
        }

        public UnAssignedConsignment CreateConsignment(User oUser, DateTime dtConsignmentDeliveryDate)
        {
            return _consignmentManagementRepository.CreateConsignment(oUser, dtConsignmentDeliveryDate);
        }

        public AssignedConsignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey)
        {
            if (oConsignmentKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Consignment Key");

            if (oDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            return _consignmentManagementRepository.AssignConsignmentToDriver(oConsignmentKey, oDriverKey);
        }

        public void UnAssignConsignment(Guid oConsignmentKey)
        {
            if (oConsignmentKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Consignment Key");

            _consignmentManagementRepository.UnAssignConsignment(oConsignmentKey);
        }

        public IEnumerable<AssignedConsignment> GetAssignedConsignments(User oUser)
        {
            return _consignmentManagementRepository.GetAssignedConsignments(oUser);
        }

        public IEnumerable<UnAssignedConsignment> GetUnAssignedConsignments(User oUser)
        {
            return _consignmentManagementRepository.GetUnAssignedConsignments(oUser);
        }

        public AssignedConsignment GetConsignmentByDriverKey(Guid oDriverKey)
        {
            if (oDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            return _consignmentManagementRepository.GetConsignmentByDriverKey(oDriverKey);
        }

        public IEnumerable<CompletedConsignment> GetCompletedConsignments(User oUser)
        {
            return _consignmentManagementRepository.GetCompletedConsignments(oUser);
        }


        public IEnumerable<User> GetUsersForConsignmentAssigning(User oUser)
        {
            return _consignmentManagementRepository.GetUsersForConsignmentAssigning(oUser);
        }
    }
}
