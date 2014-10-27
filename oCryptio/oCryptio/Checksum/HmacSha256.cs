#region

using System;
using System.Security.Cryptography;

#endregion

namespace oCryptio.Checksum
{
    public class HmacSha256
    {
        public byte[] Compute(byte[] bytes)
        {
            HMACSHA256 hash = new HMACSHA256();
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
            HMACSHA256 hash = new HMACSHA256();
            return hash.ComputeHash(mainBuffer);
        }
    }
}