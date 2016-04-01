using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace BlueZero.Air.GoogleApi
{
    public class GoogleHttpClient : HttpClient
    {
        private const string TokenInfoApiUri = "https://accounts.google.com/o/oauth2/tokeninfo";
        private const string UserInfoApiUri = "https://www.googleapis.com/oauth2/v1/userinfo";

        private const int RetryMax = 3;
        private const int RetryWaitMs = 1000;

        private ILog _log = LogManager.GetLogger(typeof(GoogleHttpClient));

        private int _retryCount;
        
        public bool TryGetUserInfo(string accessToken, out JObject info)
        {
            info = null;
            HttpResponseMessage responseMessage = null;

            do
            {
                _log.Debug("Attempting get user info request to Google...");

                responseMessage = SendRequestForResponse(accessToken);

                _log.DebugFormat("Get user info response status code from Google was '{0}'", responseMessage.StatusCode);

                if (responseMessage.IsSuccessStatusCode)
                {
                    _log.Debug("Handling good response. Reading response content....");

                    info = responseMessage.Content.ReadAsAsync<JObject>().Result;

                    return true;
                }
                else
                {
                    _retryCount++;

                    string responseContent = responseMessage.Content.ReadAsAsync<JObject>().Result.ToString();

                    _log.DebugFormat("Received bad response ({0} {1}). Waiting {2}ms and then retrying....", responseMessage.StatusCode, responseContent, RetryWaitMs);

                    Task.Delay(RetryWaitMs);
                }
            }
            while (_retryCount != RetryMax);

            return false;
        }

        private HttpResponseMessage SendRequestForResponse(string accessToken)
        {
            string uri = UserInfoApiUri + "?access_token=" + accessToken;

            HttpResponseMessage responseMessage = GetAsync(uri).Result;

            return responseMessage;
        }
    }
}
