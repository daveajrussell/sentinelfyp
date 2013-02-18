using DomainModel.Models.AssetModels;
using DomainModel.SecurityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Sentinel.Helpers.ExtensionMethods;
using Sentinel.Models;

namespace Sentinel.Tests.ExtensionMethodTests
{
    public class ToViewModelTest
    {
        private User _user;
        private List<DeliveryItem> _deliveryItemList;
        private Guid _consignmentKey;

        [Fact]
        public void TestUserToViewModel()
        {
            var date = DateTime.Now;
            _user = new User() { UserName = "Test", UserLastLogon = date };
            var result = _user.ToViewModel();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<UserViewModel>(result);
            Assert.Equal("Test", result.UserName);
            Assert.Equal(date.ToShortDateString(), result.DateLastLoggedIn);
        }

        [Fact]
        public void TestAssignedDeliveryItemToViewModel()
        {
            var consignmentKey = Guid.NewGuid();

            _deliveryItemList = new List<DeliveryItem>()
            {
                new DeliveryItem(Guid.NewGuid(), Guid.NewGuid()),
                new DeliveryItem(Guid.NewGuid(), Guid.NewGuid())
            };

            var result = _deliveryItemList.ToViewModel(consignmentKey);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<AssignDeliveryItemViewModel>>(result);

            foreach (var item in result)
            {
                Assert.NotNull(item);
                Assert.Equal(consignmentKey, item.ConsignmentKey);
            }
        }
    }
}