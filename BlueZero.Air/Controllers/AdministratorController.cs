using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace BlueZero.Air.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : ControllerBase
    {
        public AdministratorController(ILog log) : base(log)
        {
        }

        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Status()
        {
            return View("Status");
        }

        public ActionResult VisitorMapPerMonth()
        {
            return PartialView("_VisitorMapPerMonthPartial");
        }

        public ActionResult VisitorMapPerYear()
        {
            return PartialView("_VisitorMapPerYearPartial");
        }
    }
}