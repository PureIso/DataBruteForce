#region

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using oCryptio.Checksum;

#endregion

namespace oCryptio
{
    public class oDecrypt
    {
        #region Variables

        private readonly byte[] _key =
        {
            0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99,
            0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF,
            0xA9, 0xB8, 0xC7, 0xD6, 0xE5, 0xF4, 0x32, 0x10
        };

        private string _result;

        #endregion

        public oDecrypt()
        {
        }

        public oDecrypt(string inputtedData, string password)
        {
            _result = null;
            //Get Encrypted data and convert to byte[]
            byte[] encryptedData = Convert.FromBase64String(inputtedData);
            //Get actual data
            byte[] realData = new byte[encryptedData.Length - 4];
            Array.Copy(encryptedData, 0, realData, 0, realData.Length);
            //Get checksum
            byte[] checksum = new byte[4];
            Array.Copy(encryptedData, realData.Length, checksum, 0, checksum.Length);
            byte[] check = Crc32.Compute(realData);

            //compare the new computed data to the version on the encrypted data
            if (!check.SequenceEqual(checksum)) return;

            //If all is valid Get password
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(password);
            Array.Copy(buffer, 0, _key, 0, buffer.Length);

            //Get ready to decrypt
            TripleDES tripleDes = TripleDES.Create();
            tripleDes.Key = _key;
            tripleDes.Mode = CipherMode.ECB;


            ICryptoTransform icryptoTransform = tripleDes.CreateDecryptor();
            byte[] resultArray = icryptoTransform.TransformFinalBlock(realData, 0, realData.Length);
            _result = Encoding.UTF8.GetString(resultArray);

            tripleDes.Clear();
        }

        public string DecryptedData
        {
            get { return _result; }
        }

        public bool DecryptData(string inputtedData, string password)
        {
            _result = null;
            //Get Encrypted data and convert to byte[]
            byte[] encryptedData = Convert.FromBase64String(inputtedData);
            //Get actual data
            byte[] realData = new byte[encryptedData.Length - 4];
            Array.Copy(encryptedData, 0, realData, 0, realData.Length);
            //Get checksum
            byte[] checksum = new byte[4];
            Array.Copy(encryptedData, realData.Length, checksum, 0, checksum.Length);
            byte[] check = Crc32.Compute(realData);

            //compare the new computed data to the version on the encrypted data
            if (!check.SequenceEqual(checksum)) return false;

            //If all is valid Get password
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            Array.Copy(buffer, 0, _key, 0, buffer.Length);

            //Get ready to decrypt
            TripleDES tripleDes = TripleDES.Create();
            tripleDes.Key = _key;
            tripleDes.Mode = CipherMode.ECB;


            ICryptoTransform icryptoTransform = tripleDes.CreateDecryptor();
            byte[] resultArray = icryptoTransform.TransformFinalBlock(realData, 0, realData.Length);
            _result = Encoding.UTF8.GetString(resultArray);

            tripleDes.Clear();
            return true;
        }
    }
}