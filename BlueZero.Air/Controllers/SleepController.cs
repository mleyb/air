using BlueZero.Air.Data.Models;
using BlueZero.Air.Data.Services;
using BlueZero.Air.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air.Controllers
{
    [Authorize(Roles = "Parent, Administrator")]
    public class SleepController : ControllerBase
    {
        private ISleepService _sleepService;

        public SleepController(ILog log, ISleepService sleepService) : base(log)
        {
            _sleepService = sleepService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Sleep sleep = _sleepService.GetById(id);

            var viewModel = new SleepViewModel
            {
                Date = sleep.Date,
                Start = sleep.Start,
                End = sleep.End,
                Duration = sleep.End.Subtract(sleep.Start)
            };

            return PartialView("_DetailsDialogPartial", new SleepViewModel());
        }
    }
}