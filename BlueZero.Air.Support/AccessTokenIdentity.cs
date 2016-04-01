using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Support
{
    [Serializable]
    public class AccessTokenIdentity : GenericIdentity
    {
        public string Id { get; private set; }

        public string Email { get; private set; }

        public AccessTokenIdentity(string id, string name, string email) : base(name)
        {
            Id = id;
            Email = email;
        }
    }
}
