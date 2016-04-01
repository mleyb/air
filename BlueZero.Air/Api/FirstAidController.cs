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
    public class FirstAidController : ApiControllerBase
    {
        public FirstAidController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/firstaid
        public HttpResponseMessage PostFirstAid(int childId, FirstAid firstAid)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    firstAid.Child = child;

                    _db.FirstAids.Add(firstAid);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new first aid entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, firstAid);
                    response.Headers.Location = new Uri(Url.Link("FirstAidApi", new { id = firstAid.Id }));

                    LogNewEntryForChild(childId, "first aid");
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