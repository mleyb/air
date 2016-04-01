using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BlueZero.Air.Models
{
    public class DrinkViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Drink";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]        
        public string Description { get; set; }

        [Display(Name = "Amount")]        
        public decimal Amount { get; set; }

        [Display(Name = "Unit")]
        public int Unit { get; set; }
    }
}
