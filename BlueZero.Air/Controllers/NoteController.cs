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
    public class NoteController : ControllerBase
    {
        private INoteService _noteService;

        public NoteController(ILog log, INoteService noteService) : base(log)
        {
            _noteService = noteService;
        }

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "id")]
        public ActionResult Details(long id)
        {
            Note note = _noteService.GetById(id);

            var viewModel = new NoteViewModel
            {
                Date = note.Date,
                Detail = note.Detail
            };

            return PartialView("_DetailsDialogPartial", viewModel);
        }
    }
}