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
    public class DrinkController : ApiControllerBase
    {
        public DrinkController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/sick        
        public HttpResponseMessage PostDrink(int childId, Drink drink)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    drink.Child = child;

                    _db.Drinks.Add(drink);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new drink entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, drink);
                    response.Headers.Location = new Uri(Url.Link("DrinkApi", new { id = drink.Id }));

                    LogNewEntryForChild(childId, "drink");
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