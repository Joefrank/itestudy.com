using System;
using System.Security.Cryptography;
using System.IO;
using CMCommon.Security.Interfaces;

namespace CMCommon.Security.Implementation
{
    /// <summary>
    /// Common functions to do simple encryption
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private string eKey = "cyberminds";

        public string Ekey
        {
            get { return eKey; }
            set { eKey = value; }
        }

        public EncryptionService()
        {            
        }

        public EncryptionService(string key)
        {
            eKey = key;
        }

        public string Encrypt(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : Encrypt(str, eKey);
        }

        public string Encrypt(string clearText, string Password)
        {
            //turn the input string into a byte array. 
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            // Then, we need to turn the password into Key and IV         
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            // Using PasswordDeriveBytes object we are first getting 32 bytes for the Key        
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            // We are going to be using Base64 encoding that is designed.       
            return Convert.ToBase64String(encryptedData);
        }

        public byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream to accept the encrypted bytes 
            MemoryStream ms = new MemoryStream();
            // We are going to use Rijndael because it is strong and available on all platforms.         
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV.         
            alg.Key = Key;
            alg.IV = IV;
            // Create a CryptoStream through which we are going to bepumping our data.       
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the encryption 
            cs.Write(clearData, 0, clearData.Length);
            // Close the crypto stream (or do FlushFinalBlock).        
            cs.Close();
            // Now get the encrypted data from the MemoryStream.        
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public string Decrypt(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : Decrypt(str, eKey);
        }

        public string Decrypt(string cipherText, string Password)
        {
            // First we need to turn the input string into a byte array. 
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            // Then, we need to turn the password into Key and IV 
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            // Now get the key/IV and do the decryption using the function that accepts byte arrays. 
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            // Now we need to turn the resulting byte array into a string. 
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            // Create a MemoryStream that is going to accept the decrypted bytes 
            MemoryStream ms = new MemoryStream();
            // Create a symmetric algorithm. 
            Rijndael alg = Rijndael.Create();
            // Now set the key and the IV. 
            alg.Key = Key;
            alg.IV = IV;
            // Create a CryptoStream through which we are going to be pumping our data. 
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);
            // Close the crypto stream (or do FlushFinalBlock). 
            cs.Close();
            // Now get the decrypted data from the MemoryStream. 
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

    }
}
