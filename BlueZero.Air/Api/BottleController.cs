using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using log4net.Core;
using System.Web.Mvc;
using log4net;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using System.Threading.Tasks;
using BlueZero.Air.Support;

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer")]
    public class BottleController : ApiControllerBase
    {       
        public BottleController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/bottle        
        public HttpResponseMessage PostBottle(int childId, Bottle bottle)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    bottle.Child = child;

                    _db.Bottles.Add(bottle);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new bottle entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, bottle);
                    response.Headers.Location = new Uri(Url.Link("BottleApi", new { id = bottle.Id }));

                    LogNewEntryForChild(childId, "bottle");
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