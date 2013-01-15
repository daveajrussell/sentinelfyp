using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sentinel.Helpers;
using Sentinel.Helpers.ExtensionMethods;

namespace Sentinel.Controllers
{
    public class DeliveryItemController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public QRCodeResult Test()
        {
            string strQRString = "http://www.daveajrussell.com";
            return new QRCodeResult(strQRString);
        }

        public ActionResult TestTwo()
        {
            string strQRString = "http://www.daveajrussell.com";
            var result = new QRCodeResult(strQRString);
            return PartialView("QRPartial", result);
        }
    }
}
