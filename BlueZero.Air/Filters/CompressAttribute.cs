using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CompressAttribute : FilterAttribute, IResultFilter
    {
        public CompressAttribute()
        {
            Order = Int32.MaxValue;
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Do nothing just sleep
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            if (filterContext.Canceled || (filterContext.Exception != null && !filterContext.ExceptionHandled))
            {
                return;
            }

            filterContext.HttpContext.Compress();
        }
    }
}