using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Models.AssetModels;

namespace DomainModel.Test.TestHelpers
{
    public static class ConsignmentManagementTestHelper
    {
        public static Consignment CreateConsignmentMock(DateTime dtConsignmentDeliveryDate)
        {
            return CreateConsignment();
        }

        public static Consignment AssignConsignmentToDriverMock(Guid oConsignmentKey, Guid oDriverKey)
        {
            var consignment = CreateConsignment();
            consignment.AssignedDriverKey = oDriverKey;

            return consignment;
        }

        public static Consignment ReAssignConsignmentMock(Guid oConsignmentKey, Guid oPreviousDriverKey, Guid oReAssignedDriverKey)
        {
            return new Consignment()
            {
               ConsignmentKey = oConsignmentKey,
               AssignedDriverKey = oReAssignedDriverKey,
               ConsignmentDeliveryDate = DateTime.Today
            };
        }

        public static Consignment UnAssignConsignmentMock(Guid oConsignmentKey, Guid oAssignedDriverKey)
        {
            return new Consignment()
            {
                ConsignmentKey = oConsignmentKey,
                AssignedDriverKey = Guid.Empty,
                ConsignmentDeliveryDate = DateTime.Today
            };
        }

        private static Consignment CreateConsignment()
        {
            var _Key = Guid.NewGuid();
            var _DeliveryDate = DateTime.Today;

            return new Consignment(_Key, _DeliveryDate);
        }
    }
}
