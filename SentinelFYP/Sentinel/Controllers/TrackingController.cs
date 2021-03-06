﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using Sentinel.Models;

namespace Sentinel.Controllers
{
    [Authorize(Roles = "AUDITOR")]
    public class TrackingController : Controller
    {
        private IHistoricalTrackingService _histroicalTrackingService;
        private ILiveTrackingService _liveTrackingService;

        public TrackingController(IHistoricalTrackingService historicalTrackingService, ILiveTrackingService liveTrackingService)
        {
            if (null == historicalTrackingService)
                throw new ArgumentNullException("historical tracking service");

            _histroicalTrackingService = historicalTrackingService;

            if (null == liveTrackingService)
                throw new ArgumentNullException("live tracking service");

            _liveTrackingService = liveTrackingService;
        }

        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                Display = "All Active Drivers",
                URL = "~/Tracking/AllDriverLiveTracking",
                Description = "View All Live Active Drivers",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Historical Tracking",
                URL = "~/Tracking/HistoricalTracking",
                Description = "View Historical Tracking Data",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Live Tracking",
                URL = "~/Tracking/LiveTracking",
                Description = "View Live Tracking Data",
                Permission = "AUDITOR"
            }
        };

        List<ActionButtonsViewModel> pageItems = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "navigateBack('Index')"
            }
        };

        public ActionResult TrackingPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", pageItems);
        }

        public ActionResult Index()
        {
            return View(menuItems);
        }

        public ActionResult AllDriverLiveTracking()
        {
            var data = _liveTrackingService.GetAllLiveElapsedRoutes(State.User);
            return View(data);
        }

        public ActionResult HistoricalTracking()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetAllHistoricalDataByDriverKey(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var data = _histroicalTrackingService.GetAllHistoricalTrackingDataByDriverKey(oDriverKey);

            if (data.Count() > 0)
                return PartialView("AllDriverHistoricalTrackingPartial", data);
            else
                return PartialView("ErrorDialogPartial", "No historical data is available for this user.");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetFilteredHistoricalData(string strDriverKey, int iSessionID)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            
            var data = _histroicalTrackingService.GetFilteredHistoricalDataByDriverKey(oDriverKey, iSessionID);

            if (null != data)
                return PartialView("FilteredDriverHistoricalTrackingPartial", data);
            else
                return PartialView("ErrorDialogPartial", "No historical data is available for this session.");
        }

        public ActionResult FilteredHistoricalPageDataPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "showAllHistoricalData()"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", actions);
        }

        public ActionResult AllHistoricalPageDataPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "navigateBack('HistoricalTracking')"
                },
                new ActionButtonsViewModel()
                {
                    Display = "Select New Driver",
                    Javascript = "selectDriver()"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", actions);
        }

        public ActionResult GetAllDriversForDriverSelect()
        {
            var data = _histroicalTrackingService.GetDrivers(State.User);
            return PartialView("Dialogs/DriverSelectDialog", data);
        }

        public ActionResult LiveTracking()
        {
            return View();
        }

        public ActionResult GetAllDriversForLiveTracking()
        {
            var data = _liveTrackingService.GetLiveDrivers(State.User);
            return PartialView("Dialogs/LiveDriverSelectDialog", data);
        }

        public ActionResult LiveElapsedTrackingDriverFeedPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "navigateBack('LiveTracking')"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", actions);
        }

        public ActionResult GetLiveElapsedRoute(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var data = _liveTrackingService.GetLiveElapsedRoute(oDriverKey);

            if (data.Count() > 0)
                return PartialView("LiveElapsedTrackingDriverFeed", data);
            else
                return PartialView("ErrorDialogPartial", "No elapsed live data is available for this driver.");
        }

        public ActionResult GetLiveElapsedRouteUpdate(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var data = _liveTrackingService.GetLiveElapsedRoute(oDriverKey);

            if (data.Count() > 0)
                return PartialView("LiveElapsedMapPartial", data);
            else
                return PartialView("ErrorDialogPartial", "No elapsed live data is available for this driver.");
        }

        private Guid GetKeyFromString(string strKey)
        {
            return new Guid(strKey);
        }
    }
}
