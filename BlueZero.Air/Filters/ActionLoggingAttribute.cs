using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air
{
    public class ActionLoggingAttribute : ActionFilterAttribute
    {
        private const string StopwatchKey = "DebugLoggingStopWatch";

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ActionLoggingAttribute));

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (log.IsDebugEnabled)
            {
                var loggingWatch = Stopwatch.StartNew();

                filterContext.HttpContext.Items.Add(StopwatchKey, loggingWatch);

                var message = new StringBuilder();
                
                message.Append(String.Format("Executing controller {0}, action {1}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName));

                if (filterContext.ActionParameters.Count > 0)
                {
                    message.Append(" with route values");

                    foreach (var parameter in filterContext.ActionParameters)
                    {
                        message.AppendFormat(" {0} = {1},", parameter.Key, (parameter.Value != null ? parameter.Value : "NULL"));
                    }
                }

                log.Debug(message);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (log.IsDebugEnabled)
            {
                if (filterContext.HttpContext.Items[StopwatchKey] != null)
                {
                    var loggingWatch = (Stopwatch)filterContext.HttpContext.Items[StopwatchKey];

                    loggingWatch.Stop();

                    long timeSpent = loggingWatch.ElapsedMilliseconds;

                    var message = new StringBuilder();

                    message.Append(String.Format("Finished executing controller {0}, action {1} - time spent {2}",
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        filterContext.ActionDescriptor.ActionName,
                        timeSpent));

                    log.Debug(message);

                    filterContext.HttpContext.Items.Remove(StopwatchKey);
                }
            }
        }
    }
}