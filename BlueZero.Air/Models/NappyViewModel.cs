using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class NappyViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Nappy";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [UIHint("YesNo")]
        public bool Dirty { get; set; }

        [Display(Name = "Details")]
        public string Detail { get; set; }        
    }
}