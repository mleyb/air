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
    public class SickController : ControllerBase
    {
        private ISickService _sickService;

        public SickController(ILog log, ISickService sickService) : base(log)
        {
            _sickService = sickService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Sick sick = _sickService.GetById(id);

            var viewModel = new SickViewModel
            {
                Date = sick.Date,
                Description = sick.Description,
                Detail = sick.Detail,
                DoctorRecommended = sick.DoctorRecommended
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}