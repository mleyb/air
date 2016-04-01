using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Models
{
    public class Parent
    {
        public long Id { get; set; }

        // Navigation properties
        public UserProfile UserProfile { get; set; }
        public ICollection<Child> Children { get; set; }
    }
}
