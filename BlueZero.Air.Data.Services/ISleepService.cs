using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface ISleepService : IDisposable
    {
        List<Sleep> GetForChild(long id);

        List<Sleep> GetForChildByDate(long id, DateTime date);

        Sleep GetById(long id);
    }
}
