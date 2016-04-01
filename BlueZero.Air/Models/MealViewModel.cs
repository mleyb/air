using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class MealViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Meal";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string Type { get; set; }

        [Display(Name = "Amount (%)")]
        public int AmountConsumed { get; set; }
    }
}