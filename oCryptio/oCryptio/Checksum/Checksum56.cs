using System;

namespace oCryptio.Checksum
{
    public class Checksum56
    {
        public byte[] Compute(byte[] Bytes)
        {
            long num = 0L;
            foreach (byte num2 in Bytes)
            {
                num = (num + num2) & 0xffffffffffffffL;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref Bytes, 7);
            Array.Reverse(Bytes);
            return Bytes;
        }

        public static byte[] Compute(int offset, byte[] Bytes)
        {
            long num = 0L;
            for (int index = offset; index < Bytes.Length; index++)
            {
                byte num2 = Bytes[index];
                num = (num + num2) & 0xffffffffffffffL;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref Bytes, 7);
            Array.Reverse(Bytes);
            return Bytes;
        }
    }
}
