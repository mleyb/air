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
    public class SleepController : ApiControllerBase
    {
        public SleepController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/sleep
        public HttpResponseMessage PostSleep(int childId, Sleep sleep)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    sleep.Child = child;

                    _db.Sleeps.Add(sleep);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new sleep entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, sleep);
                    response.Headers.Location = new Uri(Url.Link("SleepApi", new { id = sleep.Id }));

                    LogNewEntryForChild(childId, "sleep");
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