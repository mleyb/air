using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace BlueZero.Air.Data
{
    public class SimpleMembershipInitializer
    {
        public void Initialize(IDataContext context)
        {
            try
            {
                if (!context.Database.Exists())
                {
                    // Create the SimpleMembership database without Entity Framework migration schema
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                }

                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection(
                        "DefaultConnection",
                        "UserProfile",
                        "UserId",
                        "UserName",
                        autoCreateTables: true);
                }

                SeedRoles();
                SeedUsers();
                AddUsersToRoles();              
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }

        private void SeedRoles()
        {
            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!Roles.RoleExists("Parent"))
                Roles.CreateRole("Parent");

            if (!Roles.RoleExists("Carer"))
                Roles.CreateRole("Carer");
        }

        private void SeedUsers()
        {
            if (!WebSecurity.UserExists("mleybourne1@gmail.com"))
                WebSecurity.CreateUserAndAccount(
                    "mleybourne1@gmail.com",
                    "Gargoyle56");

            if (!WebSecurity.UserExists("parent@mailinator.com"))
                WebSecurity.CreateUserAndAccount(
                    "parent@mailinator.com",
                    "Gargoyle56");
        }

        private void AddUsersToRoles()
        {
            if (!Roles.GetRolesForUser("mleybourne1@gmail.com").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "mleybourne1@gmail.com" }, new[] { "Administrator" });

            if (!Roles.GetRolesForUser("parent@mailinator.com").Contains("Parent"))
                Roles.AddUsersToRoles(new[] { "parent@mailinator.com" }, new[] { "Parent" });
        }       
    }
}
