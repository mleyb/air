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

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer")]
    public class NappyController : ApiControllerBase
    {
        public NappyController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }         

        // POST api/child/1/nappy        
        public HttpResponseMessage PostNappy(int childId, Nappy nappy)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    nappy.Child = child;

                    _db.Nappies.Add(nappy);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new nappy entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, nappy);
                    response.Headers.Location = new Uri(Url.Link("NappyApi", new { id = nappy.Id }));

                    LogNewEntryForChild(childId, "nappy");
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