using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlueZero.Air.Data.Models;
using Newtonsoft.Json;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press <Enter> to proceed...");
            Console.ReadLine();

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8177/api/child/1/bottle");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8177/api/child/1");


            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/oauth2/v1/userinfo?access_token=ya29.AHES6ZQ6MK6jq9Yx-VbOmtiB-8omoeL_mQTGlkabIqOSqTSB5tf92A");

            request.Headers["Authorization"] = "Bearer " + "1/JtkaWLmt1hiuc0r5E9CwHGPMAhm6x4Yv-8BF9JqtWcA";

            request.Method = "DELETE";
            request.ContentType = "application/json; charset=utf-8";

            //string json = JsonConvert.SerializeObject(new Bottle { AmountConsumed = 100, Date = DateTime.Now });

            //request.ContentLength = json.Length;
            request.ContentLength = 0;

            //StreamWriter writer = new StreamWriter(request.GetRequestStream());                        
            //writer.Write(json);
            //writer.Close();

            try
            {
                using (var response = request.GetResponse())
                {
                    string s = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Console.WriteLine(s);                    
                }                         
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string s = sr.ReadToEnd();

                    Console.WriteLine(ex);                    
                }                   
            }

            Console.WriteLine("Press <Enter> to finish...");
            Console.ReadLine();
        }

        public static void SetAuthHeader(WebRequest req, String token)
        {
            req.Headers["Authorization"] = "Bearer " + token;
        }
    }
}
