using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IMedicineService : IDisposable
    {
        List<Medicine> GetForChild(long id);

        List<Medicine> GetForChildByDate(long id, DateTime date);

        Medicine GetById(long id);
    }
}
