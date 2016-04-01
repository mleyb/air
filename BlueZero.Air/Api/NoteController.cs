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
    public class NoteController : ApiControllerBase
    {
        public NoteController(ILog log, IDataContext db, IUserNotifier notifier) : base(log, db, notifier)
        {
        }
       
        // POST api/child/1/note        
        public HttpResponseMessage PostNote(int childId, Note note)
        {
            HttpResponseMessage response = null;

            if (ModelState.IsValid)
            {
                Child child = _db.Children.Find(childId);
                if (child != null)
                {
                    note.Child = child;

                    _db.Notes.Add(note);
                    _db.SaveChanges();

                    _notifier.NotifyParent(child.Id, "A new note entry has been recorded.");

                    response = Request.CreateResponse(HttpStatusCode.Created, note);
                    response.Headers.Location = new Uri(Url.Link("NoteApi", new { id = note.Id }));

                    LogNewEntryForChild(childId, "note");
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