using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data
{
    public class DataContext : DbContext, IDataContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Isis.Models.IsisContext>());

        public DataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public IDbSet<Note> Notes { get; set; }

        public IDbSet<Bottle> Bottles { get; set; }

        public IDbSet<Snack> Snacks { get; set; }

        public IDbSet<Child> Children { get; set; }

        public IDbSet<Meal> Meals { get; set; }

        public IDbSet<Nappy> Nappies { get; set; }

        public IDbSet<Medicine> Medicines { get; set; }

        public IDbSet<Sleep> Sleeps { get; set; }

        public IDbSet<Carer> Carers { get; set; }

        public IDbSet<Milestone> Milestones { get; set; }

        public IDbSet<Activity> Activities { get; set; }

        public IDbSet<Drink> Drinks { get; set; }

        public IDbSet<FirstAid> FirstAids { get; set; }

        public IDbSet<Sick> Sicks { get; set; }

        public IDbSet<Parent> Parents { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<GCMRegistration> GCMRegistrations { get; set; }

        public new Database Database { get { return base.Database; } }

        void IDataContext.SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        int IDataContext.SaveChanges()
        {
            return SaveChanges();
        }
    }
}
