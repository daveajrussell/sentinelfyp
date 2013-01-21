using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using DomainModel.Models.AuditModels;

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

        public ActionResult Index()
        {
            var data = _service.GetAllHistoricalTrackingDataByDriverKey(State.User.UserKey);
            return View(data);
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

        public ActionResult HistoricalTracking()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetDriverTrackingHistoricalData(string strDriverKey)
        {
            var oDriverKey = GetKeyFromString(strDriverKey);
            return View();
        }

        private Guid GetKeyFromString(string strKey)
        {
            return new Guid(strKey);
        }*/
    }
}
