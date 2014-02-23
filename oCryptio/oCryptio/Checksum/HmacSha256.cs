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
            byte[] mainBuffer = new byte[bytes.Length - offset];
            Array.Copy(bytes, offset, mainBuffer, 0, mainBuffer.Length);
            HMACSHA256 hash = new HMACSHA256();
            return hash.ComputeHash(mainBuffer);
        }
    }
}