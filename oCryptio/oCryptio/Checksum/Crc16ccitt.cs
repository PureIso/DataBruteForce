using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptio.Checksum
{
    public class Crc16ccitt
    {
        private ushort CRC_InitialValue;
        private ushort CRC_Polynomial;
        private ushort[] CRC_Table;

        public Crc16ccitt()
        {
           int num5;
           this.CRC_Table = new ushort[0x100];
           this.CRC_Polynomial = 0x1021;
           this.CRC_InitialValue = 0;
           int index = 0;
           do
           {
               ushort num2 = 0;
               ushort num = (ushort) (((ushort) index) << 8);
               int num4 = 0;
               do
               {
                   if (((num2 ^ num) & 0x8000) > 0)
                   {
                       num2 = (ushort) (((ushort) (num2 << 1)) ^ this.CRC_Polynomial);
                   }
                   else
                   {
                       num2 = (ushort) (num2 << 1);
                   }
                   num = (ushort) (num << 1);
                   num4++;
                   num5 = 7;
               }
               while (num4 <= num5);
               this.CRC_Table[index] = num2;
               index++;
               num5 = 0xff;
           }
           while (index <= num5);
        }

        public byte[] Compute(byte[] bytes)
        {
            ushort num = this.CRC_InitialValue;
            foreach (byte num2 in bytes)
            {
                ushort num4 = (ushort)(0xff & num2);
                ushort index = (ushort)(((ushort)(num >> 8)) ^ num4);
                num = (ushort)(((ushort)(num << 8)) ^ this.CRC_Table[index]);
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }

        public byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public byte[] Compute(int offset, byte[] bytes, int eof)
        {
            ushort num = this.CRC_InitialValue;
            for (int i = offset; i < eof; i++)
            {
                byte num2 = bytes[i];
                ushort num4 = (ushort)(0xff & num2);
                ushort index = (ushort)(((ushort)(num >> 8)) ^ num4);
                num = (ushort)(((ushort)(num << 8)) ^ this.CRC_Table[index]);
            }
            bytes = BitConverter.GetBytes(num);
            Array.Reverse(bytes);
            return bytes;
        }
    }


}
