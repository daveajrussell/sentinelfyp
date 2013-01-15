using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sentinel.Helpers;
using DomainModel.Interfaces.Services;
using DomainModel.SecurityModels;
using Sentinel.Models;
using System.Web.Security;
using System.Net;
using Sentinel.Infrastructure;

namespace Sentinel.Controllers
{
    public class AccountController : Controller
    {
        private ISecurityService _securityService;
        private ISentinelAuthProvider _authProvider;

        public AccountController(ISecurityService securityService, ISentinelAuthProvider authProvider)
        {
            if (securityService == null)
                throw new ArgumentNullException("security service");

            _securityService = securityService;

            if (authProvider == null)
                throw new ArgumentNullException("auth provider");

            _authProvider = authProvider;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogonUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Username, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Username or Password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            _securityService.Logout();
            _authProvider.ClearAuthentication(this);
            return RedirectToAction("Login", "Account");
        }
    }
}
