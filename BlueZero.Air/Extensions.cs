using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace BlueZero.Air
{
    public static class Extensions
    {
        public static string DisplayName<TEnum>(this TEnum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var name = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                name = ((DisplayAttribute)attrs[0]).GetName();
            }

            return name;
        }

        public static bool IsLocal(this HttpRequestMessage request)
        {
            var localFlag = request.Properties["MS_IsLocal"] as Lazy<bool>;

            return (localFlag != null && localFlag.Value);
        }

        public static void Compress(this HttpContextBase instance)
        {
            HttpRequestBase httpRequest = instance.Request;

            // IE6
            if ((httpRequest.Browser.MajorVersion < 7) && httpRequest.Browser.IsBrowser("IE"))
            {
                return;
            }

            string acceptEncoding = httpRequest.Headers["Accept-Encoding"];

            if (String.IsNullOrEmpty(acceptEncoding))
            {
                return;
            }

            string preferredEncoding = acceptEncoding.Split(',').First();

            HttpResponseBase httpResponse = instance.Response;

            Action<string> compress = encoding =>
            {
                if (!String.IsNullOrEmpty(httpResponse.RedirectLocation))
                {
                    return;
                }

                try
                {
                    httpResponse.AppendHeader("Content-encoding", encoding);
                    httpResponse.Filter = encoding.Equals("deflate") ?
                                          new DeflateStream(httpResponse.Filter, CompressionMode.Compress) :
                                          new GZipStream(httpResponse.Filter, CompressionMode.Compress) as Stream;
                }
                catch (HttpException)
                {
                }
            };

            if (preferredEncoding.Equals("*", StringComparison.Ordinal) || preferredEncoding.Equals("gzip", StringComparison.OrdinalIgnoreCase))
            {
                compress("gzip");
            }
            else if (preferredEncoding.Equals("deflate", StringComparison.OrdinalIgnoreCase))
            {
                compress("deflate");
            }
        }
    }
}