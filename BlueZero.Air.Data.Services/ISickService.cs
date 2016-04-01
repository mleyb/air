using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface ISickService : IDisposable
    {
        List<Sick> GetForChild(long id);

        List<Sick> GetForChildByDate(long id, DateTime date);

        Sick GetById(long id);
    }
}
