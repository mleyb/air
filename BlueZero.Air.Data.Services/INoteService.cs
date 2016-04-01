using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface INoteService : IDisposable
    {
        List<Note> GetForChild(long id);

        List<Note> GetForChildByDate(long id, DateTime date);

        Note GetById(long id);
    }
}
