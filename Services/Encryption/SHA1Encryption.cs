using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Encryption
{
    public static class SHA1Encryption
    {
        public static string Hash(string str)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                var sb = new StringBuilder(hash.Length * 2);

                foreach(byte b in hash)
                {
                    // can be "X2" if uppercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
