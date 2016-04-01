using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class BottleService : DataService, IBottleService
    {
        public BottleService(IDataContext db) : base(db) { }

        public List<Bottle> GetForChild(long id)
        {
            return _db.Bottles.Where(b => b.Child.Id == id).OrderByDescending(b => b.Date).ToList();
        }

        public List<Bottle> GetForChildByDate(long id, DateTime date)
        {
            return _db.Bottles.Where(b => b.Child.Id == id && EntityFunctions.TruncateTime(b.Date) == date.Date).OrderByDescending(b => b.Date).ToList();
        }

        public Bottle GetById(long id)
        {
            return _db.Bottles.Find(id);
        }
    }
}
