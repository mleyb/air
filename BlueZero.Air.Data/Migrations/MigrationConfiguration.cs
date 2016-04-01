using BlueZero.Air.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WebMatrix.WebData;

namespace BlueZero.Air.Data.Migrations
{
    public class MigrationConfiguration : DbMigrationsConfiguration<DataContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            DatabaseSeeder.Seed(context, new RandomDataGenerator());
        }
    }
}
