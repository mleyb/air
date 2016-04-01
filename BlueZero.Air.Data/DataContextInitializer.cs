using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using BlueZero.Air.Data.Models;
using log4net;
using WebMatrix.WebData;

namespace BlueZero.Air.Data
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>    
    //public class DataContextInitializer : DropCreateDatabaseAlways<DataContext>    
    {
        protected override void Seed(DataContext context)
        {
            DatabaseSeeder.Seed(context, new RandomDataGenerator());
        }
    }
}
