using DomainModel.Interfaces.Services;
using DomainModel.Models.AuditModels;
using DomainModel.SecurityModels;
using Sentinel.Infrastructure;
using Sentinel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sentinel.Helpers.ExtensionMethods;

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
            new MenuViewModel()
            {
                Display = "Create User",
                Description = "Create a new user.",
                URL = "~/Administration/CreateUser",
                Permission = "ADMINISTRATOR"
            },
            new MenuViewModel()
            {
                Display = "Reset Password",
                Description = "Reset a user's password.",
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

        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserAdministration()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            var users = _securityService.GetUsers(State.User);
            return View(users);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateUser(string strUsername, string strKeys, string strFirstName, string strLastName, string strNumber, string strEmail)
        {
            var newUserKey = _securityService.CreateUser(strUsername, strKeys, strFirstName, strLastName, strNumber, strEmail);
            var newUser = _securityService.GetUserByUserKey(newUserKey);
            var roles = _securityService.GetRolesForUser(newUser);

            return PartialView("Dialogs/UserCreateConfirm", newUser.ToViewModel(roles));
        }

        public ActionResult GetRolesForUser()
        {
            var data = _securityService.GetRolesForUser(State.User);
            return PartialView("RoleSelectPartial", data);
        }

        public ActionResult CreateUserPageActions()
        {
            List<ActionButtonsViewModel> createUserPageActions = new List<ActionButtonsViewModel>()
            {
                new ActionButtonsViewModel()
                {
                    Display = "Back",
                    Javascript = "navigateBack('UserAdministration')"
                },
                new ActionButtonsViewModel()
                {
                    ID = "btnCreateUser",
                    Display = "Create User",
                    Permission = "ADMINISTRATOR"
                }
            };

            return PartialView("../ActionButtons/PageActionButtonsPartial", createUserPageActions);
        }
        
        public ActionResult AdministrationPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", menuItems);
        }

        public ActionResult UserAdministrationPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsLargePartial", userAdminItems);
        }

        public ActionResult UserSelectPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", userSelectItems);
        }

        public ActionResult ResetPasswordPageActionButtons()
        {
            return PartialView("../ActionButtons/PageActionButtonsPartial", resetPasswordItems);
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
