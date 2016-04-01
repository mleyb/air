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
using log4net;
using System.Web.Routing;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Models;
using BlueZero.Air.Support;
using System.Threading.Tasks;

namespace BlueZero.Air.Api
{
    [AccessTokenAuthorize(Roles = "Carer")]
    public class CarerController : ApiControllerBase
    {
        private IRandomDataGenerator _randomDataGenerator;

        public CarerController(ILog log, IDataContext db, IRandomDataGenerator randomDataGenerator) : base(log, db)
        {
            _randomDataGenerator = randomDataGenerator;
        }

        // GET api/carer        
        public async Task<Carer> GetCarer()
        {
            AccessTokenIdentity identity = (AccessTokenIdentity)User.Identity;

            Carer carer = await Task.Run<Carer>(() =>
            {
                carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identity.Id);
                if (carer == null)
                {
                    // this is a new carer registration
                    carer = new Carer();
                    carer.Id = identity.Id;
                    carer.DateCreated = DateTime.UtcNow;
                    carer.Key = _randomDataGenerator.GenerateString(Carer.KeyLength);
                    carer.Email = identity.Email;
                    carer.Name = identity.Name;

                    _db.Carers.Add(carer);
                    _db.SaveChanges();

                    _log.InfoFormat("Created new user with Id = '{0}', Name = '{1}', Email = '{2}'.", carer.Id, carer.Name, carer.Email);
                }

                return carer;
            });

            return carer;
        }

        // GET api/carer/child
        //[AccessTokenAuthorize(Roles = "Carer")]
        //public IEnumerable<Child> GetChildren()
        //{
        //    string identityId = ((AccessTokenIdentity)User.Identity).Id;

        //    Carer carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identityId);
        //    if (carer == null)
        //    {
        //        _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return carer.Children.AsEnumerable();
        //}

        // GET api/carer/child/1
        //[AccessTokenAuthorize(Roles = "Carer")]
        //public Child GetChild(int childId)
        //{
        //    string identityId = ((AccessTokenIdentity)User.Identity).Id;

        //    Carer carer = _db.Carers.Include(c => c.Children).SingleOrDefault(c => c.Id == identityId);
        //    if (carer == null)
        //    {
        //        _log.ErrorFormat("Failed to find Carer entity with Id '{0}'.", identityId);
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return carer.Children.SingleOrDefault(c => c.Id == childId);
        //}        
        
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}