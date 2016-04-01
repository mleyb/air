using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IMilestoneService : IDisposable
    {
        List<Milestone> GetForChild(long id);

        List<Milestone> GetForChildByDate(long id, DateTime date);

        Milestone GetById(long id);
    }
}
