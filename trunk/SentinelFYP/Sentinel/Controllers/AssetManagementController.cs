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
        public ActionResult GetAssignedConsignments()
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _consignmentService.GetAssignedConsignments();

            ViewBag.GridRecordCount = data.Count();

            return PartialView("AssignedConsignmentsPartial", data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetUnAssignedConsignments()
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _consignmentService.GetUnAssignedConsignments();

            ViewBag.GridRecordCount = data.Count();

            return PartialView("UnAssignedConsignmentsPartial", data);
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetUnAssignDeliveryItemPartial(string strDeliveryItemKey)
        {
            var oDeliveryItemKey = new Guid(strDeliveryItemKey);

            var item = _itemService.GetDeliveryItemByKey(oDeliveryItemKey);
            return PartialView("UnAssignDeliveryItemPartial", item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnAssignDeliveryItem(string strConsignmentKey, string strDeliveryItemKey)
        {
            var oConsignmentKey = new Guid(strConsignmentKey);
            var oDeliveryItemKey = new Guid(strDeliveryItemKey);
            _itemService.UnAssignDeliveryItem(oConsignmentKey, oDeliveryItemKey);

            return GetDeliveryItems(oConsignmentKey);
        }

        public ActionResult GetDeliveryItems(Guid oConsignmentKey)
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _itemService.GetConsignmentDeliveryItems(oConsignmentKey);
            ViewBag.GridRecordCount = data.Count();

            return PartialView("DeliveryItemGridPartial", data);
        }
    }
}
