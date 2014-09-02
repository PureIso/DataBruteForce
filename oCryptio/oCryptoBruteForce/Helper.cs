using oCryptio;
using oCryptio.Checksum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptoBruteForce
{
    public static class Helper
    {
        public static byte[] GenerateChecksum(string checksum, int offset, byte[] buffer, int eof = -1)
        {
            byte[] returnValue = null;
            switch (checksum)
            {
                case "Adler8 - {1Bytes}":
                    returnValue = Adler8.Compute(offset, buffer);
                    break;
                case "Adler16 - {2Bytes}":
                    returnValue = Adler16.Compute(offset, buffer);
                    break;
                case "Adler32 - {4Bytes}":
                    returnValue = Adler32.Compute(offset, buffer);
                    break;
                case "Checksum8 - {1Bytes}":
                    returnValue = Checksum8.Compute(offset, buffer);
                    break;
                case "Checksum16 - {2Bytes}":
                    returnValue = Checksum16.Compute(offset, buffer);
                    break;
                case "Checksum24 - {3Bytes}":
                    returnValue = Checksum24.Compute(offset, buffer);
                    break;
                case "Checksum32 - {4Bytes}":
                    returnValue = Checksum32.Compute(offset, buffer);
                    break;
                case "Checksum40 - {5Bytes}":
                    returnValue = Checksum40.Compute(offset, buffer);
                    break;
                case "Checksum48 - {6Bytes}":
                    returnValue = Checksum48.Compute(offset, buffer);
                    break;
                case "Checksum56 - {7Bytes}":
                    returnValue = Checksum56.Compute(offset, buffer);
                    break;
                case "Checksum64 - {8Bytes}":
                    returnValue = Checksum64.Compute(offset, buffer);
                    break;
                case "CRC16 - {2Bytes}":
                    Crc16 crc16 = new Crc16();
                    returnValue = crc16.Compute(offset, buffer);
                    break;
                case "CRC16 CCITT - {2Bytes}":
                    Crc16ccitt crc16Ccitt = new Crc16ccitt();
                    returnValue = crc16Ccitt.Compute(offset, buffer);
                    break;
                case "CRC32 - {4Bytes}":
                    if (eof == -1) returnValue = Crc32.Compute(offset, buffer);
                    else returnValue = Crc32.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 1 (128)  - {16Bytes}":
                    returnValue = HmacSha1.Compute(offset, buffer);
                    break;
                case "HMAC SHA 256 - {32Bytes}":
                    returnValue = HmacSha256.Compute(offset, buffer);
                    break;
                case "HMAC SHA 384 - {48Bytes}":
                    returnValue = HmacSha384.Compute(offset, buffer);
                    break;
                case "HMAC SHA 512 - {64Bytes}":
                    returnValue = HmacSha512.Compute(offset, buffer);
                    break;
                case "MD5 - {16Bytes}":
                    returnValue = Md5.Compute(offset, buffer);
                    break;
                case "MD5 CNG - {16Bytes}":
                    returnValue = Md5Cng.Compute(offset, buffer);
                    break;
            }
            return returnValue;
        }

        public static int GetChecksumLength(string checksum)
        {
            int checksumLength = -1;
            try
            {
                string[] value = checksum.Split('{', '}');
                switch (value[1].Replace("Bytes", "").Replace("s", ""))
                {
                    case "1":
                        checksumLength = 1;
                        break;
                    case "2":
                        checksumLength = 2;
                        break;
                    case "4":
                        checksumLength = 4;
                        break;
                    case "8":
                        checksumLength = 8;
                        break;
                    case "16":
                        checksumLength = 16;
                        break;
                    case "32":
                        checksumLength = 32;
                        break;
                    case "48":
                        checksumLength = 48;
                        break;
                    case "64":
                        checksumLength = 64;
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return checksumLength;
        }
    }
}
