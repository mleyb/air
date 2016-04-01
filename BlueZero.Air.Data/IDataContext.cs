using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data
{
    public interface IDataContext : IDisposable
    {
        IDbSet<Bottle> Bottles { get; set; }
        IDbSet<Carer> Carers { get; set; }
        IDbSet<Child> Children { get; set; }
        IDbSet<Activity> Activities { get; set; }
        IDbSet<Meal> Meals { get; set; }
        IDbSet<Medicine> Medicines { get; set; }
        IDbSet<Milestone> Milestones { get; set; }
        IDbSet<Nappy> Nappies { get; set; }
        IDbSet<Note> Notes { get; set; }
        IDbSet<Sleep> Sleeps { get; set; }
        IDbSet<Snack> Snacks { get; set; }
        IDbSet<Drink> Drinks { get; set; }
        IDbSet<FirstAid> FirstAids { get; set; }
        IDbSet<Sick> Sicks { get; set; }
        IDbSet<Parent> Parents { get; set; }
        
        IDbSet<UserProfile> UserProfiles { get; set; }

        IDbSet<GCMRegistration> GCMRegistrations { get; set; }

        Database Database { get; }

        

        void SetModified(object entity);
        int SaveChanges();
    }
}
