using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air.Filters
{
    public class LogClientIPAddressAttribute : ActionFilterAttribute
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(LogClientIPAddressAttribute));

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ipAddress = filterContext.HttpContext.Request.UserHostAddress;

            _log.InfoFormat("Host {0} attempting {1}",                            
                            filterContext.HttpContext.Request.UserHostAddress,
                            filterContext.HttpContext.Request.Url.PathAndQuery);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _log.InfoFormat("Host {0} completed {1}",
                            filterContext.HttpContext.Request.UserHostAddress,
                            filterContext.HttpContext.Request.Url.PathAndQuery);
        }
    }
}

