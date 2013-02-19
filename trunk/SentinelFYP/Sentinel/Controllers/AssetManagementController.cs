using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using DomainModel.Interfaces.Services;
using ASPRazorWebGrid;
using Sentinel.Helpers;
using Sentinel.Helpers.ExtensionMethods;
using Sentinel.Models;
using ASPRazorWebGrid.UI;

namespace Sentinel.Controllers
{
    public class AssetManagementController : Controller
    {
        private IConsignmentManagementService _consignmentService;
        private IDeliveryItemManagementService _itemService;

        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                Display = "Consignment Management",
                URL = "~/AssetManagement/ConsignmentManagement",
                Description = "Do Consignment Stuff"
            },
            new MenuViewModel()
            {
                Display = "Delivery Item Management",
                URL = "~/AssetManagement/DeliveryItemManagement",
                Description = "Do Delivery Item Stuff"
            },
            new MenuViewModel()
            {
                Display = "Driver Management",
                URL = "~/AssetManagement/DriverManagement",
                Description = "Do Driver Stuff"
            }
        };

        List<MenuViewModel> consignmentManagementOptions = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                ID = "AssignedConsignments",
                Display = "Assigned Consignments",
                Description = "Display all consignments that have been assigned to a driver"
            },
            new MenuViewModel()
            {
                ID = "UnAssignedConsignments",
                Display = "Unassigned Consignments",
                Description = "Display all consignents that have not yet been assigned"
            }
        };

        List<ActionButtonsViewModel> consignmentsDeliveryItemGridActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                ID = "btnUnassignItems",
                Display = "Unassign Selected Items"
            },
            new ActionButtonsViewModel()
            {
                ID = "btnAssignItems",
                Display = "Assign New Items"
            }
        };

        List<ActionButtonsViewModel> assignedConsignmentsPageActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "javascript:history.back(1)"
            },
            new ActionButtonsViewModel()
            {
                Display = "Unassign Selected Consignments"
            },
            new ActionButtonsViewModel()
            {
                Display = "Reassign Selected Consignments"
            },
            new ActionButtonsViewModel()
            {
                Display = "Print Labels For Selected Consignments"
            }
        };

        List<ActionButtonsViewModel> unassignedConsignmentsPageActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "javascript:history.back(1)"
            },
           new ActionButtonsViewModel()
            {
                Display = "Assign Selected Consignments"
            }
        };

        public AssetManagementController(IConsignmentManagementService consignmentService, IDeliveryItemManagementService itemService)
        {
            if (consignmentService == null)
                throw new ArgumentNullException("Consignment Service");

            _consignmentService = consignmentService;

            if (itemService == null)
                throw new ArgumentNullException("Item Service");

            _itemService = itemService;
        }

        public ActionResult Index()
        {
            return View(menuItems);
        }

        public ActionResult ConsignmentManagement()
        {
            return View(consignmentManagementOptions);
        }

        public ActionResult AssignedConsignments()
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _consignmentService.GetAssignedConsignments();

            ViewBag.GridRecordCount = data.Count();

            return View("ConsignmentManagement/Assigned", data);
        }

        public ActionResult AssignedConsignmentsPageActions()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", assignedConsignmentsPageActions);
        }

        public ActionResult UnAssignedConsignments()
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _consignmentService.GetUnAssignedConsignments();

            ViewBag.GridRecordCount = data.Count();

            return View("ConsignmentManagement/UnAssigned", data);
        }

        public ActionResult UnAssignedConsignmentsPageActions()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", unassignedConsignmentsPageActions);
        }

        public ActionResult DeliveryItemManagement()
        {
            return View();
        }

        public ActionResult DriverManagement()
        {
            return View();
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

        public ActionResult DeliveryItemsGridActions()
        {
            return PartialView("../ActionButtons/GridActionButtonsPartial", consignmentsDeliveryItemGridActions);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetAssignDeliveryItemPartial(string strConsignmentKey)
        {
            var key = new Guid(strConsignmentKey);
            var items = _itemService.GetAllUnassignedDeliveryItems();

            if (items.Count() <= 0)
                return PartialView("ErrorDialogPartial", "No items are available to assign");
            else
                return PartialView("AssignDeliveryItemPartial", items.ToViewModel(key));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetUnAssignDeliveryItemPartial(string strDeliveryItemKeys)
        {
            var keys = GetEnumerableGuidSourceFromString(strDeliveryItemKeys);
            var item = _itemService.GetDeliveryItemsByKey(keys);
            return PartialView("UnAssignDeliveryItemPartial", item);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AssignDeliveryItems(string strConsignmentKey, string strDeliveryItemKeys)
        {
            var keys = GetEnumerableGuidSourceFromString(strDeliveryItemKeys);
            var oConsignmentKey = new Guid(strConsignmentKey);

            _itemService.AssignDeliveryItemsToConsignment(keys, oConsignmentKey);

            return GetDeliveryItems(oConsignmentKey);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UnAssignDeliveryItems(string strConsignmentKey, string strDeliveryItemKeys)
        {
            var oConsignmentKey = new Guid(strConsignmentKey);
            var keys = GetEnumerableGuidSourceFromString(strDeliveryItemKeys);

            _itemService.UnAssignDeliveryItems(oConsignmentKey, keys);

            return GetDeliveryItems(oConsignmentKey);
        }

        private ActionResult GetDeliveryItems(Guid oConsignmentKey)
        {
            var gridParameters = GridParameters.GetGridParameters();
            var data = _itemService.GetConsignmentDeliveryItems(oConsignmentKey);
            ViewBag.GridRecordCount = data.Count();

            return PartialView("DeliveryItemGridPartial", data);
        }

        public ActionResult PrintDeliveryItemLabel(string strDeliveryItemKeys)
        {
            var keys = GetEnumerableGuidSourceFromString(strDeliveryItemKeys);
            var items = _itemService.GetDeliveryItemsByKey(keys);

            return new PDFResult(items);
        }

        private IEnumerable<Guid> GetEnumerableGuidSourceFromString(string strKeys)
        {
            if (string.IsNullOrEmpty(strKeys))
                return null;

            var keys = strKeys.Split(',');

            return from key in keys
                   select new Guid(key);
        }

    }
}
