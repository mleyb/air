using BlueZero.Air.Data;
using BlueZero.Air.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace BlueZero.Air
{
    public class DatabaseConfig
    {        
        public static void InitializeDatabase()
        {            
            using (var context = new DataContext())         
            {
                Database.SetInitializer(new DataContextInitializer());
                context.Database.Initialize(true);
                
                //new MigrateDatabaseToLatestVersion<DataContext, MigrationConfiguration>().InitializeDatabase(context);

                // initialise membership tables & data if necessary
                new SimpleMembershipInitializer().Initialize(context);

                // need to explicitly map existing test data parents to their user profiles, as we've not created 
                // them via the registration process. This needs to be done AFTER the simple membership data has
                // been initialised
                UpdateTestDataUserProfileAssociations(context);
            }
        }

        private static void UpdateTestDataUserProfileAssociations(IDataContext context)
        {
            var parent = context.Parents.First();

            if (parent.UserProfile == null)
            {
                parent.UserProfile = context.UserProfiles.Find(WebSecurity.GetUserId("parent@mailinator.com"));
            }

            context.SaveChanges();
        }
    }
}