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
                URL = "~/Tracking/Index"
            },
            new MenuViewModel() {
                Display = "Asset Management",
                URL = "~/AssetManagement/Index"
            },
            new MenuViewModel() {
                Display = "Heatmap",
                URL = "~/Heatmap/Index"
            },
            new MenuViewModel() {
                Display = "Administration",
                URL = "~/Home/Index"
            }
        };

        public ActionResult Index()
        {
            return PartialView("MenuPartial", items);
        }
    }
}
