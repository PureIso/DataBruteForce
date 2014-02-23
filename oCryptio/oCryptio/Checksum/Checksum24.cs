using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
