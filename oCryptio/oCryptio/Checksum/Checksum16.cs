using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
