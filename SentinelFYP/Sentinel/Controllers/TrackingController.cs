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

        public ActionResult Index()
        {
            return View(menuItems);
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

            return PartialView("AllDriverHistoricalTrackingPartial", data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetFilteredHistoricalData(string strDriverKey, int iSessionID)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            
            var data = _histroicalTrackingService.GetFilteredHistoricalDataByDriverKey(oDriverKey, iSessionID);

            return PartialView("FilteredDriverHistoricalTrackingPartial", data);
        }

        public ActionResult FilteredHistoricalPageDataPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "javascript:showAllHistoricalData()"
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
                    Javascript = "navigateBack('Index')"
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
            var data = _histroicalTrackingService.GetDrivers();
            return PartialView("Dialogs/DriverSelectDialog", data);
        }

        public ActionResult LiveTracking()
        {
            return View();
        }

        public ActionResult GetAllDriversForLiveTracking()
        {
            var data = _liveTrackingService.GetLiveDrivers();
            return PartialView("Dialogs/LiveDriverSelectDialog", data);
        }

        public ActionResult LiveTrackingDriverFeedPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "navigateBack('Index')"
                },
                new ActionButtonsViewModel()
                { 
                    Display = "Show Elapsed Route",
                    Javascript = "showElapsedRoute()"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", actions);
        }

        public ActionResult LiveElapsedTrackingDriverFeedPageActions()
        {
            var actions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "navigateBack('Index')"
                },
                new ActionButtonsViewModel()
                { 
                    Display = "Show Single Marker",
                    Javascript = "showSingleMarker()"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", actions);
        }

        public ActionResult GetLiveUpdateByDriverKey(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var data = _liveTrackingService.GetLiveUpdate(oDriverKey);

            return PartialView("LiveTrackingDriverFeed", data);
        }

        public ActionResult GetLiveElapsedRoute(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var data = _liveTrackingService.GetLiveElapsedRoute(oDriverKey);

            return PartialView("LiveElapsedTrackingDriverFeed", data);
        }

        private Guid GetKeyFromString(string strKey)
        {
            return new Guid(strKey);
        }

        
        /*
        public ActionResult LiveTracking()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LiveTracking(string strDriverKey)
        {
            // get driver live tracking data
            var oDriverKey = GetKeyFromString(strDriverKey);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LiveTrackingLocationUpdate(string strDriverKey)
        {
            // get an updated location for a driver, return a map partial
            var oDriverKey = GetKeyFromString(strDriverKey);
            return View();
        }
        */
    }
}