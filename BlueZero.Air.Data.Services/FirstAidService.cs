using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class FirstAidService : DataService, IFirstAidService
    {
        public FirstAidService(IDataContext db) : base(db) { }

        public List<FirstAid> GetForChild(long id)
        {
            return _db.FirstAids.Where(f => f.Child.Id == id).OrderByDescending(f => f.Date).ToList();
        }

        public List<FirstAid> GetForChildByDate(long id, DateTime date)
        {
            return _db.FirstAids.Where(f => f.Child.Id == id && EntityFunctions.TruncateTime(f.Date) == date.Date).OrderByDescending(f => f.Date).ToList();
        }

        public FirstAid GetById(long id)
        {
            return _db.FirstAids.Find(id);
        }
    }
}
