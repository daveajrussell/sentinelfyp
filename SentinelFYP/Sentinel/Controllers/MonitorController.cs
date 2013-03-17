using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Interfaces.Services;
using GMap.NET;

namespace Sentinel.Controllers
{
    [Authorize(Roles = "AUDITOR")]
    public class MonitorController : Controller
    {
        private readonly ISecurityService _securityService;

        public MonitorController(ISecurityService securityService)
        {
            if (null == securityService)
                throw new ArgumentNullException();

            _securityService = securityService;
        }

        public ActionResult GetDriverContactDetailsPartial(string strDriverKey)
        {
            Guid oDriverKey = new Guid(strDriverKey);
            var data = _securityService.GetUserByUserKey(oDriverKey);
            var vehicle = _securityService.GetUserVehicle(oDriverKey);

            data.UserAssignedVehicle = vehicle;

            return PartialView("UserContactDetailsPartial", data);
        }
    }
}
