using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebMatrix.WebData;

namespace BlueZero.Air
{
    public static class IPrincipalExtension
    {
        public static int GetUserId(this IPrincipal principal)
        {
            return WebSecurity.GetUserId(principal.Identity.Name);
        }
    }
}