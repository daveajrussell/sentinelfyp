using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using DomainModel.Interfaces.Services;
using Sentinel.Grid;
using Sentinel.Helpers;
using Sentinel.Helpers.ExtensionMethods;

namespace Sentinel.Controllers
{
    public class AssetManagementController : Controller
    {
        private IConsignmentManagementService _consignmentService;
        private IDeliveryItemManagementService _itemService;

        public AssetManagementController(IConsignmentManagementService consignmentService, IDeliveryItemManagementService itemService)
        {
            if (consignmentService == null)
                throw new ArgumentNullException("Consignment Service");

            _consignmentService = consignmentService;

            if (itemService == null)
                throw new ArgumentNullException("Item Service");

            _itemService = itemService;
        }

        public ActionResult Management()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetAllConsignments()
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _consignmentService.GetAllConsignments();

            ViewBag.GridRecordCount = data.Count();

            return PartialView("ConsignmentsPartial", data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetConsignmentDeliveryItem(string strConsignmentKey)
        {
            var gridParameters = GridParameters.GetGridParameters();
            
            var oConsignmentKey = new Guid(strConsignmentKey);

            var data = _itemService.GetConsignmentDeliveryItems(oConsignmentKey);

            ViewBag.GridRecordCount = data.Count();

            return PartialView("DeliveryItemGridPartial", data);
        }
    }
}
