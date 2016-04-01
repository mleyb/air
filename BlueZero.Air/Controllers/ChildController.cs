using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Data.Services;
using BlueZero.Air.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueZero.Air.Filters;
using Microsoft.AspNet.SignalR;
using WebMatrix.WebData;

namespace BlueZero.Air.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Parent, Administrator")]
    public class ChildController : ControllerBase
    {
        private IActivityService _activityService;
        private IBottleService _bottleService;
        private IChildService _childService;
        private IDrinkService _drinkService;
        private IFirstAidService _firstAidService;
        private IMealService _mealService;
        private IMedicineService _medicineService;
        private IMilestoneService _milestoneService;
        private INappyService _nappyService;
        private INoteService _noteService;
        private ISickService _sickService;
        private ISleepService _sleepService;
        private ISnackService _snackService;

        public ChildController(ILog log,
                               IActivityService activityService,
                               IBottleService bottleService,
                               IChildService childService,
                               IDrinkService drinkService,
                               IFirstAidService firstAidService,
                               IMealService mealService,
                               IMedicineService medicineService,
                               IMilestoneService milestoneService,
                               INappyService nappyService,
                               INoteService noteService,
                               ISickService sickService,
                               ISleepService sleepService,
                               ISnackService snackService) : base(log) 
        {
            _activityService = activityService;
            _bottleService = bottleService;
            _childService = childService;
            _drinkService = drinkService;
            _firstAidService = firstAidService;
            _mealService = mealService;
            _medicineService = medicineService;
            _milestoneService = milestoneService;
            _nappyService = nappyService;
            _noteService = noteService;
            _sickService = sickService;
            _sleepService = sleepService;
            _snackService = snackService;
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Summary(long id)
        {
            Child child = _childService.GetById(id);

            var viewModel = new ChildSummaryViewModel
            {
                Id = id,
                Name = String.Concat(child.Forename, " ", child.Surname),
                DateOfBirth = child.DateOfBirth,
                Notes = child.Notes
            };           

            return PartialView("_SummaryPartial", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Today(long id)
        {
            var viewModel = new ChildTodayViewModel
            {
                Id = id,
                DateString = DateTime.Now.Date.ToLongDateString(),
                Entries = GetDayEntries(id, DateTime.UtcNow)
            };

            return PartialView("_TodayPartial", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult DayByDay(long id)
        {
            var viewModel = new ChildDayByDayViewModel
            {
                Id = id,
                DateString = DateTime.Now.Date.ToString("dd-MM-yyyy"),
                Entries = GetDayEntries(id, DateTime.UtcNow)
            };

            _log.DebugFormat("Retrieved {0} entries for child with Id '{1}' for date {2}.", viewModel.Entries.Count, id, viewModel.DateString);

            return PartialView("_DayByDayPartial", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult DayEntryList(int id, [ModelBinder(typeof(DateTimeModelBinder))] DateTime date)
        {
            ViewData["TableId"] = String.Format("daybyday-entries-list-{0}", id);

            List<DayEntry> entries = GetDayEntries(id, date);

            _log.DebugFormat("Retrieved {0} entries for child with Id '{1}' for date {2}.", entries.Count, id, date.ToString("dd-MM-yyyy"));

            return PartialView("_DayEntryListPartial", entries);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Charts(long id)
        {
            //return PartialView("_ChartsPartial", new ChildChartViewModel { Id = id });
            return PartialView("_ChartsPartial");
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Notes(long id)
        {
            List<Note> notes = _noteService.GetForChild(id);

            List<ChildNoteViewModel> viewModels = notes.Select(n => new ChildNoteViewModel { Date = n.Date, Detail = n.Detail }).ToList();

            return PartialView("_NotesPartial", viewModels);
        }

        protected override void Dispose(bool disposing)
        {
            _activityService.Dispose();
            _bottleService.Dispose();
            _childService.Dispose();
            _drinkService.Dispose();
            _firstAidService.Dispose();
            _mealService.Dispose();
            _medicineService.Dispose();
            _milestoneService.Dispose();
            _nappyService.Dispose();
            _noteService.Dispose();
            _sickService.Dispose();
            _sleepService.Dispose();
            _snackService.Dispose();

            base.Dispose(disposing);
        }

        private List<DayEntry> GetDayEntries(long id, DateTime date)
        {
            var entries = new List<DayEntry>();

            entries.AddRange(_activityService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Activity, DateTime = x.Date }).ToList());
            entries.AddRange(_bottleService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Bottle, DateTime = x.Date }).ToList());
            entries.AddRange(_drinkService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Drink, DateTime = x.Date }).ToList());
            entries.AddRange(_firstAidService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.FirstAid, DateTime = x.Date }).ToList());
            entries.AddRange(_mealService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Meal, DateTime = x.Date }).ToList());
            entries.AddRange(_medicineService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Medicine, DateTime = x.Date }).ToList());
            entries.AddRange(_milestoneService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Milestone, DateTime = x.Date }).ToList());
            entries.AddRange(_nappyService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Nappy, DateTime = x.Date }).ToList());
            entries.AddRange(_noteService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Note, DateTime = x.Date }).ToList());
            entries.AddRange(_sickService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Sick, DateTime = x.Date }).ToList());
            entries.AddRange(_sleepService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Sleep, DateTime = x.Date }).ToList());
            entries.AddRange(_snackService.GetForChildByDate(id, date).Select(x => new DayEntry { Id = x.Id, Type = EntryType.Snack, DateTime = x.Date }).ToList());

            entries = entries.OrderByDescending(e => e.DateTime).ToList();

            return entries;
        }
    }
}