using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace BlueZero.Air.Support
{
    public class AspNetRoleProvider : IRoleProvider
    {
        public void CreateRole(string roleName)
        {
            Roles.CreateRole(roleName);
        }

        public void AddUserToRole(string username, string roleName)
        {
            Roles.AddUserToRole(username, roleName);
        }        

        public bool RoleExists(string roleName)
        {
            return (Roles.GetAllRoles().Contains(roleName));
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return Roles.IsUserInRole(username, roleName);
        }                
    }
}
