using DomainModel.Interfaces.Services;
using Sentinel.Infrastructure;
using Sentinel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Sentinel.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class AdministrationController : Controller
    {
        private ISecurityService _securityService;

        public AdministrationController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        List<MenuViewModel> menuItems = new List<MenuViewModel>()
        {
            new MenuViewModel()
            {
                Display = "User Administration",
                Description = "Perform administrative tasks for users",
                URL = "~/Administration/UserAdministration",
                Permission = "ADMINISTRATOR"
            }
        };

        List<MenuViewModel> userAdminItems = new List<MenuViewModel>()
        {
            /*new MenuViewModel()
            {
                Display = "Create User",
                URL = "~/Administration/CreateUser",
                Permission = "ADMINISTRATOR"
            },*/
            new MenuViewModel()
            {
                Display = "Reset Password",
                Description = "Reset a user's password",
                URL = "~/Administration/ResetPassword",
                Permission = "ADMINISTRATOR"
            }
        };

        List<ActionButtonsViewModel> userSelectItems = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "navigateBack('UserAdministration')"
            }
        };

        List<ActionButtonsViewModel> resetPasswordItems = new List<ActionButtonsViewModel>()
        {
            new ActionButtonsViewModel()
            {
                Display = "Back",
                Javascript = "navigateBack('ResetPassword')"
            },
            new ActionButtonsViewModel()
            {
                ID = "btnResetPassword",
                Display = "Reset Password",
                Permission = "ADMINISTRATOR"
            }
        };

        public ActionResult AdministrationPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", menuItems);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserAdministrationPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", userAdminItems);
        }

        public ActionResult UserAdministration()
        {
            return View();
        }

        public ActionResult UserSelectPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", userSelectItems);
        }

        public ActionResult ResetPasswordPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", resetPasswordItems);
        }

        public ActionResult ResetPassword()
        {
            var users = _securityService.GetUsers();
            return View(users);
        }

        public ActionResult GetResetPasswordPartial(string strUserKey)
        {
            var oUserKey = Guid.Parse(strUserKey);
            var user = _securityService.GetUserByUserKey(oUserKey);

            return PartialView("ResetPasswordPartial", user);
        }

        public ActionResult ResetUsersPassword(string strUserKey, string strPassword)
        {
            var oUserKey = Guid.Parse(strUserKey);
            bool result = _securityService.ResetPassword(oUserKey, strPassword);

            if (result)
                return PartialView("Dialogs/ResetPasswordConfirmDialogPartial", "Password Reset Successfully");
            else
                return PartialView("Dialogs/ResetPasswordConfirmDialogPartial", "Password Reset Failed");
        }
    }
}
