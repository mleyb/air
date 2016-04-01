using BlueZero.Air.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BlueZero.Air.Data.Services
{
    public class ParentService : DataService, IParentService
    {
        public ParentService(IDataContext db) : base(db) { }

        public Parent GetByUserId(int id)
        {
            return _db.Parents.Where(p => p.UserProfile.UserId == id).Include(p => p.Children).Single();
        }

        public bool TryGetByUserId(int id, out Parent parent)
        {
            parent = _db.Parents.Where(p => p.UserProfile.UserId == id).Include(p => p.Children).SingleOrDefault();

            return (parent != null);
        }

        public Parent GetParentForChild(long childId)
        {
            return _db.Parents.Where(p => p.Children.Any(c => c.Id == childId)).Include(p => p.UserProfile).SingleOrDefault();
        }
    }
}
