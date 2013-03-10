using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Imaging;
using DomainModel.Interfaces.Services;
using Sentinel.Grid;
using Sentinel.Helpers;
using Sentinel.Helpers.ExtensionMethods;
using Sentinel.Models;
using DomainModel.Models.AssetModels;

namespace Sentinel.Controllers
{
    [Authorize(Roles = "AUDITOR")]
    public class AssetManagementController : Controller
    {
        private IConsignmentManagementService _consignmentService;
        private IDeliveryItemManagementService _itemService;

        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            /*new MenuViewModel()
            {
                Display = "Delivery Item Management",
                URL = "~/AssetManagement/DeliveryItemManagement",
                Description = "Manage and assign delivery items to consignments."
            },*/
            new MenuViewModel()
            {
                Display = "Consignment Management",
                URL = "~/AssetManagement/ConsignmentManagement",
                Description = "Manage and assign consignments to drivers.",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                Display = "Driver Management",
                URL = "~/AssetManagement/DriverManagement",
                Description = "Manage and assign vehicles to drivers.",
                Permission = "AUDITOR"
            }
        };

        List<MenuViewModel> consignmentManagementOptions = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                URL = "~/AssetManagement/AssignedConsignments",
                Display = "Assigned Consignments",
                Description = "Display all consignments that have been assigned to a driver",
                Permission = "AUDITOR"
            },
            new MenuViewModel()
            {
                URL = "~/AssetManagement/UnAssignedConsignments",
                Display = "Unassigned Consignments",
                Description = "Display all consignents that have not yet been assigned",
                Permission = "AUDITOR"
            }
        };

        List<ActionButtonsViewModel> consignmentsDeliveryItemGridActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                ID = "btnUnassignItems",
                Display = "Unassign Selected Items",
                Permission = "ADMINISTRATOR"
            },
            new ActionButtonsViewModel()
            {
                ID = "btnAssignItems",
                Display = "Assign New Items",
                Permission = "ADMINISTRATOR"
            }
        };

        List<ActionButtonsViewModel> assignedConsignmentsPageActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "navigateBack('Index')"
            },
            new ActionButtonsViewModel()
            {
                ID = "btnUnAssignSelectedConsignments",
                Display = "Unassign Selected Consignments",
                Permission = "ADMINISTRATOR"
            },
            new ActionButtonsViewModel()
            {
                ID = "btnPrintDeliveryLabels",
                Display = "Print Labels For Selected Consignments",
                Permission = "ADMINISTRATOR"
            }
        };

        List<ActionButtonsViewModel> unassignedConsignmentsPageActions = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "navigateBack('Index')"
            },
           new ActionButtonsViewModel()
            {
                ID = "btnAssignSelectedConsignments",
                Display = "Assign Selected Consignments",
                Permission = "AUDITOR"
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

        public ActionResult AssetManagementPageActions()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", menuItems);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsignmentManagementPageActions()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", consignmentManagementOptions);
        }

        public ActionResult ConsignmentManagement()
        {
            return View();
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

        public ActionResult GetDriverPartialForAssigningConsignment()
        {
            var data = _consignmentService.GetUsersForConsignmentAssigning();
            
            if (data.Count() <= 0)
                return PartialView("ErrorDialogPartial", "No drivers are available to assign this consignment to.");
            else
                return PartialView("Dialogs/DriverSelectDialog", data);
        }

        public void AssignConsignmentToDriver(string strDriverKey, string strConsignmentKey)
        {
            Guid oDriverKey = Guid.Parse(strDriverKey);
            Guid oConsignmentKey = Guid.Parse(strConsignmentKey);

            _consignmentService.AssignConsignmentToDriver(oConsignmentKey, oDriverKey);
        }

        public void UnAssignConsignments(string strConsignmentKeys)
        {
            var keys = GetEnumerableGuidSourceFromString(strConsignmentKeys);

            foreach (var key in keys)
            {
                _consignmentService.UnAssignConsignment(key);
            }
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

        public ActionResult PrintDeliveryItemLabel(string strConsignmentKeys)
        {
            var keys = GetEnumerableGuidSourceFromString(strConsignmentKeys);
            List<AssignedDeliveryItem> items = new List<AssignedDeliveryItem>();

            foreach (var key in keys)
            {
                items.AddRange(_itemService.GetConsignmentDeliveryItems(key));
            }
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
