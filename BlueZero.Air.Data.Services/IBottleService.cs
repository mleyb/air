using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IBottleService : IDisposable
    {
        List<Bottle> GetForChild(long id);

        List<Bottle> GetForChildByDate(long id, DateTime date);

        Bottle GetById(long id);
    }
}
