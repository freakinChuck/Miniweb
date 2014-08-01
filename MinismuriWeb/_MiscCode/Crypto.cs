using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace MinismuriWeb
{
    public static class Crypto
    {
        public static string GetHash(string password)
        {
            SHA256Managed sha = new SHA256Managed();
            return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.Default.GetBytes(password)));
        }
    }
}