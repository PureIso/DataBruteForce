using System;

namespace oCryptio.Checksum
{
    public class Checksum64
    {
        public byte[] Compute(byte[] Bytes)
        {
            ulong num = 0L;
            foreach (byte num2 in Bytes)
            {
                num = (num + num2) & ulong.MaxValue;
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
            ulong num = 0L;
            for (int index = offset; index < eof; index++)
            {
                byte num2 = bytes[index];
                num = (num + num2) & ulong.MaxValue;
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
