#region

using System;
using System.Security.Cryptography;

#endregion

namespace oCryptio.Checksum
{
    public static class HmacSha1
    {
        public static byte[] Compute(byte[] bytes)
        {
            HMACSHA1 hash = new HMACSHA1();
            return hash.ComputeHash(bytes);
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            byte[] mainBuffer = new byte[bytes.Length-offset];
            Array.Copy(bytes,offset,mainBuffer,0,mainBuffer.Length);
            HMACSHA1 hash = new HMACSHA1();
            return hash.ComputeHash(mainBuffer);
        }
    }
}