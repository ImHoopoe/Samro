using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.Core.Tools.Security
{
    public class KeyGenerator
    {
        public static string GenerateKey(int keySizeInBits)
        {
            if (keySizeInBits % 8 != 0)
            {
                throw new ArgumentException("Key size must be a multiple of 8.");
            }

            int keySizeInBytes = keySizeInBits / 8;
            byte[] keyBytes = new byte[keySizeInBytes];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            return Convert.ToBase64String(keyBytes);
        }
    }
}
