using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public class CarerService : DataService, ICarerService
    {
        public CarerService(IDataContext db) : base(db) {}

        public Carer GetForChild(long id)
        {
            throw new NotImplementedException();
        }
    }
}
