#region

using System;
using System.Security.Cryptography;

#endregion

namespace oCryptio.Checksum
{
    public class Md5
    {
        public byte[] Compute(byte[] bytes)
        {
            MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider();
            return hash.ComputeHash(bytes);
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            byte[] mainBuffer = new byte[bytes.Length - offset];
            Array.Copy(bytes, offset, mainBuffer, 0, mainBuffer.Length);
            MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider();
            return hash.ComputeHash(mainBuffer);
        }
    }
}