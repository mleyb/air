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
    public class BottleController : ControllerBase
    {
        private IBottleService _bottleService;

        public BottleController(ILog log, IBottleService bottleService) : base(log)
        {
            _bottleService = bottleService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Bottle bottle = _bottleService.GetById(id);

            var viewModel = new BottleViewModel
            {
                Date = bottle.Date,
                Amount = bottle.Amount,
                Unit = bottle.Unit
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}