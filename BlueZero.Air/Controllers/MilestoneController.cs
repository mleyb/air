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
    public class MilestoneController : ControllerBase
    {
        private IMilestoneService _milestoneService;

        public MilestoneController(ILog log, IMilestoneService milestoneService) : base(log)
        {
            _milestoneService = milestoneService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Milestone milestone = _milestoneService.GetById(id);

            var viewModel = new MilestoneViewModel
            {
                Date = milestone.Date,
                Description = milestone.Description,
                Detail = milestone.Detail
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}