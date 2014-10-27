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

        public static byte[] Compute(int offset, byte[] bytes)
        {
            return Compute(offset, bytes, bytes.Length);
        }

        public static byte[] Compute(int offset, byte[] bytes, int eof)
        {
            byte num2 = 0;
            for (int index = offset; index < eof; index++)
            {
                byte num3 = bytes[index];
                num2 = (byte)(((byte)(num2 + num3)) & 0xff);
            }
            return new[] { num2 };
        }
    }
}
