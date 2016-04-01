using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BlueZero.Air.Models;

namespace BlueZero.Air
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "SOME CRAP",
            //    clientSecret: "SOME MORE CRAP");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "SOME CRAP",
            //    consumerSecret: "SOME MORE CRAP");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "484760114882346",
                appSecret: "ed4f46c7a1a9c3c9580000d424f67574");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
