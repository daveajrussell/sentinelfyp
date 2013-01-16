using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.SecurityModels;
using Sentinel.Helpers.ExtensionMethods;
using System.Web.Security;
using System.Security.Principal;
using DomainModel.Models.AuditModels;

namespace Sentinel.Controllers
{
    public class HeaderController : Controller
    {
        public ActionResult Index()
        {
            var user = State.User;

            if (user == null)
                return RedirectToAction("Index", "Account");
            else
                return PartialView("HeaderPartial", State.User.ToViewModel());
        }
    }
}
