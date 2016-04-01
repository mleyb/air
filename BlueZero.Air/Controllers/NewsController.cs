using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace BlueZero.Air.Controllers
{
    [Authorize(Roles = "Parent, Administrator")]
    public class NewsController : ControllerBase
    {
        public NewsController(ILog log) : base(log)
        {
        }

        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
