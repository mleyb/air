using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BlueZero.Air.Data.Models;

namespace BlueZero.Air.Data.Fakes
{
    public class FakeDataContext : IDataContext
    {
        public FakeDataContext()
        {
            Notes = new FakeNoteSet();
            Bottles = new FakeBottleSet();
            Snacks = new FakeSnackSet();
            Children = new FakeChildSet();
            Meals = new FakeMealSet();
            Nappies = new FakeNappySet();
            Medicines = new FakeMedicineSet();
            Sleeps = new FakeSleepSet();
            Carers = new FakeCarerSet();
            Milestones = new FakeMilestoneSet();
            Activities = new FakeActivitySet();
            Drinks = new FakeDrinkSet();
            FirstAids = new FakeFirstAidSet();
            Sicks = new FakeSickSet();
            UserProfiles = new FakeUserProfileSet();
            GCMRegistrations = new FakeGCMRegistrationSet();
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

        public Database Database { get { return null; } }

        void IDataContext.SetModified(object entity)
        {
        }

        int IDataContext.SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
