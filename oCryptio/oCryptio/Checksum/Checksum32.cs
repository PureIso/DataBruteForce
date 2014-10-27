using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptio.Checksum
{
    public class Checksum32
    {
        public byte[] Compute(byte[] Bytes)
        {
            uint num = 0;
            foreach (byte num2 in Bytes)
            {
                num = (num + num2) & 0xffffffff;
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
            uint num = 0;
            for (int index = offset; index < eof; index++)
            {
                byte num2 = bytes[index];
                num = (num + num2) & 0xffffffff;
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
