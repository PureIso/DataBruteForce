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
    }
}
