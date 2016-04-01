using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class NappyService : DataService, INappyService
    {
        public NappyService(IDataContext db) : base(db) { }

        public List<Nappy> GetForChild(long id)
        {
            return _db.Nappies.Where(n => n.Child.Id == id).OrderByDescending(n => n.Date).ToList();
        }

        public List<Nappy> GetForChildByDate(long id, DateTime date)
        {
            return _db.Nappies.Where(n => n.Child.Id == id && EntityFunctions.TruncateTime(n.Date) == date.Date).OrderByDescending(n => n.Date).ToList();
        }

        public Nappy GetById(long id)
        {
            return _db.Nappies.Find(id);
        }
    }
}
