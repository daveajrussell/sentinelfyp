using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using DomainModel.Models.GISModels;
using DomainModel.SecurityModels;
using Moq;
using Sentinel.Controllers;
using Sentinel.Models;
using Xunit;

namespace Sentinel.Tests.Controllers
{
    public class TrackingControllerTest
    {
        private Mock<IHistoricalTrackingService> _histroicalTrackingService;
        private Mock<ILiveTrackingService> _liveTrackingService;
        private List<MenuViewModel> _menuItems;

        public TrackingControllerTest()
        {
            _menuItems = new List<MenuViewModel>()
            {
                new MenuViewModel()
                {
                    Display = "Historical Tracking",
                    URL = "~/Tracking/HistoricalTracking",
                    Description = "View Historical Tracking Data"
                },
                new MenuViewModel()
                {
                    Display = "Live Tracking",
                    URL = "~/Tracking/LiveTracking",
                    Description = "View Live Tracking Data"
                }
            };

            _histroicalTrackingService = new Mock<IHistoricalTrackingService>();

            _histroicalTrackingService.Setup(m => m.GetAllHistoricalTrackingDataByDriverKey(It.IsAny<Guid>()))
                .Returns(() => new List<HistoricalGeospatialInformation>() { new HistoricalGeospatialInformation(), new HistoricalGeospatialInformation() });

            _histroicalTrackingService.Setup(m => m.GetFilteredHistoricalDataByDriverKey(It.IsAny<Guid>(), It.IsAny<int>()))
                .Returns<Guid, int>((driverKey, sessionID) => new HistoricalGeospatialInformation() { DriverKey = driverKey, HistoricalSessionID = sessionID });

            _histroicalTrackingService.Setup(m => m.GetDrivers())
                .Returns(() => new List<User>() { new User(), new User() });

            _liveTrackingService = new Mock<ILiveTrackingService>();

            _liveTrackingService.Setup(m => m.GetLiveDrivers())
                .Returns(() => new List<User>() { new User(), new User() });

            _liveTrackingService.Setup(m => m.GetLiveUpdate(It.IsAny<Guid>()))
                .Returns<Guid>((userKey) => new GeospatialInformation());

            _liveTrackingService.Setup(m => m.GetLiveElapsedRoute(It.IsAny<Guid>()))
                .Returns<Guid>((userKey) => new List<GeospatialInformation>() { new GeospatialInformation(), new GeospatialInformation() });
        }

        [Fact]
        public void TestConstructor()
        {
            TrackingController target = null;

            Assert.DoesNotThrow(() => target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object));
            Assert.NotNull(target);
        }

        [Fact]
        public void TestConstructorShouldThrow()
        {
            TrackingController target = null;

            Assert.Throws<ArgumentNullException>(() => target = new TrackingController(null, null));
            Assert.Null(target);
        }

        [Fact]
        public void TestIndex()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.Index();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
            
            var itemOne = (new List<MenuViewModel>(((List<MenuViewModel>)(((ViewResultBase)(result)).Model))))[1];
            var itemTwo = (new List<MenuViewModel>(((List<MenuViewModel>)(((ViewResultBase)(result)).Model))))[2];

            Assert.NotNull(itemOne);
            Assert.IsAssignableFrom<MenuViewModel>(itemOne);
            Assert.Equal("Historical Tracking", itemOne.Display);
            Assert.Equal("~/Tracking/HistoricalTracking", itemOne.URL);
            Assert.Equal("View Historical Tracking Data", itemOne.Description);

            Assert.NotNull(itemTwo);
            Assert.IsAssignableFrom<MenuViewModel>(itemTwo);
            Assert.Equal("Live Tracking", itemTwo.Display);
            Assert.Equal("~/Tracking/LiveTracking", itemTwo.URL);
            Assert.Equal("View Live Tracking Data", itemTwo.Description);
        }

        [Fact]
        public void TestHistoricalTracking()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.HistoricalTracking();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void TestGetAllHistoricalDataByDriverKey()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);
            var driverKey = "66FBA0E1-6429-4999-9538-6566DEE70048";

            var result = target.GetAllHistoricalDataByDriverKey(driverKey);
            var itemOne = (new List<HistoricalGeospatialInformation>(((List<HistoricalGeospatialInformation>)(((ViewResultBase)(result)).Model))))[0];

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("AllDriverHistoricalTrackingPartial", ((ViewResultBase)(result)).ViewName);
            Assert.NotNull(itemOne);
            Assert.IsAssignableFrom<HistoricalGeospatialInformation>(itemOne);
            
        }

        [Fact]
        public void TestGetFilteredHistoricalData()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);
            var driverKey = "66FBA0E1-6429-4999-9538-6566DEE70048";
            var sessionID = new Random().Next();

            var result = target.GetFilteredHistoricalData(driverKey, sessionID);
            var itemOne = ((ViewResultBase)(result)).Model;

            Assert.NotNull(result);
            Assert.NotNull(itemOne);

            Assert.Equal("FilteredDriverHistoricalTrackingPartial", ((ViewResultBase)(result)).ViewName);
            Assert.IsAssignableFrom<HistoricalGeospatialInformation>(itemOne);
        }

        [Fact]
        public void TestFilteredHistoricalPageDataPageActions()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.FilteredHistoricalPageDataPageActions();
            var item = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];

            Assert.NotNull(result);
            Assert.NotNull(item);

            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("../ActionButtons/PageActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Assert.IsAssignableFrom<ActionButtonsViewModel>(item);
            Assert.Equal("Back", item.Display);
            Assert.Equal("javascript:showAllHistoricalData()", item.Javascript);
        }

        [Fact]
        public void TestAllHistoricalPageDataPageActions()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.AllHistoricalPageDataPageActions();
            var itemOne = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];
            var itemTwo = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[1];

            Assert.NotNull(result);
            Assert.NotNull(itemOne);
            Assert.NotNull(itemTwo);

            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("../ActionButtons/PageActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Assert.IsAssignableFrom<ActionButtonsViewModel>(itemOne);
            Assert.Equal("Back", itemOne.Display);
            Assert.Equal("navigateBack('Index')", itemOne.Javascript);

            Assert.IsAssignableFrom<ActionButtonsViewModel>(itemTwo);
            Assert.Equal("Select New Driver", itemTwo.Display);
            Assert.Equal("selectDriver()", itemTwo.Javascript);
        }

        [Fact]
        public void TestGetAllDriversForDriverSelect()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.GetAllDriversForDriverSelect();
            var item = (new List<User>(((List<User>)(((ViewResultBase)(result)).Model))))[0];

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("Dialogs/DriverSelectDialog", ((ViewResultBase)(result)).ViewName);

            Assert.NotNull(item);
            Assert.IsAssignableFrom<User>(item);
        }

        [Fact]
        public void TestLiveTracking()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.LiveTracking();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void TestGetAllDriversForLiveTracking()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.GetAllDriversForLiveTracking();
            var item = (new List<User>(((List<User>)(((ViewResultBase)(result)).Model))))[0];

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("Dialogs/LiveDriverSelectDialog", ((ViewResultBase)(result)).ViewName);

            Assert.NotNull(item);
            Assert.IsAssignableFrom<User>(item);
        }

        [Fact]
        public void TestLiveElapsedTrackingDriverFeedPageActions()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);

            var result = target.LiveElapsedTrackingDriverFeedPageActions();
            var itemOne = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[0];
            var itemTwo = (new List<ActionButtonsViewModel>(((List<ActionButtonsViewModel>)(((ViewResultBase)(result)).Model))))[1];

            Assert.NotNull(result);
            Assert.NotNull(itemOne);
            Assert.NotNull(itemTwo);

            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("../ActionButtons/PageActionButtonsPartial", ((ViewResultBase)(result)).ViewName);

            Assert.IsAssignableFrom<ActionButtonsViewModel>(itemOne);
            Assert.Equal("Back", itemOne.Display);
            Assert.Equal("navigateBack('Index')", itemOne.Javascript);

            Assert.IsAssignableFrom<ActionButtonsViewModel>(itemTwo);
            Assert.Equal("Show Single Marker", itemTwo.Display);
            Assert.Equal("showSingleMarker()", itemTwo.Javascript);
        }

        [Fact]
        public void TestGetLiveElapsedRoute()
        {
            var target = new TrackingController(_histroicalTrackingService.Object, _liveTrackingService.Object);
            var driverKey = "66FBA0E1-6429-4999-9538-6566DEE70048";

            var result = target.GetLiveElapsedRoute(driverKey);
            var item = ((ViewResultBase)(result)).Model;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PartialViewResult>(result);
            Assert.Equal("LiveElapsedTrackingDriverFeed", ((ViewResultBase)(result)).ViewName);

            Assert.NotNull(item);
            Assert.IsAssignableFrom<IEnumerable<GeospatialInformation>>(item);
        }
    }
}
