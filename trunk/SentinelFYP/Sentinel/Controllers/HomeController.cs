using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DomainModel.Interfaces.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using DomainModel.Models;
using GMap.NET;
using Sentinel.Helpers.ExtensionMethods;
using System.Drawing.Imaging;
using System.Drawing;
using DomainModel.SecurityModels;
using System.Web.Security;
using Sentinel.Helpers;
using Sentinel.Models;

namespace Sentinel.Controllers
{
    public class HomeController : Controller
    {
        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                Display = "Tracking",
                URL = "~/Tracking/Index",
                Description = "View historical and live tracking data for your fleet.",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Asset Management",
                URL = "~/AssetManagement/Index",
                Description = "Manage your fleet's assets.",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Heatmapping",
                URL = "~/Heatmapping/Index",
                Description = "View activity heatmap data from your fleet.",
                Permission = "AUDITOR"
            }
        };

        public ActionResult HomePageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", menuItems);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
