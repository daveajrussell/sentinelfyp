using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sentinel.Models;

namespace Sentinel.Controllers
{
    public class MenuController : Controller
    {
        List<MenuViewModel> items = new List<MenuViewModel>()
        {
            new MenuViewModel() {
                Display = "Tracking",
                URL = "~/Tracking/Index",
                Permission = "AUDITOR"
            },
            new MenuViewModel() {
                Display = "Asset Management",
                URL = "~/AssetManagement/Index",
                Permission = "AUDITOR"
            },
            new MenuViewModel() {
                Display = "Heatmapping",
                URL = "~/Heatmapping/Index",
                Permission = "AUDITOR"
            },
            new MenuViewModel() {
                Display = "Administration",
                URL = "~/Administration/Index",
                Permission = "ADMINISTRATOR"
            }
        };

        public ActionResult Index()
        {
            return PartialView("MenuPartial", items);
        }
    }
}
