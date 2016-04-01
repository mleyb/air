using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class MilestoneViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Milestone";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Details")]
        public string Detail { get; set; }        
    }
}