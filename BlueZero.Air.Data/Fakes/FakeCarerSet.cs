using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data.Fakes
{
    public class FakeCarerSet : FakeDbSet<Carer>
    {
        public override Carer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.Id == (string)keyValues.Single());
        }
    }
}
