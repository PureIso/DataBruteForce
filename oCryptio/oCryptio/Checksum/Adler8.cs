namespace oCryptio
{
    public class Adler8
    {
        public byte Compute(byte[] bytes)
        {
            const ushort alderMod = 13;
            ushort num4 = 1;
            ushort num2 = (ushort) (num4 & 0xff);
            ushort num3 = (ushort) (((ushort) (num4 >> 4)) & 0xff);
            foreach (byte num5 in bytes)
            {
                num2 = (ushort) (((ushort) (num2 + num5))%alderMod);
                num3 = (ushort) (((ushort) (num3 + num2))%alderMod);
            }
            num4 = (ushort) (((ushort) (num3 << 4)) | num2);
            return (byte) num4;
        }

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            const ushort alderMod = 13;
            ushort num4 = 1;
            ushort num2 = (ushort)(num4 & 0xff);
            ushort num3 = (ushort)(((ushort)(num4 >> 4)) & 0xff);

            for (int num5 = offset; num5 < eof; num5++)
            {
                num2 = (ushort)(((ushort)(num2 + bytes[num5])) % alderMod);
                num3 = (ushort)(((ushort)(num3 + num2)) % alderMod);
            }
            num4 = (ushort)(((ushort)(num3 << 4)) | num2);
            byte[] returnBufferBytes = new[] { (byte)num4 };
            return returnBufferBytes;
        }
    }
}