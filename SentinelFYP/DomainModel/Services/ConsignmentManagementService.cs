﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Interfaces.Repositories;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;

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

        public Consignment CreateConsignment(DateTime dtConsignmentDeliveryDate)
        {
            return _consignmentManagementRepository.CreateConsignment(dtConsignmentDeliveryDate);
        }

        public Consignment AssignConsignmentToDriver(Guid oConsignmentKey, Guid oDriverKey)
        {
            if (oConsignmentKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Consignment Key");

            if (oDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            return _consignmentManagementRepository.AssignConsignmentToDriver(oConsignmentKey, oDriverKey);
        }

        public Consignment ReAssignConsignment(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey)
        {
            if (oConsignmentKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Consignment Key");

            if (oPreviousDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            if (oReAssignedDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            return _consignmentManagementRepository.ReAssignConsignment(oConsignmentKey, oPreviousDriverKey, oReAssignedDriverKey);
        }

        public void UnAssignConsignment(Guid oConsignmentKey, Guid oAssignedDriverKey)
        {
            if (oConsignmentKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Consignment Key");

            if (oAssignedDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            _consignmentManagementRepository.UnAssignConsignment(oConsignmentKey, oAssignedDriverKey);
        }

        public IEnumerable<Consignment> GetAllConsignments()
        {
            return _consignmentManagementRepository.GetAllConsignments();
        }

        public IEnumerable<Consignment> GetAssignedConsignments()
        {
            return _consignmentManagementRepository.GetAssignedConsignments();
        }

        public IEnumerable<Consignment> GetUnAssignedConsignments()
        {
            return _consignmentManagementRepository.GetUnAssignedConsignments();
        }

        public Consignment GetConsignmentByDriverKey(Guid oDriverKey)
        {
            if (oDriverKey == Guid.Empty)
                throw new ArgumentOutOfRangeException("Driver Key");

            return _consignmentManagementRepository.GetConsignmentByDriverKey(oDriverKey);
        }

        public IEnumerable<Consignment> GetCompletedConsignments()
        {
            return _consignmentManagementRepository.GetCompletedConsignments();
        }
    }
}
