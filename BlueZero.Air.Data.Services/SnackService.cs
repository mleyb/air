using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class SnackService : DataService, ISnackService
    {
        public SnackService(IDataContext db) : base(db) { }

        public List<Snack> GetForChild(long id)
        {
            return _db.Snacks.Where(s => s.Child.Id == id).OrderByDescending(s => s.Date).ToList();
        }

        public List<Snack> GetForChildByDate(long id, DateTime date)
        {
            return _db.Snacks.Where(s => s.Child.Id == id && EntityFunctions.TruncateTime(s.Date) == date.Date).OrderByDescending(s => s.Date).ToList();
        }

        public Snack GetById(long id)
        {
            return _db.Snacks.Find(id);
        }
    }
}
