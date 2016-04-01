using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface ISnackService : IDisposable
    {
        List<Snack> GetForChild(long id);

        List<Snack> GetForChildByDate(long id, DateTime date);

        Snack GetById(long id);
    }
}
