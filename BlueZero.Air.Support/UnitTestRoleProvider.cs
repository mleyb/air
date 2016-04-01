using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueZero.Air.Support
{
    public class UnitTestRoleProvider : IRoleProvider
    {
        public void CreateRole(string roleName)
        {
        }

        public void AddUserToRole(string username, string roleName)
        {
        }

        public bool RoleExists(string roleName)
        {
            return true;
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return true;
        }              
    }
}
