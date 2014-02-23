using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptio.Checksum
{
    public class Checksum8
    {
        public byte Compute(byte[] Bytes)
        {
            byte num2 = 0;
            foreach (byte num3 in Bytes)
            {
                num2 = (byte)(((byte)(num2 + num3)) & 0xff);
            }
            return num2;
        }

    }
}
