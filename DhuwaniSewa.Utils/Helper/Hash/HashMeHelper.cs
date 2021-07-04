using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DhuwaniSewa.Utils.Helper
{
    public class HashMeHelper
    {
        public static string Get(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
