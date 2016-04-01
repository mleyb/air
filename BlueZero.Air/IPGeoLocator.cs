using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace BlueZero.Air
{
    public class LocationInfo
    { 
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Zip { get; set; }
        public string IPAddress { get; set; }
    }

    public class IPGeoLocator
    {
        private const string APIKey = "3515418a573b5cabfde70bf52ace7e6056a2c1169d1f2b9002d2080dc99345c8";

        public LocationInfo GetData(string ipAddress)
        {
            LocationInfo loc = null;

            try
            {
                WebClient client = new WebClient();

                string[] eResult = client.DownloadString(GenerateIPQueryURI(ipAddress)).ToString().Split(',');
                
                if (eResult.Length > 0)
                {
                    loc = (LocationInfo)Deserialize(eResult[0].ToString());
                }
            }
            catch
            { }

            return loc;
        }

        private string GenerateIPQueryURI(string ipAddress)        
        {
            string uri = String.Format("http://api.ipinfodb.com/v2/ip_query.php?key={0}&ip={1}&timezone=false", APIKey, ipAddress);

            return uri;
        }

        private Object Deserialize(String pXmlizedString)
        {
            XmlSerializer xs = new XmlSerializer(typeof(LocationInfo));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }
 
        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
    }
}