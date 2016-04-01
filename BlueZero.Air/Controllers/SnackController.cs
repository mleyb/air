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
    public class SnackController : ControllerBase
    {
        private ISnackService _snackService;

        public SnackController(ILog log, ISnackService snackService) : base(log)
        {
            _snackService = snackService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Snack snack = _snackService.GetById(id);

            var viewModel = new SnackViewModel
            {
                Date = snack.Date,
                Description = snack.Description,
                AmountConsumed = snack.AmountConsumed
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}