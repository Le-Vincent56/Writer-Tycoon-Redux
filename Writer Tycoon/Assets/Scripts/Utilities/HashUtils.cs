using System;
using System.Security.Cryptography;
using System.Text;

namespace WriterTycoon.Utilities.Hash
{
    public static class HashUtils
    {
        public static int GenerateHash(string input)
        {
            // Use SHA256 for hash generation
            using(SHA256 sha256 = SHA256.Create())
            {
                // Get the UTF-8 bytes from the input
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Convert the bytes to an int
                int hashInt = BitConverter.ToInt32(hashBytes, 0);

                return hashInt;
            }
        }
    }
}