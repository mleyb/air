using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Models
{
    public class GCMRegistration
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string RegistrationId { get; set; }

        // Navigation properties
        public Carer Carer { get; set; }
        public Parent Parent { get; set; }
    }
}
