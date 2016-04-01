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
    public class NappyController : ControllerBase
    {
        private INappyService _nappyService;

        public NappyController(ILog log, INappyService nappyService) : base(log)
        {
            _nappyService = nappyService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Nappy nappy = _nappyService.GetById(id);

            var viewModel = new NappyViewModel
            {
                Date = nappy.Date,
                Detail = nappy.Detail,
                Dirty = nappy.Dirty
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}