using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class MilestoneService : DataService, IMilestoneService
    {
        public MilestoneService(IDataContext db) : base(db) { }

        public List<Milestone> GetForChild(long id)
        {
            return _db.Milestones.Where(m => m.Child.Id == id).OrderByDescending(m => m.Date).ToList();
        }

        public List<Milestone> GetForChildByDate(long id, DateTime date)
        {
            return _db.Milestones.Where(m => m.Child.Id == id && EntityFunctions.TruncateTime(m.Date) == date.Date).OrderByDescending(m => m.Date).ToList();
        }

        public Milestone GetById(long id)
        {
            return _db.Milestones.Find(id);
        }
    }
}
