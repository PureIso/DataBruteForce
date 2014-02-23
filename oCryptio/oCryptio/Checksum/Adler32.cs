#region

using System;

#endregion

namespace oCryptio
{
    public class Adler32
    {
        public byte[] Compute(byte[] bytes)
        {
            const ushort adlerMod = 65521;
            uint num = 1;
            uint num2 = 0;
            foreach (byte num3 in bytes)
            {
                num = (num + num3)%adlerMod;
                num2 = (num2 + num)%adlerMod;
            }
            bytes = BitConverter.GetBytes((num2 << 16) | num);
            Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            const ushort adlerMod = 65521;
            uint num = 1;
            uint num2 = 0;
            for (int i = offset; i < bytes.Length; i++)
            {
                num = (num + bytes[i]) % adlerMod;
                num2 = (num2 + num) % adlerMod;
            }
            bytes = BitConverter.GetBytes((num2 << 16) | num);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}