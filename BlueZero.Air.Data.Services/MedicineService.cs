using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class MedicineService : DataService, IMedicineService
    {
        public MedicineService(IDataContext db) : base(db) { }

        public List<Medicine> GetForChild(long id)
        {
            return _db.Medicines.Where(m => m.Child.Id == id).OrderByDescending(m => m.Date).ToList();
        }

        public List<Medicine> GetForChildByDate(long id, DateTime date)
        {
            return _db.Medicines.Where(m => m.Child.Id == id && EntityFunctions.TruncateTime(m.Date) == date.Date).OrderByDescending(m => m.Date).ToList();
        }

        public Medicine GetById(long id)
        {
            return _db.Medicines.Find(id);
        }
    }
}
