using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface INappyService : IDisposable
    {
        List<Nappy> GetForChild(long id);

        List<Nappy> GetForChildByDate(long id, DateTime date);

        Nappy GetById(long id);
    }
}
