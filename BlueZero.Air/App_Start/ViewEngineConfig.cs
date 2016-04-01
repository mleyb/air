using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air
{
    public static class ViewEngineConfig
    {
        public static void ConfigureViewEngines()
        {
            var viewEngines = ViewEngines.Engines;

            var webFormsEngine = viewEngines.OfType<WebFormViewEngine>().FirstOrDefault();

            if (webFormsEngine != null)
            {
                // remove the web forms engine - we don't need it
                viewEngines.Remove(webFormsEngine);
            }
        }
    }
}