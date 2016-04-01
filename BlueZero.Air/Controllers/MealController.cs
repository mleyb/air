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
    public class MealController : ControllerBase
    {
        private IMealService _mealService;

        public MealController(ILog log, IMealService mealService) : base(log)
        {
            _mealService = mealService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Meal meal = _mealService.GetById(id);

            var viewModel = new MealViewModel
            {
                Date = meal.Date,
                AmountConsumed = meal.AmountConsumed
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}