using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace NFZ_PRIVATE
{
    class SecurityMD5
    {
        static string GetMd5Hash(MD5 md5Hash, string password)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string password, string checkConvert)
        {
            // Hash the input.
            string convertPassword = GetMd5Hash(md5Hash, password);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(convertPassword, checkConvert))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void PasswordToMD5(ref string password, out bool confirm)
        {
            confirm = false;

            using (MD5 md5Hash = MD5.Create())
            {
                string convertPassword = GetMd5Hash(md5Hash, password);

                if (VerifyMd5Hash(md5Hash, password, convertPassword))
                {
                    password = convertPassword;
                    confirm = true;
                }
                else
                {
                    confirm = false;
                }
            }

        }
    }
}
