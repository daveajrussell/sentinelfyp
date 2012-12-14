using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SqlRepositories.Helper
{
    public static class SecurityHelper
    {
        public static bool ValidatePassword(string strPassword, string strUserSalt, string strUserHash)
        {
            byte[] arrbUserSalt = Convert.FromBase64String(strUserSalt);
            byte[] arrbUserHash = Convert.FromBase64String(strUserHash);

            byte[] arrbHash = ToPBKDF2(strPassword, arrbUserSalt, arrbUserHash.Length);
            return ValidateHash(arrbUserHash, arrbHash);
        }

        private static byte[] ToPBKDF2(string strPassword, byte[] arrbSalt, int iOutput)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(strPassword, arrbSalt);
            return pbkdf2.GetBytes(iOutput);
        }

        private static bool ValidateHash(byte[] arrbUserHash, byte[] arrbHash)
        {
            uint iDiff = (uint)arrbUserHash.Length ^ (uint)arrbHash.Length;

            for (int i = 0; i < arrbUserHash.Length && i < arrbHash.Length; i++)
                iDiff |= (uint)(arrbUserHash[i] ^ arrbHash[i]);

            return iDiff == 0;
        }
    }
}
