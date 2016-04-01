﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air.Filters
{
    public class SetTempDataModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            filterContext.Controller.TempData["ModelState"] = filterContext.Controller.ViewData.ModelState;
        }
    }
}