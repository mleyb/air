using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IMealService : IDisposable
    {
        List<Meal> GetForChild(long id);

        List<Meal> GetForChildByDate(long id, DateTime date);

        Meal GetById(long id);
    }
}
