using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IFirstAidService : IDisposable
    {
        List<FirstAid> GetForChild(long id);

        List<FirstAid> GetForChildByDate(long id, DateTime date);

        FirstAid GetById(long id);
    }
}
