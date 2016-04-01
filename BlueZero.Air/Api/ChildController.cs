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
using System.Web.Mvc;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Support;
using EntityFramework.Extensions;
using log4net;

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer")]
    public class ChildController : ApiController
    {
        private ILog _log;
        private IDataContext _db;
        private IRandomDataGenerator _randomDataGenerator;

        public ChildController(ILog log, IDataContext db, IRandomDataGenerator randomDataGenerator)
        {
            _log = log;
            _db = db;
            _randomDataGenerator = randomDataGenerator;
        }

        // GET api/Child        
        public IEnumerable<Child> GetChildren()
        {
            string identityId = ((AccessTokenIdentity)User.Identity).Id;

            Carer carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identityId);
            if (carer == null)
            {
                _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return carer.Children.AsEnumerable();
        }

        // GET api/Child/5
        public Child GetChild(int id)
        {
            string identityId = ((AccessTokenIdentity)User.Identity).Id;

            Carer carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identityId);
            if (carer == null)
            {
                _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return carer.Children.SingleOrDefault(c => c.Id == id);
        }

        // PUT api/Child/5
        public HttpResponseMessage PutChild(int id, Child child)
        {
            if (ModelState.IsValid && id == child.Id)
            {
                _db.SetModified(child);

                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _log.ErrorFormat("Failed to update Child entity with Id '{0}'. The entity was removed or updated elsewhere.", id);
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                _log.InfoFormat("Updated Child entity with Id '{0}'.", id);
                return Request.CreateResponse(HttpStatusCode.OK, child);
            }
            else
            {
                _log.ErrorFormat("Failed to update Child entity with Id '{0}'. The model state was invalid.", id);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Child
        public HttpResponseMessage PostChild(Child child)
        {
            string identityId = ((AccessTokenIdentity)User.Identity).Id;

            if (ModelState.IsValid)
            {
                Carer carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identityId);
                if (carer != null)
                {
                    child.DateCreated = DateTime.UtcNow;
                    child.Key = _randomDataGenerator.GenerateString(Child.KeyLength);
                    child.RegistrationCode = HttpUtility.UrlEncode(ShortGuid.NewGuid());

                    carer.Children.Add(child);

                    _db.SaveChanges();

                    _log.InfoFormat("Created new Child entity with Id '{0}', Key '{1}' and registration code '{2}' for Carer entity with Id '{3}'.", child.Id, child.Key, child.RegistrationCode, identityId);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, child);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "child", id = child.Id }));
                    return response;
                }
                else
                {
                    _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            else
            {
                _log.ErrorFormat("Failed to create new Child entity for Carer entity with Id '{1}'. The model state was invalid.", identityId);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Child/5
        public HttpResponseMessage DeleteChild(int id)
        {
            Child child = _db.Children.Find(id);
            if (child == null)
            {
                _log.ErrorFormat("Failed to delete Child entity with Id '{0}'. The entity was not found.", id);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                /* TODO - require actual deletion?
                _db.Activities.Delete(a => a.Child.Id == child.Id);
                _db.Bottles.Delete(b => b.Child.Id == child.Id);
                _db.Drinks.Delete(d => d.Child.Id == child.Id);
                _db.FirstAids.Delete(f => f.Child.Id == child.Id);
                _db.Meals.Delete(m => m.Child.Id == child.Id);
                _db.Medicines.Delete(m => m.Child.Id == child.Id);
                _db.Milestones.Delete(m => m.Child.Id == child.Id);
                _db.Nappies.Delete(n => n.Child.Id == child.Id);
                _db.Notes.Delete(n => n.Child.Id == child.Id);
                _db.Sicks.Delete(s => s.Child.Id == child.Id);
                _db.Sleeps.Delete(s => s.Child.Id == child.Id);
                _db.Snacks.Delete(s => s.Child.Id == child.Id);

                _db.Children.Remove(child);
                */

                // just mark as deleted for now
                child.Deleted = true;                

                _db.SaveChanges();                
            }
            catch (DbUpdateConcurrencyException)
            {
                _log.ErrorFormat("Failed to delete Child entity with Id '{0}'. The entity has been removed.", id);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _log.InfoFormat("Deleted Child entity with Id '{0}'.", id);
            return Request.CreateResponse(HttpStatusCode.OK, child);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}