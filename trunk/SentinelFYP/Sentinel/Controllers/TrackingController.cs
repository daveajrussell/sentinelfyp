using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AuditModels;
using Sentinel.Models;

namespace Sentinel.Controllers
{
    public class TrackingController : Controller
    {
        private IHistoricalTrackingService _service;

        public TrackingController(IHistoricalTrackingService service)
        {
            if (service == null)
                throw new ArgumentNullException("tracking service");

            _service = service;
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
            var data = _service.GetAllHistoricalTrackingDataByDriverKey(oDriverKey);

            return PartialView("AllDriverHistoricalTrackingPartial", data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetFilteredHistoricalData(string strDriverKey, string strDateRange)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            var oDateRange = DateTime.Parse(strDateRange);

            var data = _service.GetFilteredHistoricalDataByDriverKey(oDriverKey, oDateRange);

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
            var data = _service.GetDrivers();
            return PartialView("Dialogs/DriverSelectDialog", data);
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
