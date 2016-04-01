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
    public class ActivityController : ApiControllerBase
    {
        public ActivityController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/activity        
        public HttpResponseMessage PostActivity(int childId, Activity activity)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    activity.Child = child;

                    _db.Activities.Add(activity);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new activity entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, activity);
                    response.Headers.Location = new Uri(Url.Link("ActivityApi", new { id = activity.Id }));

                    LogNewEntryForChild(childId, "activity");                     
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