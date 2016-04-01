using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using log4net;

namespace BlueZero.Air
{
    public class HttpNotFoundAwareDefaultApiControllerSelector : DefaultHttpControllerSelector
    {
        private ILog _log;

        public HttpNotFoundAwareDefaultApiControllerSelector(HttpConfiguration configuration, ILog log) : base(configuration)
        {
            _log = log;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;

            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;

                if (code != HttpStatusCode.NotFound)
                {
                    throw;
                }

                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "Error";
                routeValues["action"] = "Handle404";

                decriptor = base.SelectController(request);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Exception whilst trying to select controller for request with URI '{0}': {1}", request.RequestUri, e);

                throw;
            }

            return decriptor;
        }
    }
}