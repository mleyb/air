using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IActivityService : IDisposable
    {
        List<Activity> GetForChild(long id);

        List<Activity> GetForChildByDate(long id, DateTime date);

        Activity GetById(long id);
    }
}
