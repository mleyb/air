using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Support;
using log4net;

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer, Parent")]
    public class GCMRegistrationController : ApiControllerBase
    {
        public GCMRegistrationController(ILog log, IDataContext db) : base(log, db)            
        {
        }

        // POST api/gcm
        public HttpResponseMessage PostGCMRegistration(string registrationId)
        {            
            string identityId = ((AccessTokenIdentity)User.Identity).Id;

            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Carer carer = _db.Carers.SingleOrDefault(c => c.Id == identityId);
                if (carer != null)
                {
                    GCMRegistration gcmRegistration = new GCMRegistration { RegistrationId = registrationId, Carer = carer };

                    _db.GCMRegistrations.Add(gcmRegistration);
                
                    _db.SaveChanges();

                    response = Request.CreateResponse(HttpStatusCode.Created, gcmRegistration);
                    response.Headers.Location = new Uri(Url.Link("GCMRegistrationApi", new { id = gcmRegistration.Id }));                    
                }
                else
                {
                    // TODO - handle the case where identity could be a parent....

                    _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            else
            {
                _log.ErrorFormat("Failed to create new GCMRegistration entity for Carer entity with Id '{1}'. The model state was invalid.", identityId);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return response;
        }

        // DELETE api/gcm/id
        public HttpResponseMessage DeleteGCMRegistration(string registrationId)
        {
            GCMRegistration registration = _db.GCMRegistrations.SingleOrDefault(g => g.RegistrationId == registrationId);
            if (registration == null)
            {
                _log.ErrorFormat("Failed to delete GCMRegistration entity with Id '{0}'. The entity was not found.", registrationId);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                _db.GCMRegistrations.Remove(registration);

                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                _log.ErrorFormat("Failed to delete GCMRegistration entity with Id '{0}'. The entity has been removed.", registrationId);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _log.InfoFormat("Deleted GCMRegistration entity with Id '{0}'.", registrationId);
            return Request.CreateResponse(HttpStatusCode.OK, registration);
        }
    }
}