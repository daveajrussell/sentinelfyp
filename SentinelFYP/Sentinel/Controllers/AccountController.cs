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

namespace Sentinel.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private ISecurityService _securityService;

        public AccountController(IUserService userService, ISecurityService securityService)
        {
            if (userService == null)
                throw new ArgumentNullException("user service");

            _userService = userService;

            if (securityService == null)
                throw new ArgumentNullException("security service");

            _securityService = securityService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LogonUserViewModel model)
        {
            var userAgent = Request.UserAgent;
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
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            _securityService.Logout(State.Session.SessionID);

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Account");
        }
    }
}
