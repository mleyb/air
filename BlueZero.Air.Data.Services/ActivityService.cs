using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class ActivityService : DataService, IActivityService
    {
        public ActivityService(IDataContext db) : base(db) { }

        public List<Activity> GetForChild(long id)
        {
            return _db.Activities.Where(a => a.Child.Id == id).OrderByDescending(a => a.Date).ToList();
        }

        public List<Activity> GetForChildByDate(long id, DateTime date)
        {
            return _db.Activities.Where(a => a.Child.Id == id && EntityFunctions.TruncateTime(a.Date) == date.Date).OrderByDescending(a => a.Date).ToList();
        }

        public Activity GetById(long id)
        {
            return _db.Activities.Find(id);
        }
    }
}
