using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using System.Threading.Tasks;

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer")]
    public class SnackController : ApiControllerBase
    {
        public SnackController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/snack
        public HttpResponseMessage PostSnack(int childId, Snack snack)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    snack.Child = child;

                    _db.Snacks.Add(snack);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new snack entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, snack);
                    response.Headers.Location = new Uri(Url.Link("SnackApi", new { id = snack.Id }));

                    LogNewEntryForChild(childId, "snack");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return response;
        }        
    }
}