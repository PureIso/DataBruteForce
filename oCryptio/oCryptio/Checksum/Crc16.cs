using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptio.Checksum
{
    public class Crc16
    {
        private ushort CRC_InitialValue;
        private ushort CRC_Polynomial;
        private ushort[] CRC_Table;

        public Crc16()
        {
            int num5;
            CRC_Table = new ushort[0x100];
            CRC_Polynomial = 0xa001;
            CRC_InitialValue = 0;
            int index = 0;
            do
            {
                ushort num2 = 0;
                ushort num = (ushort)index;
                int num4 = 0;
                do
                {
                    if (((num2 ^ num) & 1) > 0)
                    {
                        num2 = (ushort)(((ushort)(num2 >> 1)) ^ CRC_Polynomial);
                    }
                    else
                    {
                        num2 = (ushort)(num2 >> 1);
                    }
                    num = (ushort)(num >> 1);
                    num4++;
                    num5 = 7;
                }
                while (num4 <= num5);
                CRC_Table[index] = num2;
                index++;
                num5 = 0xff;
            }
            while (index <= num5);

        }
        public byte[] Compute(byte[] bytes)
        {
            ushort num = CRC_InitialValue;
            foreach (byte num4 in bytes)
            {
                ushort num3 = (ushort)(0xff & num4);
                ushort num2 = (ushort)(num ^ num3);
                num = (ushort)(((ushort)(num >> 8)) ^ CRC_Table[num2 & 0xff]);
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }
    }
}
