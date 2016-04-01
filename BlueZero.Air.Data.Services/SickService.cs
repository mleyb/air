using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class SickService : DataService, ISickService
    {
        public SickService(IDataContext db) : base(db) { }

        public List<Sick> GetForChild(long id)
        {
            return _db.Sicks.Where(s => s.Child.Id == id).OrderByDescending(s => s.Date).ToList();
        }

        public List<Sick> GetForChildByDate(long id, DateTime date)
        {
            return _db.Sicks.Where(s => s.Child.Id == id && EntityFunctions.TruncateTime(s.Date) == date.Date).OrderByDescending(s => s.Date).ToList();
        }

        public Sick GetById(long id)
        {
            return _db.Sicks.Find(id);
        }
    }
}
