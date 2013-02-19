using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DomainModel.Interfaces.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using DomainModel.Models;
using GMap.NET;
using Sentinel.Helpers.ExtensionMethods;
using System.Drawing.Imaging;
using System.Drawing;
using DomainModel.SecurityModels;
using System.Web.Security;
using Sentinel.Helpers;

namespace Sentinel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
