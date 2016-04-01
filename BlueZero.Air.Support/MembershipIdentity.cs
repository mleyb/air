using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace BlueZero.Air.Support
{
    public class MembershipIdentity : GenericIdentity
    {
        public string Email { get; private set; }

        public Guid ProviderUserKey { get; private set; }

        public MembershipIdentity(string name, string email, Guid providerUserKey) : base(name)
        {
            Email = email;
            ProviderUserKey = providerUserKey;
        }
    }
}
