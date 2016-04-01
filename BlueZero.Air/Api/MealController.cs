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
    public class MealController : ApiControllerBase
    {
        public MealController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/meal
        public HttpResponseMessage PostMeal(int childId, Meal meal)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    meal.Child = child;

                    _db.Meals.Add(meal);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new meal entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, meal);
                    response.Headers.Location = new Uri(Url.Link("MealApi", new { id = meal.Id }));

                    LogNewEntryForChild(childId, "meal");
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