using DomainModel.Interfaces.Services;
using DomainModel.Models.AssetModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sentinel.Controllers;
using Sentinel.Helpers.ExtensionMethods;
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
                .Returns(() => new List<AssignedConsignment>() { new AssignedConsignment(Guid.NewGuid(), Guid.NewGuid(), "", "", "", DateTime.Now) });

            _consignmentService.Setup(m => m.GetUnAssignedConsignments())
                .Returns(() => new List<UnAssignedConsignment>() { new UnAssignedConsignment(Guid.NewGuid(), DateTime.Now), new UnAssignedConsignment(Guid.NewGuid(), DateTime.Now) });

            _itemService.Setup(m => m.GetConsignmentDeliveryItems(It.IsAny<Guid>()))
                .Returns<Guid>((consignmentKey) => new List<AssignedDeliveryItem>() { new AssignedDeliveryItem(consignmentKey, Guid.NewGuid(), Guid.NewGuid()), new AssignedDeliveryItem(consignmentKey, Guid.NewGuid(), Guid.NewGuid()) });

            _itemService.Setup(m => m.GetDeliveryItemsByKey(It.IsAny<IEnumerable<Guid>>()))
                .Returns(() => new List<AssignedDeliveryItem>() { new AssignedDeliveryItem(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()), new AssignedDeliveryItem(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()) });
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
        public void TestAssignedConsignments()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);
            target.ControllerContext = _mock.Object;

            var result = target.AssignedConsignments();
            var item = (new List<AssignedConsignment>(((List<AssignedConsignment>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("ConsignmentManagement/Assigned", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<AssignedConsignment>(item);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestAssignedConsignmentsPageActions()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.AssignedConsignmentsPageActions();
            var item = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("../ActionButtons/PageActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Xunit.Assert.NotNull(item);
            Xunit.Assert.IsAssignableFrom<ActionButtonsViewModel>(item);
            Xunit.Assert.Equal("Back", item.Display);
            Xunit.Assert.Equal("navigateBack('Index')", item.Javascript);
        }

        [Fact]
        [TestMethod]
        public void TestUnAssignedConsignments()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);
            target.ControllerContext = _mock.Object;

            var result = target.UnAssignedConsignments();
            var item = (new List<UnAssignedConsignment>(((List<UnAssignedConsignment>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("ConsignmentManagement/UnAssigned", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<UnAssignedConsignment>(item);
            Xunit.Assert.IsNotType<AssignedConsignment>(item);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestUnAssignedConsignmentsPageActions()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.UnAssignedConsignmentsPageActions();
            var itemOne = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];
            var itemTwo = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[1];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("../ActionButtons/PageActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Xunit.Assert.NotNull(itemOne);
            Xunit.Assert.IsAssignableFrom<ActionButtonsViewModel>(itemOne);
            Xunit.Assert.Equal("Back", itemOne.Display);
            Xunit.Assert.Equal("navigateBack('Index')", itemOne.Javascript);

            Xunit.Assert.NotNull(itemTwo);
            Xunit.Assert.IsAssignableFrom<ActionButtonsViewModel>(itemTwo);
            Xunit.Assert.Equal("Assign Selected Consignments", itemTwo.Display);
        }

        [Fact]
        [TestMethod]
        public void TestDeliveryItemManagement()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.DeliveryItemManagement();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        [TestMethod]
        public void TestDriverManagement()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.DriverManagement();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        [TestMethod]
        public void TestGetConsignmentDeliveryItem()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);
            target.ControllerContext = _mock.Object;

            var result = target.GetConsignmentDeliveryItem(Guid.NewGuid().ToString());
            var item = (new List<AssignedDeliveryItem>(((List<AssignedDeliveryItem>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("DeliveryItemGridPartial", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<AssignedDeliveryItem>(item);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestDeliveryItemsGridActions()
        {
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.DeliveryItemsGridActions();
            var itemOne = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];
            var itemTwo = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[1];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("../ActionButtons/GridActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Xunit.Assert.NotNull(itemOne);
            Xunit.Assert.IsAssignableFrom<ActionButtonsViewModel>(itemOne);
            Xunit.Assert.Equal("btnUnassignItems", itemOne.ID);
            Xunit.Assert.Equal("Unassign Selected Items", itemOne.Display);

            Xunit.Assert.NotNull(itemTwo);
            Xunit.Assert.IsAssignableFrom<ActionButtonsViewModel>(itemTwo);
            Xunit.Assert.Equal("btnAssignItems", itemTwo.ID);
            Xunit.Assert.Equal("Assign New Items", itemTwo.Display);
        }

        [Fact]
        [TestMethod]
        public void TestGetAssignDeliveryItemPartialNoItems()
        {
            _itemService.Setup(m => m.GetAllUnassignedDeliveryItems())
                .Returns(() => new List<DeliveryItem>());

            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.GetAssignDeliveryItemPartial(Guid.NewGuid().ToString());

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("ErrorDialogPartial", ((ViewResultBase)(result)).ViewName);
        }

        [Fact]
        [TestMethod]
        public void TestGetAssignDeliveryItemPartialWithItems()
        {
            _itemService.Setup(m => m.GetAllUnassignedDeliveryItems())
                .Returns(() => new List<DeliveryItem>() { new DeliveryItem(Guid.NewGuid(), Guid.NewGuid()), new DeliveryItem(Guid.NewGuid(), Guid.NewGuid()) });

            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.GetAssignDeliveryItemPartial(Guid.NewGuid().ToString());
            var model = ((ViewResultBase)(result)).Model;

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("AssignDeliveryItemPartial", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<IEnumerable<AssignDeliveryItemViewModel>>(model);
        }

        [Fact]
        [TestMethod]
        public void TestGetUnAssignDeliveryItemPartial()
        {
            var guidsString = string.Format("{0}, {1}, {2}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.GetUnAssignDeliveryItemPartial(guidsString);
            var model = ((ViewResultBase)(result)).Model;

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PartialViewResult>(result);
            Xunit.Assert.Equal("UnAssignDeliveryItemPartial", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<IEnumerable<AssignedDeliveryItem>>(model);
        }

        [Fact]
        [TestMethod]
        public void TestAssignDeliveryItems()
        {
            var guidsString = string.Format("{0}, {1}, {2}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var consignmentKey = Guid.NewGuid().ToString();
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.AssignDeliveryItems(consignmentKey, guidsString);
            var item = (new List<AssignedDeliveryItem>(((List<AssignedDeliveryItem>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("DeliveryItemGridPartial", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<AssignedDeliveryItem>(item);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestUnAssignDeliveryItems()
        {
            var guidsString = string.Format("{0}, {1}, {2}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var consignmentKey = Guid.NewGuid().ToString();
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.UnAssignDeliveryItems(consignmentKey, guidsString);
            var item = (new List<AssignedDeliveryItem>(((List<AssignedDeliveryItem>)(((ViewResultBase)(result)).Model))))[0];

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("DeliveryItemGridPartial", ((ViewResultBase)(result)).ViewName);
            Xunit.Assert.IsAssignableFrom<AssignedDeliveryItem>(item);
            Xunit.Assert.True(((ViewResultBase)(result)).ViewBag.GridRecordCount > 0);
        }

        [Fact]
        [TestMethod]
        public void TestPrintDeliveryItemLabel()
        {
            var guidsString = string.Format("{0}, {1}, {2}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var target = new AssetManagementController(_consignmentService.Object, _itemService.Object);

            var result = target.PrintDeliveryItemLabel(guidsString);

            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsAssignableFrom<PDFResult>(result);
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
