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
    public class DrinkController : ControllerBase
    {
        private IDrinkService _drinkService;

        public DrinkController(ILog log, IDrinkService drinkService) : base(log)
        {
            _drinkService = drinkService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Drink drink = _drinkService.GetById(id);

            var viewModel = new BottleViewModel
            {
                Date = drink.Date,
                Amount = drink.Amount,
                Unit = drink.Unit
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}