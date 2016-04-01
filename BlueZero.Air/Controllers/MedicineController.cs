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
    public class MedicineController : ControllerBase
    {
        private IMedicineService _medicineService;

        public MedicineController(ILog log, IMedicineService medicineService) : base(log)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Medicine medicine = _medicineService.GetById(id);

            var viewModel = new MedicineViewModel
            {
                Date = medicine.Date,                
                Type = medicine.Type,
                Detail = medicine.Detail,
                Amount = medicine.Amount
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}