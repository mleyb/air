using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BlueZero.Air.Support
{
    public interface IMembershipProvider
    {
        MembershipUser CreateUser(string username, string password, string email, out Guid providerUserKey, out MembershipCreateStatus status);
    }
}