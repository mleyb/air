using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class ChildService : DataService, IChildService
    {
        public ChildService(IDataContext db) : base(db) { }

        public Child GetById(long id)
        {
            return _db.Children.Single(c => c.Id == id);
        }
    }
}
