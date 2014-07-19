using System;
namespace oCryptio.Checksum
{
    public class Checksum24
    {
        public byte[] Compute(byte[] Bytes)
        {
            int num = 0;
            foreach (byte num2 in Bytes)
            {
                num = (num + num2) & 0xffffff;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref Bytes, 3);
            Array.Reverse(Bytes);
            return Bytes;
        }

        public static byte[] Compute(int offset, byte[] Bytes)
        {
            int num = 0;
            for (int index = offset; index < Bytes.Length; index++)
            {
                byte num2 = Bytes[index];
                num = (num + num2) & 0xffffff;
            }
            Bytes = BitConverter.GetBytes(num);
            Array.Resize<byte>(ref Bytes, 3);
            Array.Reverse(Bytes);
            return Bytes;
        }
    }
}
