using System;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class EncryptionUtility
    {

        //static or addSingleton
        public static string HashSha256(string input)
        {
            using (var sha256Hash = SHA256.Create())
            {
                //compute hash returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


                //convert byte array to a string

                //var hash = new System.Text.StringBuilder();
                StringBuilder builder = new StringBuilder();
                foreach (byte theByte in bytes)
                {
                    builder.Append(theByte.ToString("x2"));
                }
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();

            }


        }

        public static String sha256_hash(String value)
        {
            //StringBuilder Sb = new StringBuilder();

            //using (SHA256 hash = SHA256Managed.Create())
            //{
            //    Encoding enc = Encoding.UTF8;
            //    Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            //    foreach (Byte b in result)
            //        Sb.Append(b.ToString("x2"));
            //}

            //return Sb.ToString();

            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(value));
            return Convert.ToBase64String(hashedDataBytes);
        }



        public static string HashPasswordWithSalt(string password, string salt)
        {
            return HashSha256(salt + password);
        }
    }
}
