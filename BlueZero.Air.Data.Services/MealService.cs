using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class MealService : DataService, IMealService
    {
        public MealService(IDataContext db) : base(db) { }

        public List<Meal> GetForChild(long id)
        {
            return _db.Meals.Where(m => m.Child.Id == id).OrderByDescending(m => m.Date).ToList();
        }

        public List<Meal> GetForChildByDate(long id, DateTime date)
        {
            return _db.Meals.Where(m => m.Child.Id == id && EntityFunctions.TruncateTime(m.Date) == date.Date).OrderByDescending(m => m.Date).ToList();
        }

        public Meal GetById(long id)
        {
            return _db.Meals.Find(id);
        }
    }
}
