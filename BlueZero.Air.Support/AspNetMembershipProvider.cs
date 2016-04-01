using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BlueZero.Air.Support
{
    public class AspNetMembershipProvider : IMembershipProvider
    {
        public MembershipUser CreateUser(string username, string password, string email, out Guid providerUserKey, out MembershipCreateStatus status)
        {
            providerUserKey = Guid.Empty;

            MembershipUser user = Membership.CreateUser(username, password, email, null, null, true, null, out status);

            if (status == MembershipCreateStatus.Success)
            {
                providerUserKey = Guid.Parse(user.ProviderUserKey.ToString());
            }

            return user;
        }
    }
}