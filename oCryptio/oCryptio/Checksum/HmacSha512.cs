#region

using System;
using System.Security.Cryptography;

#endregion

namespace oCryptio.Checksum
{
    public class HmacSha512
    {
        public byte[] Compute(byte[] bytes)
        {
            HMACSHA512 hash = new HMACSHA512();
            return hash.ComputeHash(bytes);
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            byte[] mainBuffer = new byte[bytes.Length - offset];
            Array.Copy(bytes, offset, mainBuffer, 0, mainBuffer.Length);
            HMACSHA512 hash = new HMACSHA512();
            return hash.ComputeHash(mainBuffer);
        }
    }
}