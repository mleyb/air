using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlueZero.Air.Data
{
    public class RandomDataGenerator : IRandomDataGenerator
    {
        public byte[] GenerateBytes(int lengthInBytes)
        {
            using (var crypto = RNGCryptoServiceProvider.Create())
            {
                byte[] buffer = new byte[lengthInBytes];

                crypto.GetBytes(buffer);

                return buffer;
            }
        }

        public string GenerateBase64(int lengthInBytes)
        {
            using (var crypto = RNGCryptoServiceProvider.Create())
            {
                byte[] buffer = new byte[lengthInBytes];

                crypto.GetBytes(buffer);

                return Convert.ToBase64String(buffer);
            }
        }

        public string GenerateString(int lengthInChars)
        {
            string base64 = GenerateBase64(lengthInChars + 10);

            return base64.Substring(0, lengthInChars);
        }
    }
}
