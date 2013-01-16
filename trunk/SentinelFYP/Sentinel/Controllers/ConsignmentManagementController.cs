using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using Sentinel.Helpers;
using Sentinel.Helpers.ExtensionMethods;

namespace Sentinel.Controllers
{
    public class ConsignmentManagementController : Controller
    {
        public ActionResult UnassignedDeliveryItems()
        {
            return View();
        }

        public ActionResult Consignments()
        {
            return View();
        }
    }
}
