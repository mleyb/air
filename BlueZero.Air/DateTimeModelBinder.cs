using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air
{
    public class DateTimeModelBinder : IModelBinder
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var dateString = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (String.IsNullOrEmpty(dateString))
                return null;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));
            try
            {
                DateTime parsedDate;

                if (!DateTime.TryParse(dateString, out parsedDate))
                {
                    log.ErrorFormat("Date string '{0}' could not be parsed.", dateString);
                }
                else
                {
                    return parsedDate;
                }
            }
            catch (Exception)
            {
                // Fall through
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format("\"{0}\" is invalid.", bindingContext.ModelName));
            return null;
        }
    }
}