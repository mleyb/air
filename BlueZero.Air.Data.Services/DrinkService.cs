using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class DrinkService : DataService, IDrinkService
    {
        public DrinkService(IDataContext db) : base(db) { }

        public List<Drink> GetForChild(long id)
        {
            return _db.Drinks.Where(d => d.Child.Id == id).OrderByDescending(d => d.Date).ToList();
        }

        public List<Drink> GetForChildByDate(long id, DateTime date)
        {
            return _db.Drinks.Where(d => d.Child.Id == id && EntityFunctions.TruncateTime(d.Date) == date.Date).OrderByDescending(d => d.Date).ToList();
        }

        public Drink GetById(long id)
        {
            return _db.Drinks.Find(id);
        }
    }
}
