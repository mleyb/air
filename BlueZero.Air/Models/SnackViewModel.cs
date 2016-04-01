using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class SnackViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Snack";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Amount (%)")]
        public int AmountConsumed { get; set; }
    }
}