using BlueZero.Air.Data;
using BlueZero.Air.Filters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Models;
using BlueZero.Air.Data.Services;
using WebMatrix.WebData;
using System.Net;
using BlueZero.Air.Support;

namespace BlueZero.Air.Controllers
{
    [Authorize(Roles = "Parent, Administrator")]
    public class ParentController : ControllerBase
    {
        private IParentService _parentService;

        public ParentController(ILog log, IParentService parentService) : base(log) 
        {
            _parentService = parentService;
        }

        public ActionResult Index()
        {
            Parent parent = _parentService.GetByUserId(User.GetUserId());

            var model = new ParentIndexViewModel();
            model.ChildInfos = parent.Children.Select(c => new ChildInfoModel { Id = c.Id, Name = String.Concat(c.Forename, " ", c.Surname) }).ToList();

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _parentService.Dispose();
        }
    }
}