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
        private List<PointLatLng> _points;

        public HeatmapController(IPointService pointService, IGHeatService gheatService)
        {
            _pointService = pointService;
            _gheatService = gheatService;

            if (_points == null)
                _points = _pointService.LoadPoints();

            _points.Add(new PointLatLng(30.123866, -92.070673));
        }

        public ActionResult Index()
        {
            return View();
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
