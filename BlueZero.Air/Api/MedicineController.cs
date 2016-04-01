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
    public class MedicineController : ApiControllerBase
    {
        public MedicineController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }

        // POST api/child/1/medicine
        public HttpResponseMessage PostMedicine(int childId, Medicine medicine)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    medicine.Child = child;

                    _db.Medicines.Add(medicine);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new medicine entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, medicine);
                    response.Headers.Location = new Uri(Url.Link("MedicineApi", new { id = medicine.Id }));

                    LogNewEntryForChild(childId, "medicine");
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