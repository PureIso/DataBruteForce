#region

using System;
using System.Security.Cryptography;

#endregion

namespace oCryptio.Checksum
{
    public class HmacSha384
    {
        public byte[] Compute(byte[] bytes)
        {
            HMACSHA384 hash = new HMACSHA384();
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
            HMACSHA384 hash = new HMACSHA384();
            return hash.ComputeHash(mainBuffer);
        }
    }
}