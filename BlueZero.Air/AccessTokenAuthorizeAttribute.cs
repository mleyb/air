using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac.Core;
using BlueZero.Air.GoogleApi;
using BlueZero.Air.Support;
using log4net;
using Newtonsoft.Json.Linq;

namespace BlueZero.Air
{
    public class AccessTokenAuthorizeAttribute : AuthorizeAttribute
    {
        private const string TokenAuthAuthResponseHeaderValue = "Bearer";

        private ILog _log = LogManager.GetLogger(typeof(AccessTokenAuthorizeAttribute));

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizationDisabled(actionContext) || AuthorizeRequest(actionContext.ControllerContext.Request))
            {
                _log.Debug("Authorization successful");

                return;
            }
            else
            {
                _log.Debug("Authorization failed");

                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = CreateUnauthorizedResponse(actionContext.ControllerContext.Request);
        }

        private HttpResponseMessage CreateUnauthorizedResponse(HttpRequestMessage request)
        {
            var result = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                RequestMessage = request
            };

            return result;
        }

        private static bool AuthorizationDisabled(HttpActionContext actionContext)
        {
            // support new AllowAnonymousAttribute
            if (!actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            }
            else
            {
                return true;
            }
        }

        private bool AuthorizeRequest(HttpRequestMessage request)
        {
            try
            {
                AuthenticationHeaderValue authValue = request.Headers.Authorization;

                if (authValue == null ||
                    String.IsNullOrWhiteSpace(authValue.Parameter) ||
                    String.IsNullOrWhiteSpace(authValue.Scheme) ||
                    authValue.Scheme != TokenAuthAuthResponseHeaderValue)
                {
                    _log.Error("Authentication header malformed or missing");

                    return false;
                }
                else
                {
                    _log.InfoFormat("Received request with bearer token '{0}'", authValue.Parameter);

                    IPrincipal principal = null;

                    if (TryCreatePrincipal(authValue.Parameter, out principal))
                    {
                        // set the principal
                        HttpContext.Current.User = Thread.CurrentPrincipal = principal;

                        _log.DebugFormat("Received valid token for principal with name {0}", principal.Identity.Name);

                        return CheckRoles(principal) && CheckUsers(principal);
                    }
                    else
                    {
                        _log.ErrorFormat("Received invalid token - {0}", authValue.Parameter);

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Authorize failure with exception: {0}", ex.ToString());

                return false;
            }
        }

        private bool CheckUsers(IPrincipal principal)
        {
            string[] users = UsersSplit;

            if (users.Length == 0)
            {
                return true;
            }

            // NOTE: This is a case sensitive comparison
            return users.Any(u => principal.Identity.Name == u);
        }

        private bool CheckRoles(IPrincipal principal)
        {
            string[] roles = RolesSplit;

            if (roles.Length == 0)
            {
                return true;
            }

            return roles.Any(principal.IsInRole);
        }

        protected string[] RolesSplit
        {
            get { return SplitStrings(Roles); }
        }

        protected string[] UsersSplit
        {
            get { return SplitStrings(Users); }
        }

        protected static string[] SplitStrings(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new string[0];
            }

            var result = input.Split(',').Where(s => !String.IsNullOrWhiteSpace(s.Trim()));

            return result.Select(s => s.Trim()).ToArray();
        }

        private bool TryCreatePrincipal(string accessToken, out IPrincipal principal)
        {
            principal = null;

            var httpClient = new GoogleHttpClient();

            JObject userInfo;
                
            bool success = httpClient.TryGetUserInfo(accessToken, out userInfo);

            if (success)
            {
                _log.DebugFormat("Authentication attempt by Google identity: {0}", userInfo.ToString());

                string[] roles = new string[] { "Carer" };

                principal = new GenericPrincipal(new AccessTokenIdentity(userInfo.Value<string>("id"), userInfo.Value<string>("name"), userInfo.Value<string>("email")), roles);
            }

            return success;
        }


/*

        2012-08-31 08:45:50,892 [50] DEBUG BlueZero.Isis.Api.AccessTokenAuthorizeAttribute [(null)] - {
  "id": "102430956086763645040",
  "email": "mleybourne1@gmail.com",
  "verified_email": true,
  "name": "Mark Leybourne",
  "given_name": "Mark",
  "family_name": "Leybourne",
  "link": "https://plus.google.com/102430956086763645040",
  "picture": "https://lh3.googleusercontent.com/-HB9nEDI9u9Q/AAAAAAAAAAI/AAAAAAAAEQY/BvBZzVArX-I/photo.jpg",
  "gender": "male",
  "locale": "en-GB"
}*/

    }
}