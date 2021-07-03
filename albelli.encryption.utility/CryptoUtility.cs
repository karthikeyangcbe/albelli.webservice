using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace albelli.encryption.utility
{
    public class CryptoUtility : ICryptoUtility
    {
        #region Fields
        private static CryptoUtility cryptoUtility = null;
        private static object instanceSync = new object();
        #endregion

        #region Properties
        public static CryptoUtility Instance
        {
            get
            {
                if (cryptoUtility == null)
                {
                    lock (instanceSync)
                    {
                        cryptoUtility = new CryptoUtility();
                    }
                }
                return cryptoUtility;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="encryptString">
        /// String that needs to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted String.
        /// </returns>
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "albellikey1234567890";
            byte[] bufferBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes aecInstance = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                aecInstance.Key = pdb.GetBytes(32);
                aecInstance.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aecInstance.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bufferBytes, 0, bufferBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        /// <summary>
        /// Decrypts the encrypted string.
        /// </summary>
        /// <param name="encryptedText">
        /// The string that needs to be decrypted.
        /// </param>
        /// <returns>
        /// The decrypted string.
        /// </returns>
        public string Decrypt(string encryptedText)
        {
            string encryptionKey = "albellikey1234567890";
            string returnValue;
            encryptedText = encryptedText.Replace(" ", "+");
            byte[] clearBytes = Convert.FromBase64String(encryptedText);
            using (Aes AesInsance = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                AesInsance.Key = pdb.GetBytes(32);
                AesInsance.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(ms, AesInsance.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(clearBytes, 0, clearBytes.Length);
                        cryptoStream.Close();
                    }
                    returnValue = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return returnValue;
        }

        #endregion
    }
}

