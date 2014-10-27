using System;
using System.Security.Cryptography;

namespace oCryptio.Checksum
{
    public class Md5Cng
    {
        public byte[] Compute(byte[] bytes)
        {
            Md5Cng hash = new Md5Cng();
            return hash.Compute(bytes);
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            byte[] mainBuffer = new byte[eof - offset];
            Array.Copy(bytes, offset, mainBuffer, 0, mainBuffer.Length);
            Md5Cng hash = new Md5Cng();
            return hash.Compute(mainBuffer);
        }
    }
}