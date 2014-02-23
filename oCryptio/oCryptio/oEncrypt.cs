#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using oCryptio.Checksum;

#endregion

namespace oCryptio
{
    public class oEncrypt
    {
        #region Variables

        private readonly byte[] _key =
        {
            0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99,
            0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF,
            0xA9, 0xB8, 0xC7, 0xD6, 0xE5, 0xF4, 0x32, 0x10
        };

        #endregion

        public oEncrypt()
        {
        }

        public oEncrypt(string inputtedData, string password, string filename)
        {
            string encryptedData;
            //Turn password to byte[]
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            //Copy to key
            Array.Copy(buffer, 0, _key, 0, buffer.Length);
            //Turn Data to byte[]
            byte[] data = Encoding.UTF8.GetBytes(inputtedData);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                TripleDES tripleDes = TripleDES.Create();
                tripleDes.Key = _key;
                tripleDes.Mode = CipherMode.ECB;

                using (
                    CryptoStream crytoStream = new CryptoStream(memoryStream, tripleDes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                {
                    crytoStream.Write(data, 0, data.Length);
                    crytoStream.FlushFinalBlock();
                    byte[] cipherBytes = memoryStream.ToArray();
                    byte[] newBuffer = new byte[cipherBytes.Length + 4];

                    Array.Copy(cipherBytes, 0, newBuffer, 0, cipherBytes.Length);
                    Array.Copy(Crc32.Compute(cipherBytes), 0, newBuffer, cipherBytes.Length, 4);
                    encryptedData = Convert.ToBase64String(newBuffer);
                }
                tripleDes.Clear();
            }

            using (StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\" + filename))
            {
                writer.Write(encryptedData);
            }
        }

        public string EncryptData(string inputtedData, string password)
        {
            string encryptedData;
            //Turn password to byte[]
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            //Copy to key
            Array.Copy(buffer, 0, _key, 0, buffer.Length);
            //Turn Data to byte[]
            byte[] data = Encoding.UTF8.GetBytes(inputtedData);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                TripleDES tripleDes = TripleDES.Create();
                tripleDes.Key = _key;
                tripleDes.Mode = CipherMode.ECB;

                using (
                    CryptoStream crytoStream = new CryptoStream(memoryStream, tripleDes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                {
                    crytoStream.Write(data, 0, data.Length);
                    crytoStream.FlushFinalBlock();
                    byte[] cipherBytes = memoryStream.ToArray();
                    byte[] newBuffer = new byte[cipherBytes.Length + 4];

                    Array.Copy(cipherBytes, 0, newBuffer, 0, cipherBytes.Length);
                    Array.Copy(Crc32.Compute(cipherBytes), 0, newBuffer, cipherBytes.Length, 4);
                    encryptedData = Convert.ToBase64String(newBuffer);
                }
                tripleDes.Clear();
            }
            return encryptedData;
        }

        public bool EncryptData(string inputtedData, string password, string filename)
        {
            string encryptedData;
            //Turn password to byte[]
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            //Copy to key
            Array.Copy(buffer, 0, _key, 0, buffer.Length);
            //Turn Data to byte[]
            byte[] data = Encoding.UTF8.GetBytes(inputtedData);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                TripleDES tripleDes = TripleDES.Create();
                tripleDes.Key = _key;
                tripleDes.Mode = CipherMode.ECB;

                using (
                    CryptoStream crytoStream = new CryptoStream(memoryStream, tripleDes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                {
                    crytoStream.Write(data, 0, data.Length);
                    crytoStream.FlushFinalBlock();
                    byte[] cipherBytes = memoryStream.ToArray();
                    byte[] newBuffer = new byte[cipherBytes.Length + 4];

                    Array.Copy(cipherBytes, 0, newBuffer, 0, cipherBytes.Length);
                    Array.Copy(Crc32.Compute(cipherBytes), 0, newBuffer, cipherBytes.Length, 4);
                    encryptedData = Convert.ToBase64String(newBuffer);
                }
                tripleDes.Clear();
            }

            using (StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "\\" + filename))
            {
                writer.Write(encryptedData);
            }
            return true;
        }
    }
}