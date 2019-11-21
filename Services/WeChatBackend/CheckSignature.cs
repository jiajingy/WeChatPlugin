using Services.Encryption;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.WeChatBackend
{
    public class CheckSignature
    {
        /// <summary>
        /// This is from WeChat settings, where account admin set it up.
        /// </summary>
        private string _token;


        public CheckSignature(string token)
        {
            _token = token;
        }

        public bool IsValidSignature(string timestamp, string nonce, string signature)
        {
            string[] arr = new string[] { _token, timestamp, nonce };
            Array.Sort(arr);

            StringBuilder sb = new StringBuilder();
            foreach (var s in arr)
                sb.Append(s);

            string str = sb.ToString();
            string SHA1Str = SHA1Encryption.Hash(str);

            return String.Equals(SHA1Str, signature);

        }
    }
}
