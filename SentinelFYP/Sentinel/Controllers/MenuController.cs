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
                Display = "Heatmap",
                URL = "~/Heatmap"
            },
            new MenuViewModel() {
                Display = "Real-time Monitor",
                URL = "~/Monitor"
            },
            new MenuViewModel() {
                Display = "Delivery Items",
                URL = "~/DeliveryItem"
            }
        };

        public ActionResult Index()
        {
            return PartialView("MenuPartial", items);
        }
    }
}
