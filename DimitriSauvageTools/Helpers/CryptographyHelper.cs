﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DimitriSauvageTools.Helpers
{
    /// <summary>
    /// Classe gérant le chiffrement des données
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary> 
        /// Encrypt the given string using AES. The string can be decrypted using
        /// DecryptStringAES(). The sharedSecret parameters must match. 
        /// </summary> 
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param> 
        /// <param name="salt">Chaine utilisée pour complexifié le cryptage</param>
        public static string EncryptStringAES(string plainText, string sharedSecret, string salt)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));

            string outStr = null;                   // Encrypted string to return 
            RijndaelManaged aesAlg = null;          // RijndaelManaged object used to encrypt the data. 
            try
            { 
                //generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt));

                // Create a RijndaelManaged object 
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform. 
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV 
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream. 
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null) aesAlg.Clear();
            }
            // Return the encrypted bytes from the memory stream. 
            return outStr;
        }

        /// <summary> 
        /// Encrypt the given string using AES. The string can be decrypted using 
        /// DecryptStringAES(). The sharedSecret parameters must match. 
        /// </summary> 
        /// <param name="plainText">The text to encrypt.</param> 
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        /// 
        /// <param name="salt">Chaine utilisée pour complexifié le cryptage</param>
        public static string DecryptStringAES(string cipherText, string sharedSecret, string salt)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(sharedSecret)) throw new ArgumentNullException(nameof(sharedSecret));

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;
            try
            {
                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(salt));

                // Create the streams used for decryption. 
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // Create a RijndaelManaged object 
                    // with the specified key and IV. 
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    // Get the initialization vector from the encrypted stream 
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string. 
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null) aesAlg.Clear();
            }
            return plaintext;
        }
        
        /// <summary>
        /// Lit byte par byte 
        /// </summary>
        /// <param name="s">Stream dans lequel lire</param>
        /// <returns></returns>
        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }
            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }
            return buffer;
        }

    }
}
