using BlueZero.Air.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using log4net;

namespace BlueZero.Air
{
    public class HttpNotFoundAwareApiControllerActionSelector : ApiControllerActionSelector
    {
        private ILog _log;

        public HttpNotFoundAwareApiControllerActionSelector(ILog log)
        {
            _log = log;
        }

        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;

            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;

                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                {
                    throw;
                }

                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Handle404";

                IHttpController httpController = new ErrorController();

                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());

                decriptor = base.SelectAction(controllerContext);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Exception whilst trying to select controller action for request with URI '{0}': {1}", controllerContext.Request.RequestUri, e);

                throw;
            }

            return decriptor;
        }
    }
}