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
    public class SickController : ApiControllerBase
    {
        public SickController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/sick
        public HttpResponseMessage PostSick(int childId, Sick sick)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    sick.Child = child;

                    _db.Sicks.Add(sick);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new sick entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, sick);
                    response.Headers.Location = new Uri(Url.Link("SickApi", new { id = sick.Id }));

                    LogNewEntryForChild(childId, "sick");
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