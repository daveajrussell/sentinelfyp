﻿using System;
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
            return PartialView("UserContactDetailsPartial", data);
        }
    }
}
