using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Test.TestHelpers
{
    public static class ConsignmentManagementTestHelper
    {
        public static UnAssignedConsignment CreateConsignmentMock(DateTime dtConsignmentDeliveryDate)
        {
            return CreateConsignment();
        }

        public static AssignedConsignment AssignConsignmentToDriverMock(Guid oConsignmentKey, Guid oDriverKey)
        {
            UnAssignedConsignment oUnAssignedConsignment = CreateConsignment();

            return new AssignedConsignment(oUnAssignedConsignment.ConsignmentKey, oDriverKey, "Test", "User", 0141242123, oUnAssignedConsignment.ConsignmentDateTime);
        }

        public static AssignedConsignment ReAssignConsignmentMock(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey)
        {
            return new AssignedConsignment(oConsignmentKey, oReAssignedDriverKey, "Test", "User", 0141242123, DateTime.Today);
        }

        public static UnAssignedConsignment UnAssignConsignmentMock(Guid oConsignmentKey, Guid oAssignedDriverKey)
        {
            return new UnAssignedConsignment(oConsignmentKey, DateTime.Today);
        }

        private static UnAssignedConsignment CreateConsignment()
        {
            var _ConsignmentKey = Guid.NewGuid();
            var _ConsignmentDateTime = DateTime.Today;

            return new UnAssignedConsignment(_ConsignmentKey, _ConsignmentDateTime);
        }
    }
}
