using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace BlueZero.Air
{
    public static class HttpMessageExtension
    {
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            try
            {
                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];

                    return prop.Address;
                }
            }
            catch { }
            
            return null;
        }
    }
}