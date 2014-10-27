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
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            byte[] mainBuffer = new byte[eof - offset];
            Array.Copy(bytes, offset, mainBuffer, 0, mainBuffer.Length);
            MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider();
            return hash.ComputeHash(mainBuffer);
        }
    }
}