using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueZero.Air.Support
{
    public interface IRoleProvider
    {
        void CreateRole(string roleName);

        void AddUserToRole(string username, string roleName);

        bool RoleExists(string roleName);

        bool IsUserInRole(string username, string roleName);
    }
}
