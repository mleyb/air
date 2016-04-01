using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class NoteService : DataService, INoteService
    {
        public NoteService(IDataContext db) : base(db) { }

        public List<Note> GetForChild(long id)
        {
            return _db.Notes.Where(n => n.Child.Id == id).OrderByDescending(n => n.Date).ToList();
        }

        public List<Note> GetForChildByDate(long id, DateTime date)
        {
            return _db.Notes.Where(n => n.Child.Id == id && EntityFunctions.TruncateTime(n.Date) == date.Date).OrderByDescending(n => n.Date).ToList();
        }

        public Note GetById(long id)
        {
            return _db.Notes.Find(id);
        }
    }
}
