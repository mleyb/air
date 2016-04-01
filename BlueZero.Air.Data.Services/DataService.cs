﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public abstract class DataService : IDisposable
    {
        protected IDataContext _db;

        public DataService(IDataContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
