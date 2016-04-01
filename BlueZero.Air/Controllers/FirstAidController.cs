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
    public class FirstAidController : ControllerBase
    {
        private IFirstAidService _firstAidService;

        public FirstAidController(ILog log, IFirstAidService firstAidService) : base(log)
        {
            _firstAidService = firstAidService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            FirstAid firstAid = _firstAidService.GetById(id);

            var viewModel = new FirstAidViewModel
            {
                Date = firstAid.Date,
                Description = firstAid.Description,
                Reason = firstAid.Reason,
                Detail = firstAid.Detail,
                DoctorRecommended = firstAid.DoctorRecommended
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}