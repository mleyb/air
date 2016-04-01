using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BlueZero.Air.Data;
using BlueZero.Air.Support;

namespace BlueZero.Air.Api
{
    public abstract class ApiControllerBase : ApiController
    {
        protected ILog _log;
        protected IDataContext _db;
        protected IUserNotifier _notifier;

        public ApiControllerBase(ILog log, IDataContext db, IUserNotifier notifier)
        {
            _log = log;
            _db = db;
            _notifier = notifier;
        }

        public ApiControllerBase(ILog log, IDataContext db)
        {
            _log = log;
            _db = db;
        }

        protected void LogNewEntryForChild(int childId, string entryName)
        {
            _log.DebugFormat("Principal with Id '{0}' recorded new {1} entry for child with Id '{1}'", ((AccessTokenIdentity)User.Identity).Id, entryName, childId);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();

            base.Dispose(disposing);
        }
    }
}
