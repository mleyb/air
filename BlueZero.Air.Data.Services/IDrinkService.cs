using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IDrinkService : IDisposable
    {
        List<Drink> GetForChild(long id);

        List<Drink> GetForChildByDate(long id, DateTime date);

        Drink GetById(long id);
    }
}
