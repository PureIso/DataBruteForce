#region

using System;

#endregion

namespace oCryptio
{
    public class Adler16
    {
        public byte[] Compute(byte[] bytes)
        {
            const ushort adlerMod = 251;
            ushort num3 = 1;
            ushort num = (ushort) (num3 & 0xffff);
            ushort num2 = (ushort) (((ushort) (num3 >> 8)) & 0xffff);
            foreach (byte num4 in bytes)
            {
                num = (ushort) (((ushort) (num + num4))%adlerMod);
                num2 = (ushort) (((ushort) (num2 + num))%adlerMod);
            }
            num3 = (ushort) (((ushort) (num2 << 8)) | num);
            bytes = BitConverter.GetBytes(num3);
            Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            const ushort adlerMod = 251;
            ushort num3 = 1;
            ushort num = (ushort)(num3 & 0xffff);
            ushort num2 = (ushort)(((ushort)(num3 >> 8)) & 0xffff);
            for (int i = offset; i < eof; i++)
            {
                num = (ushort)(((ushort)(num + bytes[i])) % adlerMod);
                num2 = (ushort)(((ushort)(num2 + num)) % adlerMod);
            }
            num3 = (ushort)(((ushort)(num2 << 8)) | num);
            bytes = BitConverter.GetBytes(num3);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}