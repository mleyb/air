using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data.Fakes
{
    public class FakeUserProfileSet : FakeDbSet<UserProfile>
    {
        public override UserProfile Find(params object[] keyValues)
        {
            return this.SingleOrDefault(x => x.UserId == (int)keyValues.Single());
        }
    }
}
