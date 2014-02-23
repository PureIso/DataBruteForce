using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
