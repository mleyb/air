using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace BlueZero.Air
{
    public class TraceConfig
    {
        public static void RegisterTraceWriter(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(ITraceWriter), new Log4NetTraceWriter());
        }
    }
}