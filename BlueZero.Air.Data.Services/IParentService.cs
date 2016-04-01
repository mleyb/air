using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IParentService : IDisposable
    {
        Parent GetByUserId(int id);

        bool TryGetByUserId(int id, out Parent parent);

        Parent GetParentForChild(long childId);
    }
}
