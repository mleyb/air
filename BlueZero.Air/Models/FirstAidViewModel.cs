using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BlueZero.Air.Models
{
    public class FirstAidViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "First Aid";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }        

        [Display(Name = "Details")]
        public string Detail { get; set; }        

        [UIHint("YesNo")]
        public bool DoctorRecommended { get; set; }
    }
}
