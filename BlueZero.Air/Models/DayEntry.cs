using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class DayEntry
    {
        public long Id { get; set; }

        [EnumDataType(typeof(EntryType))]
        [Display(Name = "Entry Type")]
        public EntryType Type { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime DateTime { get; set; }
    }

    public enum EntryType
    {
        [Display(Name = "Activity")]   
        Activity = 0,

        [Display(Name = "Bottle")]   
        Bottle,

        [Display(Name = "Drink")]   
        Drink,

        [Display(Name = "FirstAid")]   
        FirstAid,

        [Display(Name = "Meal")]   
        Meal,

        [Display(Name = "Medicine")]   
        Medicine,

        [Display(Name = "Milestone")]   
        Milestone,

        [Display(Name = "Nappy")]   
        Nappy,

        [Display(Name = "Note")]   
        Note,

        [Display(Name = "Sick")]   
        Sick,

        [Display(Name = "Sleep")]   
        Sleep,

        [Display(Name = "Snack")]   
        Snack
    }
}