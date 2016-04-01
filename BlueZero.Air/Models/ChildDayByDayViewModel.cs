using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class ChildDayByDayViewModel
    {
        public long Id { get; set; }

        public string DateString { get; set; }

        public List<DayEntry> Entries = new List<DayEntry>();
    }
}