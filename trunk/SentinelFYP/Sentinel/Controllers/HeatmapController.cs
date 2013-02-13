using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using GMap.NET;
using Sentinel.Helpers.ExtensionMethods;

namespace Sentinel.Controllers
{
    public class HeatmapController : Controller
    {
        private readonly IPointService _pointService;
        private readonly IGHeatService _gheatService;
        private readonly ILiveTrackingService _trackingService;
        private List<PointLatLng> _points;

        public HeatmapController(IPointService pointService, IGHeatService gheatService, ILiveTrackingService trackingService)
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

            if (_points == null)
                _points = _pointService.LoadPoints();

        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LiveHeatmapData()
        {
            var data = _trackingService.GetAllLiveElapsedRoutes();
            return PartialView("HeatmapPartial", data);
        }

        public TileResult Tile(string colorScheme, string zoom, string x, string y, string rand)
        {
            using (var tile = _gheatService.GetTile(_points, "classic", int.Parse(zoom), int.Parse(x), int.Parse(y)))
            {
                return new TileResult(tile);
            }
        }
    }
}
