using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using SignalR.Hubs;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            logger.Info("Index()");
            return View();
        }

        public JsonResult AddLogEntry()
        {
            logger.Info("Hello from AddLogEntry()!");
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
