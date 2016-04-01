using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueZero.Air.Data.Models;
using log4net;

namespace BlueZero.Air.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(DataContext context, IRandomDataGenerator randomDataGenerator)
        {
            if (context.Carers.Find("9823542345979") == null)
            {
                LogManager.GetLogger(typeof(DataContextInitializer)).Debug("Seeding database...");

                // carers & children

                var carer = new Carer { Id = "9823542345979", DateCreated = DateTime.UtcNow, Key = "ABC123", Name = "Lisa Jones" };

                var children = new List<Child>
                {
                    // parentuser
                    new Child { DateCreated = DateTime.UtcNow, Key = randomDataGenerator.GenerateString(Child.KeyLength), Forename = "John", Surname = "Smith", DateOfBirth = DateTime.Now, ParentContactUri = "http://test/1", RegistrationCode = "ABC" },
                    new Child { DateCreated = DateTime.UtcNow, Key = randomDataGenerator.GenerateString(Child.KeyLength), Forename = "Bob", Surname = "Onion", DateOfBirth = DateTime.Now, ParentContactUri = "http://test/2", RegistrationCode = "DEF" },
                    new Child { DateCreated = DateTime.UtcNow, Key = randomDataGenerator.GenerateString(Child.KeyLength), Forename = "Sausage", Surname = "Dog", DateOfBirth = DateTime.Now, ParentContactUri = "http://test/2", RegistrationCode = "GHI" },
                    
                    // no parent
                    new Child { DateCreated = DateTime.UtcNow, Key = randomDataGenerator.GenerateString(Child.KeyLength), Forename = "Long", Surname = "Trousers", DateOfBirth = DateTime.Now, ParentContactUri = "http://test/2", RegistrationCode = "JKL" },
                    new Child { DateCreated = DateTime.UtcNow, Key = randomDataGenerator.GenerateString(Child.KeyLength), Forename = "Long", Surname = "Trousers", DateOfBirth = DateTime.Now, ParentContactUri = "http://test/2", RegistrationCode = "MNO" }
                };

                carer.Children = children;

                context.Carers.Add(carer);

                children.ForEach(c => context.Children.Add(c));

                context.SaveChanges();

                // parents

                var parent = new Parent
                {
                    Children = new List<Child> { children[0], children[1], children[3] }
                };

                context.Parents.Add(parent);

                context.SaveChanges();

                // bottles

                var bottles = new List<Bottle>
                {
                    new Bottle { Date = DateTime.UtcNow, Amount = 100, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow, Amount = 75, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow, Amount = 50, Child = children[1] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-1), Amount = 100, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-1), Amount = 75, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-1), Amount = 50, Child = children[1] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 100, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 75, Child = children[0] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 50, Child = children[1] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 100, Child = children[1] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 75, Child = children[1] },
                    new Bottle { Date = DateTime.UtcNow.AddDays(-2), Amount = 50, Child = children[2] }
                };

                bottles.ForEach(b => context.Bottles.Add(b));

                context.SaveChanges();

                // notes

                var notes = new List<Note>()            
                {
                    new Note { Date = DateTime.UtcNow, Detail = "A note", Child = children[0] },
                    new Note { Date = DateTime.UtcNow, Detail = "Another note", Child = children[1] },
                    new Note { Date = DateTime.UtcNow.AddDays(-1), Detail = "A note", Child = children[0] },
                    new Note { Date = DateTime.UtcNow.AddDays(-1), Detail = "Another note", Child = children[1] },
                    new Note { Date = DateTime.UtcNow.AddDays(-2), Detail = "A note", Child = children[0] },
                    new Note { Date = DateTime.UtcNow.AddDays(-2), Detail = "Another note", Child = children[1] }
                };

                notes.ForEach(n => context.Notes.Add(n));

                context.SaveChanges();

                // nappies

                var nappies = new List<Nappy>
                {
                    new Nappy { Date = DateTime.UtcNow, Dirty =  true, Child = children[0] },
                    new Nappy { Date = DateTime.UtcNow, Dirty =  false, Child = children[1] },
                    new Nappy { Date = DateTime.UtcNow, Dirty =  true, Child = children[1] },
                    new Nappy { Date = DateTime.UtcNow.AddDays(-1), Dirty =  true, Child = children[0] },
                    new Nappy { Date = DateTime.UtcNow.AddDays(-1), Dirty =  false, Child = children[1] },
                    new Nappy { Date = DateTime.UtcNow.AddDays(-1), Dirty =  true, Child = children[1] }
                };

                nappies.ForEach(n => context.Nappies.Add(n));

                context.SaveChanges();

                // meals

                var meals = new List<Meal>
                {
                    new Meal { Date = DateTime.UtcNow, Description = "Some pies", Child = children[0] },
                    new Meal { Date = DateTime.UtcNow, Description = "Some eggs", Child = children[1] },
                    new Meal { Date = DateTime.UtcNow.AddDays(-1), Description = "Some pies", Child = children[0] },
                    new Meal { Date = DateTime.UtcNow.AddDays(-1), Description = "Some eggs", Child = children[1] }
                };

                meals.ForEach(m => context.Meals.Add(m));

                context.SaveChanges();

                LogManager.GetLogger(typeof(DataContextInitializer)).Debug("Database seeded.");
            }
            else
            {
                LogManager.GetLogger(typeof(DataContextInitializer)).Debug("Database already seeded.");
            }
        }
    }
}
