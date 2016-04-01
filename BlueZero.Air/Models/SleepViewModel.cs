using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class SleepViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Sleep";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        public DateTime End { get; set; }

        [Display(Name = "Duration")]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }
    }
}