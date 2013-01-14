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
        private IUserService _userService;
        private ISecurityService _securityService;
        private ISentinelAuthProvider _authProvider;

        public AccountController(IUserService userService, ISecurityService securityService, ISentinelAuthProvider authProvider)
        {
            if (userService == null)
                throw new ArgumentNullException("user service");

            _userService = userService;

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
            /*var userAgent = Request.UserAgent;
            var ipAddress = "127.0.0.0";

            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipAddress = ip.ToString();
                }
            }

            var user = _securityService.LogIn(model.Username, model.Password, userAgent, ipAddress);
            State.User = user;

            if (user == null)
                return RedirectToAction("Index", "Account");
            else
            {
                _authProvider.Authenticate("", "");
                return RedirectToAction("Index", "Home");
            }*/
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
            _securityService.Logout(State.Session.SessionID);
            /*
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Account");
            */
            _authProvider.ClearAuthentication(this);
            return RedirectToAction("Login", "Account");
        }
    }
}
