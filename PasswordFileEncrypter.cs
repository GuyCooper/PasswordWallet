namespace PasswordWallet
{
    using System;
    using System.IO;
    #region References

    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    public static class PasswordFileEncrypter
    {
        /// <summary>
        /// Encrypts a file from its path and a plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="password"></param>
        public static async Task FileEncrypt(string inputFile, string outputFile,  string password)
        {
            await StringEncrypt(File.ReadAllText(inputFile), outputFile, password);
        }

        public static async Task StringEncrypt(string input, string outputFile, string password)
        {
            await Task.Run(() =>
            {
                //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

                //generate random salt
                byte[] salt = GenerateRandomSalt();

                //create output file name
                FileStream fsCrypt = new FileStream(outputFile, FileMode.Create);

                //convert password string to byte arrray
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                //Set Rijndael symmetric encryption algorithm
                RijndaelManaged AES = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128,
                    Padding = PaddingMode.PKCS7
                };

                //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
                //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
                var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
                AES.Mode = CipherMode.CFB;

                // write salt to the begining of the output file, so in this case can be random every time
                fsCrypt.Write(salt, 0, salt.Length);

                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

                Stream fsIn = new MemoryStream( Encoding.UTF8.GetBytes(input ));

                //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
                byte[] buffer = new byte[1048576];
                int read;

                try
                {
                    while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }

                    // Close up
                    fsIn.Close();
                }
                finally
                {
                    cs.Close();
                    fsCrypt.Close();
                }
            });
        }

        /// <summary>
        /// Decrypts an encrypted file with the FileEncrypt method through its path and the plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="password"></param>
        public static async Task FileDecrypt(string inputFile, string outputFile, string password)
        {
            var result = await FileDecrypt(inputFile, password);
            File.WriteAllText(outputFile, result);
        }

        public static async Task<string> FileDecrypt(string inputFile, string password)
        {
            return await Task.Run(() =>
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] salt = new byte[32];

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                fsCrypt.Read(salt, 0, salt.Length);

                RijndaelManaged AES = new RijndaelManaged
                {
                    KeySize = 256,
                    BlockSize = 128
                };
                var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Padding = PaddingMode.PKCS7;
                AES.Mode = CipherMode.CFB;

                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

                MemoryStream fsOut = new MemoryStream();

                int read;
                byte[] buffer = new byte[1048576];

                string result;
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsOut.Write(buffer, 0, read);
                }

                result = Encoding.UTF8.GetString(fsOut.GetBuffer());
                try
                {
                    cs.Close();
                }
                finally
                {
                    fsOut.Close();
                    fsCrypt.Close();
                }

                return result;
            });
        }

        private static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

    }
}
