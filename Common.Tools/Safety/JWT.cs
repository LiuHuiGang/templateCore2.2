using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Tools.Safety
{
    //没用到此类
    class JWT
    {
        // c# Hmacsha256
        protected string HMacSha256Hash(string key, string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                var buffer = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));
                var b64 = Base64UrlSafeString(buffer);
                return b64;
            }
        }

        protected string Base64UrlSafeString(byte[] bytes)
        {
            var b64 = Convert.ToBase64String(bytes);
            return b64.Replace("=", "").Replace("+", "-").Replace("/", "_");
        }

        // c# jwt
        public void testJWT()
        {
            string secret = "eerp";
            string header = "{\"type\":\"JWT\",\"alg\":\"HS256\"}";
            string claim = "{\"iss\":\"cnooc\", \"sub\":\"yrm\", \"username\":\"yrm\", \"admin\":true}";

            var encoding = Encoding.UTF8;
            var base64Header = Base64UrlSafeString(encoding.GetBytes(header));
            var base64Claim = Base64UrlSafeString(encoding.GetBytes(claim));
            var signature = HMacSha256Hash(secret, base64Header + "." + base64Claim);
            var jwt = base64Header + "." + base64Claim + "." + signature;
            Console.WriteLine(jwt);
        }


    }
}
