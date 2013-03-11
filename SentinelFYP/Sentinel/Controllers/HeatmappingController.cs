using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using GMap.NET;
using Sentinel.Helpers.ExtensionMethods;
using Sentinel.Models;

namespace Sentinel.Controllers
{
    [Authorize(Roles = "AUDITOR")]
    public class HeatmappingController : Controller
    {
        private readonly IPointService _pointService;
        private readonly IGHeatService _gheatService;
        private readonly ILiveTrackingService _trackingService;
        private List<PointLatLng> _signalBlackspotPoints;
        private List<PointLatLng> _activityPoints;

        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                Display = "Activity Heatmap",
                URL = "~/Heatmapping/ActivityMap",
                Description = "View a heatmap of your fleet's activity.",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Signal Heatmap",
                URL = "~/Heatmapping/SignalMap",
                Description = "View a heatmap of signal blackspots.",
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

        public HeatmappingController(IPointService pointService, IGHeatService gheatService, ILiveTrackingService trackingService)
        {
            if (pointService == null)
                throw new ArgumentNullException("Point Service");

            _pointService = pointService;

            if (gheatService == null)
                throw new ArgumentNullException("GHeat Service");

            _gheatService = gheatService;

            if (trackingService == null)
                throw new ArgumentNullException("Tracking Service");

            _trackingService = trackingService;

            _activityPoints = _pointService.LoadActivityPoints();

            _signalBlackspotPoints = _pointService.LoadSignalBlackspotPoints();
        }

        public ActionResult ActivityMonitoringPageActions()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", menuItems);
        }

        public ActionResult HeatmapPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", pageItems);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActivityMap()
        {   
            return View();
        }

        public ActionResult SignalMap()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LiveActivityHeatmapData()
        {
            var data = _trackingService.GetAllLiveElapsedRoutes();

            if(data.Count() > 0)
                return PartialView("ActivityHeatmapPartial", data);
            else
                return PartialView("ErrorDialogPartial", "No activity heatmap data is available.");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LiveSignalHeatmapData()
        {
            var data = _trackingService.GetAllLiveElapsedRoutes();

            if(data.Count() > 0)
                return PartialView("SignalHeatmapPartial", data);
            else
                return PartialView("ErrorDialogPartial", "No signal heatmap data is available.");
        }

        public TileResult ActivityTile(string zoom, string x, string y)
        {
            using (var tile = _gheatService.GetTile(_activityPoints, "classic", int.Parse(zoom), int.Parse(x), int.Parse(y)))
            {
                return new TileResult(tile);
            }
        }

        public TileResult BlackSpotTile(string zoom, string x, string y)
        {
            using (var tile = _gheatService.GetTile(_signalBlackspotPoints, "pgaitch", int.Parse(zoom), int.Parse(x), int.Parse(y)))
            {
                return new TileResult(tile);
            }
        }
    }
}
