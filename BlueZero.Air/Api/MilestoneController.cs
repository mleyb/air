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
    public class MilestoneController : ApiControllerBase
    {
        public MilestoneController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/milestone
        public HttpResponseMessage PostMilestone(int childId, Milestone milestone)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    milestone.Child = child;

                    _db.Milestones.Add(milestone);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new milestone entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, milestone);
                    response.Headers.Location = new Uri(Url.Link("MilestoneApi", new { id = milestone.Id }));

                    LogNewEntryForChild(childId, "milestone");
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