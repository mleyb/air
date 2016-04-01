using BlueZero.Air.Data.Services;
using BlueZero.Air.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Net.Http;

namespace BlueZero.Air.Controllers
{
    [Authorize(Roles = "Parent, Administrator")]
    public class ChartController : ControllerBase
    {
        private IChartService _chartService;

        public ChartController(ILog log, IChartService chartService) : base(log)
        {
            _chartService = chartService;
        }

        public ActionResult Index()
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                .SetXAxis(new DotNet.Highcharts.Options.XAxis
                {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new DotNet.Highcharts.Options.Series
                {
                    Data = new DotNet.Highcharts.Helpers.Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                });

            return PartialView("_ChartsPartial", chart);
        }

        //[HttpPost]
        //public async Task<string> GenerateChart(long id, ChartType type)
        //{
        //    string url = null;

        //    switch (type)
        //    {
        //        case ChartType.PieChart:
        //            url = await _chartService.GeneratePieChart();
        //            break;

        //        case ChartType.BarChart:
        //            url = await _chartService.GenerateBarChart();
        //            break;

        //        case ChartType.LineChart:
        //            url = await _chartService.GenerateLineChart();
        //            break;
        //    }

        //    return url;

            //var client = new HttpClient();

            //HttpResponseMessage response = await client.GetAsync(url);

            //response.EnsureSuccessStatusCode(); 

            //byte[] imageData = await response.Content.ReadAsByteArrayAsync();

            //return base.File(imageData, "image/png");       
        //}
    }
}