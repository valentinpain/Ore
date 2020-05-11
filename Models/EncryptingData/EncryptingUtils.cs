using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ore.Models.EncryptingData
{
    /// <summary>
    /// The class used to encrypt passwords before inserting them in the database or to find a match when login in
    /// </summary>
    /// <remarks>
    /// To encrypt, we need an initVector and a keysize
    /// </remarks>
    public class EncryptingUtils
    {
        #region Properties

        /// <summary>
        /// The 16-bit initVector
        /// </summary>
        /// <remarks>
        /// The vector must always be 16-bit long in our case to work
        /// </remarks>
        private const string InitVector = "pemgail9uzpgzl88";

        /// <summary>
        /// The key size to encrypt our passwords strongly 
        /// </summary>
        private const int Keysize = 256;

        #endregion

        #region Methods

        /// <summary>
        /// The main method we use to encrypt the data
        /// </summary>
        /// <param name="plainText">The data that we need to encrypt</param>
        /// <param name="passPhrase">A password that we uses to encrypt data</param>
        /// <remarks>
        /// The pass phrase must be the same everytime if we want to have the real passwords back.
        /// We mainly use the <c>plainText</c> parameter to encrypt our passwords.
        /// </remarks>
        /// <returns>The encrypted data</returns>
        public static string EncryptString(string plainText, string passPhrase)
        {
            // We format the vector and the data for the encryption
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Derivation instantiation
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);

            // The key bite formatting
            byte[] keyBytes = password.GetBytes(Keysize / 8);

            // The symetric key creation to encrypt
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // The encryting mode
            symmetricKey.Mode = CipherMode.CBC;

            // Instanciation of the main encryptor
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Capacity instanciator
            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            // Data writting
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            // The final data crypted
            byte[] cipherTextBytes = memoryStream.ToArray();

            // We close the crypting features
            memoryStream.Close();
            cryptoStream.Close();

            return Convert.ToBase64String(cipherTextBytes);
        }

        #endregion

    }
}
