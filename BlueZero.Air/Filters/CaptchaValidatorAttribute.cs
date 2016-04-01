using Recaptcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueZero.Air.Filters
{
    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];
            var captchaValidtor = new RecaptchaValidator
                                      {
                                          PrivateKey = "6LcUf9kSAAAAAEfGX4DTlj7SVwu93s88L1YitIb9",
                                          RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                                          Challenge = captchaChallengeValue,
                                          Response = captchaResponseValue
                                      };

            var recaptchaResponse = captchaValidtor.Validate();

            filterContext.ActionParameters["isCaptchaValid"] = recaptchaResponse.IsValid;

            if (!recaptchaResponse.IsValid)
            {
                filterContext.ActionParameters["captchaErrorMessage"] = recaptchaResponse.ErrorMessage;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}