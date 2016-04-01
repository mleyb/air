using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class SleepService : DataService, ISleepService
    {
        public SleepService(IDataContext db) : base(db) { }

        public List<Sleep> GetForChild(long id)
        {
            return _db.Sleeps.Where(s => s.Child.Id == id).OrderByDescending(s => s.Date).ToList();
        }

        public List<Sleep> GetForChildByDate(long id, DateTime date)
        {
            return _db.Sleeps.Where(s => s.Child.Id == id && EntityFunctions.TruncateTime(s.Date) == date.Date).OrderByDescending(d => d.Date).ToList();
        }

        public Sleep GetById(long id)
        {
            return _db.Sleeps.Find(id);
        }
    }
}
