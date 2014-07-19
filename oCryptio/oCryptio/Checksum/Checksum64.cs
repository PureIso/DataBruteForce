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

        public static byte[] Compute(int offset, byte[] Bytes)
        {
            ulong num = 0L;
            for (int index = offset; index < Bytes.Length; index++)
            {
                byte num2 = Bytes[index];
                num = (num + num2) & ulong.MaxValue;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Reverse(Bytes);
            return Bytes;
        }
    }
}
