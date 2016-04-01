using BlueZero.Air.Filters;
using GoogleAnalyticsTracker.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueZero.Air.Support;
using System.Threading.Tasks;

namespace BlueZero.Air.Controllers
{    
    [Compress]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.OS = "Android";

            return View("Index");
        }

        public ActionResult Android()
        {
            ViewBag.OS = "Android";

            return View("Index");
        }

        public ActionResult Apple()
        {
            ViewBag.OS = "Apple";

            return View("Index");
        }

        public ActionResult Windows()
        {
            ViewBag.OS = "Windows";

            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult About()
        {
            return PartialView("_AboutPartial");
        }

        [ChildActionOnly]
        public ActionResult Features()
        {
            return PartialView("_FeaturesPartial");
        }

        [ChildActionOnly]
        public ActionResult Releases()
        {
            return PartialView("_ReleasesPartial");
        }

        [ChildActionOnly]
        public ActionResult Authors()
        {
            return PartialView("_AuthorsPartial");
        }

        [ChildActionOnly]
        public ActionResult Contact()
        {
            return PartialView("_ContactPartial");
        }

        public ActionResult Login()
        {
            if (User.IsInRole(RoleNames.Administrator))
            {
                return RedirectToAction("Index", "Administrator");
            }
            else if (User.IsInRole(RoleNames.Parent))
            {
                return RedirectToAction("Index", "Parent");
            }
            else 
            {
                return RedirectToAction("Index");
            }
        }
    }
}
