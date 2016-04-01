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
    public class ActivityController : ControllerBase
    {
        private IActivityService _activityService;

        public ActivityController(ILog log, IActivityService activityService) : base(log)
        {
            _activityService = activityService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Activity activity = _activityService.GetById(id);

            var viewModel = new ActivityViewModel
            {
                Date = activity.Date,
                Description = activity.Description,
                Detail = activity.Detail
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}