using System;

namespace oCryptio.Checksum
{
    public class Checksum40
    {
        public byte[] Compute(byte[] Bytes)
        {
            long num = 0L;
            foreach (byte num2 in Bytes)
            {
                num = (num + num2) & 0xffffffffffL;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref Bytes, 5);
            Array.Reverse(Bytes);
            return Bytes;
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            long num = 0L;
            for (int index = offset; index < eof; index++)
            {
                byte num2 = bytes[index];
                num = (num + num2) & 0xffffffffffL;
            }
            bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref bytes, 5);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
