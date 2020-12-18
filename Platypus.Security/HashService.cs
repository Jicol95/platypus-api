using Platypus.Security.Interface;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Platypus.Security {

    public class HashService : IHashService {
        private readonly int hashLength;
        private readonly int iterations;
        private readonly int saltLength;

        public HashService(int hashLength, int saltLength, int iterations) {
            this.hashLength = hashLength;
            this.saltLength = saltLength;
            this.iterations = iterations;
        }

        public HashService()
            : this(24, 24, 100000) {
        }

        private byte[] ComputeHash(byte[] data, byte[] salt) {
            using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(data, salt, iterations);
            return pbkdf2.GetBytes(hashLength);
        }

        public void GetHashAndSalt(byte[] data, out byte[] hash, out byte[] salt) {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

            salt = new byte[saltLength];
            provider.GetBytes(salt);

            using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(data, salt, iterations);
            hash = pbkdf2.GetBytes(hashLength);
        }

        public void GetHashAndSaltString(string data, out string hash, out string salt) {
            GetHashAndSalt(Encoding.UTF8.GetBytes(data), out byte[] hashOut, out byte[] saltOut);

            hash = Convert.ToBase64String(hashOut);
            salt = Convert.ToBase64String(saltOut);
        }

        public bool VerifyHash(byte[] data, byte[] hash, byte[] salt) {
            byte[] newHash = ComputeHash(data, salt);

            if (newHash.Length != hash.Length)
                return false;

            for (int Lp = 0; Lp < hash.Length; Lp++)
                if (!hash[Lp].Equals(newHash[Lp]))
                    return false;

            return true;
        }

        public bool VerifyHashString(string data, string hash, string salt) {
            byte[] hashToVerify = Convert.FromBase64String(hash);
            byte[] saltToVerify = Convert.FromBase64String(salt);
            byte[] dataToVerify = Encoding.UTF8.GetBytes(data);

            return VerifyHash(dataToVerify, hashToVerify, saltToVerify);
        }
    }
}