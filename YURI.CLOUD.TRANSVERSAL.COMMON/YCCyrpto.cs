using System.Security.Cryptography;
using System.Text;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    
    public static class YCCyrpto
    {
        public enum SHAFamily
        {
            SHA256, SHA384, SHA512
        }

        private static readonly TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();

        private static byte[] MD5Hash(string value)
        {
            return MD5.ComputeHash(Encoding.ASCII.GetBytes(value));
        }

        public static string CifrarClave(string stringToEncrypt, string key)
        {
            DES.Key = MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = Encoding.ASCII.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string DescifrarClave(string encryptedString, string key)
        {
            try
            {
                encryptedString = encryptedString.Replace(" ", "+");
                string clave;
                DES.Key = MD5Hash(key);
                DES.Mode = CipherMode.ECB;
                byte[] Buffer = Convert.FromBase64String(encryptedString);
                clave = Encoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
                return clave;
            }
            catch (YCException ex)
            {
                return null;
            }
        }

        public static string DescifrarClave(string dataToDecrypt, string password, string salt)
        {
            try
            {
                dataToDecrypt = dataToDecrypt.Replace(" ", "+");
                AesManaged aes = null;
                System.IO.MemoryStream memoryStream = null;
                try
                {
                    Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
                    aes = new AesManaged();
                    aes.Key = rfc2898.GetBytes(32);
                    aes.IV = rfc2898.GetBytes(16);
                    memoryStream = new System.IO.MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    byte[] data = Convert.FromBase64String(dataToDecrypt);
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] decryptBytes = memoryStream.ToArray();
                    if (cryptoStream != null)
                        cryptoStream.Dispose();
                    return Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                }
                finally
                {
                    if (memoryStream != null)
                        memoryStream.Dispose();
                    if (aes != null)
                        aes.Clear();
                }
            }
            catch (YCException ex)
            {
                // Return ex.Message.ToString
                return null;
            }
        }

        /// <summary>
        /// Método de crifrado de contraseñas
        /// </summary>
        /// <param name="plainText">Contraseña a encriptar</param>
        /// <param name="salt">bits aleatorios, es opcional, se generará una si no es enviada</param>
        /// <param name="algorithm">método de cifrado, es opcional, si no se define por defecto tomada SHA512</param>
        /// <returns>retorna un hash con la salt concatenada</returns>
        public static string ComputeHashV1(string plainText, byte[] salt = null, SHAFamily algorithm = SHAFamily.SHA512)
        {
            int minSaltLength = 4, maxSaltLength = 16;

            byte[] saltBytes = null;
            if (salt != null)
            {
                saltBytes = salt;
            }
            else
            {
                Random r = new Random();
                int SaltLength = r.Next(minSaltLength, maxSaltLength);
                saltBytes = new byte[SaltLength];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(saltBytes);
                rng.Dispose();
            }

            byte[] plainData = ASCIIEncoding.UTF8.GetBytes(plainText);
            byte[] plainDataWithSalt = new byte[plainData.Length + saltBytes.Length];

            for (int x = 0; x < plainData.Length; x++)
                plainDataWithSalt[x] = plainData[x];
            for (int n = 0; n < saltBytes.Length; n++)
                plainDataWithSalt[plainData.Length + n] = saltBytes[n];

            byte[] hashValue = null;

            switch (algorithm)
            {
                case SHAFamily.SHA256:
                    SHA256Managed sha = new SHA256Managed();
                    hashValue = sha.ComputeHash(plainDataWithSalt);
                    sha.Dispose();
                    break;
                case SHAFamily.SHA384:
                    SHA384Managed sha1 = new SHA384Managed();
                    hashValue = sha1.ComputeHash(plainDataWithSalt);
                    sha1.Dispose();
                    break;
                case SHAFamily.SHA512:
                    SHA512Managed sha2 = new SHA512Managed();
                    hashValue = sha2.ComputeHash(plainDataWithSalt);
                    sha2.Dispose();
                    break;
            }

            byte[] result = new byte[hashValue.Length + saltBytes.Length];
            for (int x = 0; x < hashValue.Length; x++)
                result[x] = hashValue[x];
            for (int n = 0; n < saltBytes.Length; n++)
                result[hashValue.Length + n] = saltBytes[n];

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Método de verificado de contraseñas
        /// </summary>
        /// <param name="plainText">Contraseña sin encriptar a verificar</param>
        /// <param name="hashValue">Hash generado previamente con cual se desea verificar</param>
        /// <param name="algorithm">método de cifrado, es opcional, si no se define por defecto tomada SHA512</param>
        /// <returns>retorna true o false</returns>
        public static bool ConfirmHashV1(string plainText, string hashValue, SHAFamily algorithm = SHAFamily.SHA512)
        {
            byte[] hashBytes = Convert.FromBase64String(hashValue);
            int hashSize = 0;

            switch (algorithm)
            {
                case SHAFamily.SHA256:
                    hashSize = 32;
                    break;
                case SHAFamily.SHA384:
                    hashSize = 48;
                    break;
                case SHAFamily.SHA512:
                    hashSize = 64;
                    break;
            }

            byte[] saltBytes = new byte[hashBytes.Length - hashSize];

            for (int x = 0; x < saltBytes.Length; x++)
                saltBytes[x] = hashBytes[hashSize + x];

            string newHash = ComputeHashV1(plainText, saltBytes, algorithm);

            return (hashValue == newHash);
        }

        /// <summary>
        /// Método de crifrado de contraseñas que genera un hash sin un salt concatenado
        /// </summary>
        /// <param name="plainText">Contraseña a encriptar</param>
        /// <param name="salt">bits aleatorios, es opcional, se generará una si no es enviada</param>
        /// <param name="algorithm">método de cifrado, es opcional, si no se define por defecto tomada SHA512</param>
        /// <returns>retorna un hash y una cadena salt aleatoria </returns>
        public static (string Hash, string HashSalt) ComputeHashV2(string plainText, byte[] salt = null, SHAFamily algorithm = SHAFamily.SHA512)
        {
            int minSaltLength = 4, maxSaltLength = 16;

            if (salt is null)
            {
                Random rndm = new Random();
                int SaltLength = rndm.Next(minSaltLength, maxSaltLength);
                salt = new byte[SaltLength];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(salt);
                rng.Dispose();
            }

            byte[] plainData = Encoding.UTF8.GetBytes(plainText);
            byte[] plainDataWithSalt = new byte[plainData.Length + salt.Length];

            for (int x = 0; x < plainData.Length; x++)
                plainDataWithSalt[x] = plainData[x];
            for (int n = 0; n < salt.Length; n++)
                plainDataWithSalt[plainData.Length + n] = salt[n];

            byte[] hashValue = null;

            switch (algorithm)
            {
                case SHAFamily.SHA256:
                    SHA256Managed sha = new SHA256Managed();
                    hashValue = sha.ComputeHash(plainDataWithSalt);
                    sha.Dispose();
                    break;
                case SHAFamily.SHA384:
                    SHA384Managed sha1 = new SHA384Managed();
                    hashValue = sha1.ComputeHash(plainDataWithSalt);
                    sha1.Dispose();
                    break;
                case SHAFamily.SHA512:
                    SHA512Managed sha2 = new SHA512Managed();
                    hashValue = sha2.ComputeHash(plainDataWithSalt);
                    sha2.Dispose();
                    break;
            }

            return (Convert.ToBase64String(hashValue), Convert.ToBase64String(salt));
        }

        /// <summary>
        /// Método de verificado de contraseñas a partir de un hash (generado previamente) y un salt
        /// </summary>
        /// <param name="plainText">Contraseña sin encriptar a verificar</param>
        /// <param name="hashValue">has genrado sin salt</param>
        /// <param name="hashSalt">salt generada previamente</param>
        /// <param name="algorithm">método de cifrado, es opcional, si no se define por defecto tomada SHA512</param>
        /// <returns>retorna true o false</returns>
        public static bool ConfirmHashV2(string plainText, string hashValue, string hashSalt, SHAFamily algorithm = SHAFamily.SHA512)
        {
            try
            {
                byte[] saltBytes = Convert.FromBase64String(hashSalt);

                (string newHash, _) = ComputeHashV2(plainText, saltBytes, algorithm);

                return (hashValue == newHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
