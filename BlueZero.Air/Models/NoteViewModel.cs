using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class NoteViewModel
    {
        [Display(Name = "Title")]
        public readonly string Title = "Note";

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Display(Name = "Details")]
        public string Detail { get; set; }        
    }
}