using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DomainModel.Security
{
    public static class SaltedHashGenerator
    {
        private const int SALT_BYTES = 24;
        private const int HASH_BYTES = 24;
        private const int PBKDF2_ITERATIONS = 1000;

        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;

        public static void CreateSaltAndHashFromPassword(string password, out string strSalt, out string strHash)
        {
            RNGCryptoServiceProvider oCryptography = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTES];
            oCryptography.GetBytes(salt);

            byte[] hash = GetSaltHashBytes(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);
        
            strSalt = Convert.ToBase64String(salt);
            strHash = Convert.ToBase64String(hash);
        }

        private static byte[] GetSaltHashBytes(string strPassword, byte[] arrbSalt, int iIterations, int iOutputBytes)
        {
            Rfc2898DeriveBytes oDeriveBytesToSaltHash = new Rfc2898DeriveBytes(strPassword, arrbSalt);
            oDeriveBytesToSaltHash.IterationCount = iIterations;
            return oDeriveBytesToSaltHash.GetBytes(iOutputBytes);
        }
    }
}
