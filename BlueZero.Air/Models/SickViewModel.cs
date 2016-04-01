using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Models
{
    public class SickViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Sick";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Details")]
        public string Detail { get; set; }

        [UIHint("YesNo")]
        public bool DoctorRecommended { get; set; }
    }
}
