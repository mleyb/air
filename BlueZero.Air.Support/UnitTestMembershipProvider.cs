using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace BlueZero.Air.Support
{
    public class UnitTestMembershipProvider : IMembershipProvider
    {
        public MembershipUser CreateUser(string username, string password, string email, out Guid providerUserKey, out MembershipCreateStatus status)
        {
            providerUserKey = Guid.NewGuid();

            status = MembershipCreateStatus.Success;

            return new FakeMembershipUser();
        }
    }

    public class FakeMembershipUser : MembershipUser
    {
    }
}
