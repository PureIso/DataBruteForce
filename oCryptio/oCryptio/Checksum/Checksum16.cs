using System;
namespace oCryptio.Checksum
{
    public class Checksum16
    {
        public byte[] Compute(byte[] Bytes)
        {
            ushort num = 0;
            foreach (byte num2 in Bytes)
            {
                num = (ushort)(((ushort)(num + num2)) & 0xffff);
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Reverse(Bytes);
            return Bytes;
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            ushort num = 0;
            for (int index = offset; index < eof; index++)
            {
                byte num2 = bytes[index];
                num = (ushort)(((ushort)(num + num2)) & 0xffff);
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
