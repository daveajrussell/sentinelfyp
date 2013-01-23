using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using GMap.NET;

namespace Sentinel.Controllers
{
    public class MonitorController : Controller
    {
        /*private readonly IPointService _pointService;
        private readonly IGHeatService _gheatService;
        private readonly IGISService _gisService;
        private List<PointLatLng> _points;

        public MonitorController(IPointService pointService, IGHeatService gheatService, IGISService gisService)
        {
            if (pointService == null)
                throw new ArgumentNullException("point service");

            _pointService = pointService;

            if (gheatService == null)
                throw new ArgumentNullException("gheat service");

            _gheatService = gheatService;

            if (gisService == null)
                throw new ArgumentNullException("gis service");

            _gisService = gisService;

            if (_points == null)
                _points = _pointService.LoadPoints();
        }

        public ActionResult Index()
        {
            var gis = _gisService.GetGIS();
            return View(gis);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetUpdate()
        {
            var gis = _gisService.GetGIS();
            return PartialView("MapPartial", gis);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetRoute()
        {
            var data = _gisService.GetAllGISData();
            return PartialView("PolylineMapPartial", data);
        }*/
    }
}
