using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Extentions
{
    public static class Hash
    { 
        public static int StringToSHA512(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            byte[] hash = null;
            using (var shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }

            return BitConverter.ToInt32(hash, 0);
        }
    }
}
