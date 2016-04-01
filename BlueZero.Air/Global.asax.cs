using BlueZero.Air.Data;
using BlueZero.Air.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using StackExchange.Profiling;
using System.Data.Entity.Infrastructure;
using NMALib;
using System.Net;

namespace BlueZero.Air
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog _log;

        private static readonly List<string> headersToRemove = new List<string> { "Server", "X-AspNet-Version", "X-Powered-By" };

        protected void Application_Start()
        {
            PreSendRequestHeaders += (sender, eventArgs) => headersToRemove.ForEach(header => this.Response.Headers.Remove(header));

            MvcHandler.DisableMvcResponseHeader = true;

            log4net.Config.XmlConfigurator.Configure();

            var x = log4net.LogManager.GetCurrentLoggers();

            _log = LogManager.GetLogger(typeof(MvcApplication));

            _log.Info("");
            _log.Info("");
            _log.Info("");
            _log.Info("");
            _log.Info("");
            _log.Info("*** Application starting ***");
            _log.Info("");
            _log.Info("");
            _log.Info("");
            _log.Info("");
            _log.Info("");

            AreaRegistration.RegisterAllAreas();

            // intitialise SignalR hubs
            RouteTable.Routes.MapHubs();            
            
            ResolverConfig.RegisterDependencyResolver(GlobalConfiguration.Configuration);
            TraceConfig.RegisterTraceWriter(GlobalConfiguration.Configuration);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultApiControllerSelector(GlobalConfiguration.Configuration, DependencyResolver.Current.GetService<ILog>()));
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareApiControllerActionSelector(DependencyResolver.Current.GetService<ILog>()));

            DatabaseConfig.InitializeDatabase();
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }        

        protected void Application_Error(Object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            if (ex != null)
            {
                if (ex is HttpRequestValidationException)
                {
                    _log.ErrorFormat("Http Request Validation Application error: {0}", ex.ToString());

                    Server.ClearError();
                    Response.Clear();
                    Response.StatusCode = 200;

                    // notify
                    NMANotifier.SendNotification("Application Error", "HttpRequestValidationError occurred", NMANotificationPriority.High);

                    Response.Redirect("~/account/login", true);
                }
                else
                {
                    _log.ErrorFormat("Application error: {0}", ex.ToString());

                    Server.ClearError();
                    Response.Clear();
                    Response.StatusCode = 500;

                    Response.End();                 
                }
            }
        }
    }    
}