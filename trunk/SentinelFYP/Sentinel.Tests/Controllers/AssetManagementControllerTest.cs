using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sentinel.Controllers;
using Sentinel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Xunit;

namespace Sentinel.Tests.Controllers
{

    [TestClass]
    public class AssetManagementControllerTest
    {
        private Mock<IConsignmentManagementService> _consignmentService;
        private Mock<IDeliveryItemManagementService> _itemService;

        private Mock<ControllerContext> _mock;
        private Mock<HttpSessionStateBase> _mockSession;

        public AssetManagementControllerTest()
        {
            _mock = new Mock<ControllerContext>();
            _mockSession = new Mock<HttpSessionStateBase>();

            _mock.Setup(p => p.HttpContext.Session)
                .Returns(_mockSession.Object);

            HttpContext.Current = FakeHttpContext();

            _consignmentService = new Mock<IConsignmentManagementService>();
            _itemService = new Mock<IDeliveryItemManagementService>();

            _consignmentService.Setup(m => m.GetAssignedConsignments())
                .Returns(() => new List<AssignedConsignment>() { new AssignedConsignment(Guid.NewGuid(), Guid.NewGuid(), "", "", 0, DateTime.Now) });
        }

        [Fact]
        [TestMethod]
        public void TestConstructor()
        {
            AssetManagementController target = null;

            Xunit.Assert.DoesNotThrow(() => target = new AssetManagementController(_consignmentService.Object, _itemService.Object));
            Xunit.Assert.NotNull(target);
            Xunit.Assert.IsAssignableFrom<AssetManagementController>(target);
        }

        [Fact]
        [TestMethod]
        public void TestConstructorShouldThrow()
        {
            AssetManagementController target = null;

            Xunit.Assert.Throws<ArgumentNullException>(() => target = new AssetManagementController(null, null));
            Xunit.Assert.Null(target);
        }

        [Fact]
        [TestMethod]
        public void TestIndex()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.Index();
            var item = (new List<MenuViewModel>(((List<MenuViewModel>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<ViewResult>(result);

            Xunit.Assert.NotNull(item);
            Xunit.Assert.IsAssignableFrom<MenuViewModel>(item);
            Xunit.Assert.Equal("Consignment Management", item.Display);
            Xunit.Assert.Equal("~/AssetManagement/ConsignmentManagement", item.URL);
            Xunit.Assert.Equal("Do Consignment Stuff", item.Description);
        }

        [Fact]
        [TestMethod]
        public void TestConsignmentManagement()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.ConsignmentManagement();
            var item = (new List<MenuViewModel>(((List<MenuViewModel>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<ViewResult>(result);

            Xunit.Assert.NotNull(item);
            Xunit.Assert.IsAssignableFrom<MenuViewModel>(item);
            Xunit.Assert.Equal("AssignedConsignments", item.ID);
            Xunit.Assert.Equal("Assigned Consignments", item.Display);
            Xunit.Assert.Equal("Display all consignments that have been assigned to a driver", item.Description);
        }

        [Fact]
        [TestMethod]
        public void TestAssignedConsignments()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);
            target.ControllerContext = _mock.Object;

            var result = target.AssignedConsignments();
            var item = (new List<AssignedConsignment>(((List<AssignedConsignment>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("ConsignmentManagement/Assigned", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestAssignedConsignmentsPageActions()
        {
        }

        [Fact]
        [TestMethod]
        public void TestUnAssignedConsignments()
        {
        }

        [Fact]
        [TestMethod]
        public void TestUnAssignedConsignmentsPageActions()
        {
        }

        [Fact]
        [TestMethod]
        public void TestDeliveryItemManagement()
        {

        }

        [Fact]
        [TestMethod]
        public void TestDriverManagement()
        {

        }

        [Fact]
        [TestMethod]
        public void TestGetConsignmentDeliveryItem()
        {

        }

        [Fact]
        [TestMethod]
        public void TestDeliveryItemsGridActions()
        {

        }

        [Fact]
        [TestMethod]
        public void TestGetAssignDeliveryItemPartial()
        {

        }

        [Fact]
        [TestMethod]
        public void TestGetUnAssignDeliveryItemPartial()
        {

        }

        [Fact]
        [TestMethod]
        public void TestAssignDeliveryItems()
        {

        }

        [Fact]
        [TestMethod]
        public void TestUnAssignDeliveryItems()
        {

        }

        [Fact]
        [TestMethod]
        public void TestGetDeliveryItems()
        {

        }

        [Fact]
        [TestMethod]
        public void TestPrintDeliveryItemLabel()
        {

        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/Sentinel/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard, new[] { typeof(HttpSessionStateContainer) }, null)
                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }
}
