using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlueZero.Air.Models
{
    public class ChildChartViewModel
    {
        public long Id { get; set; }

        public ChartType Type { get; set; }
    }

    public enum ChartType
    {
        [Display(Name = "Pie Chart")]
        PieChart = 0,

        [Display(Name = "Bar Chart")]
        BarChart,

        [Display(Name = "Line Chart")]
        LineChart
    }
}