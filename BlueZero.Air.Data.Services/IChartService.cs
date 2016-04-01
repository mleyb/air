using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data.Services
{
    public interface IChartService : IDisposable
    {
        Task<string> GeneratePieChart();

        Task<string> GenerateBarChart();

        Task<string> GenerateLineChart();
    }
}
