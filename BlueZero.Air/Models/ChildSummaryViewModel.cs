using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class ChildSummaryViewModel
    {
        [Required]
        public long Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}