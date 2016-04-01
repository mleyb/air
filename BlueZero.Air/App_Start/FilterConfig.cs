using BlueZero.Air.Filters;
using GoogleAnalyticsTracker.Web.Mvc;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new HttpHeaderAttribute("X-Frame-Options", "deny"));
            filters.Add(new HttpHeaderAttribute("X-Content-Type-Options", "nosniff"));            
            filters.Add(new LogClientIPAddressAttribute());
            filters.Add(new ActionLoggingAttribute());
            //filters.Add(new ActionTrackingAttribute(Constants.GAUrchin, Constants.GADomain, a => true));            
            filters.Add(new HandleErrorAttribute());            
        }
    }
}