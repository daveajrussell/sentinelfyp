using DomainModel.Models.AuditModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sentinel.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(Exception exception)
        {
            if (null == State.Session)
                return RedirectToAction("Login", "Account");
            else
                return View(exception);
        }
    }
}
