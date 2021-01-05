using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurePasswordStorage
{
    public class PasswordManager
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 50000;

        public byte[] GenerateSalt()
        {
            var salt = new byte[SaltByteSize];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public HashedPassword HashPassword(string password)
        {
            byte[] salt = this.GenerateSalt();
            var hash = ComputeHash(password, salt);

            HashedPassword hashedPassword = new HashedPassword(salt, Convert.ToBase64String(hash));
            return hashedPassword;
        }

        internal static byte[] ComputeHash(string password, byte[] salt, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(hashByteSize);
        }

        public bool IsValid(string UserPassword, HashedPassword DBPass, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            var userHash = ComputeHash(UserPassword, DBPass.Salt, iterations);
            if(Convert.ToBase64String(userHash) == DBPass.Hash)
            {
                return (true);
            }

            return (false);


        }
    }
}
