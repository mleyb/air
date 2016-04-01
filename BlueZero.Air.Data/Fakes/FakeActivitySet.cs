using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data.Fakes
{
    public class FakeActivitySet : FakeDbSet<Activity>
    {
        public override Activity Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.Id == (int)keyValues.Single());
        }
    }
}
