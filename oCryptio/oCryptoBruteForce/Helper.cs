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
                    if (eof == -1) returnValue = Adler8.Compute(offset, buffer);
                    else returnValue = Adler8.Compute(offset, buffer, eof);
                    break;
                case "Adler16 - {2Bytes}":
                    if (eof == -1) returnValue = Adler16.Compute(offset, buffer);
                    else returnValue = Adler16.Compute(offset, buffer, eof);
                    break;
                case "Adler32 - {4Bytes}":
                    if (eof == -1) returnValue = Adler32.Compute(offset, buffer);
                    else returnValue = Adler32.Compute(offset, buffer, eof);
                    break;
                case "Checksum8 - {1Bytes}":
                    if (eof == -1) returnValue = Checksum8.Compute(offset, buffer);
                    else returnValue = Checksum8.Compute(offset, buffer, eof);
                    break;
                case "Checksum16 - {2Bytes}":
                    if (eof == -1) returnValue = Checksum16.Compute(offset, buffer);
                    else returnValue = Checksum16.Compute(offset, buffer, eof);
                    break;
                case "Checksum24 - {3Bytes}":
                    if (eof == -1) returnValue = Checksum24.Compute(offset, buffer);
                    else returnValue = Checksum24.Compute(offset, buffer, eof);
                    break;
                case "Checksum32 - {4Bytes}":
                    if (eof == -1) returnValue = Checksum32.Compute(offset, buffer);
                    else returnValue = Checksum32.Compute(offset, buffer, eof);
                    break;
                case "Checksum40 - {5Bytes}":
                    if (eof == -1) returnValue = Checksum40.Compute(offset, buffer);
                    else returnValue = Checksum40.Compute(offset, buffer, eof);
                    break;
                case "Checksum48 - {6Bytes}":
                    if (eof == -1) returnValue = Checksum48.Compute(offset, buffer);
                    else returnValue = Checksum48.Compute(offset, buffer, eof);
                    break;
                case "Checksum56 - {7Bytes}":
                    if (eof == -1) returnValue = Checksum56.Compute(offset, buffer);
                    else returnValue = Checksum56.Compute(offset, buffer, eof);
                    break;
                case "Checksum64 - {8Bytes}":
                    if (eof == -1) returnValue = Checksum64.Compute(offset, buffer);
                    else returnValue = Checksum64.Compute(offset, buffer, eof);
                    break;
                case "CRC16 - {2Bytes}":
                    Crc16 crc16 = new Crc16();
                    if (eof == -1) returnValue = crc16.Compute(offset, buffer);
                    else returnValue = crc16.Compute(offset, buffer, eof);
                    break;
                case "CRC16 CCITT - {2Bytes}":
                    Crc16ccitt crc16Ccitt = new Crc16ccitt();
                    if (eof == -1) returnValue = crc16Ccitt.Compute(offset, buffer);
                    else returnValue = crc16Ccitt.Compute(offset, buffer, eof);
                    break;
                case "CRC32 - {4Bytes}":
                    if (eof == -1) returnValue = Crc32.Compute(offset, buffer);
                    else returnValue = Crc32.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 1 (128)  - {16Bytes}":
                    if (eof == -1) returnValue = HmacSha1.Compute(offset, buffer);
                    else returnValue = HmacSha1.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 256 - {32Bytes}":
                    if (eof == -1) returnValue = HmacSha256.Compute(offset, buffer);
                    else returnValue = HmacSha256.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 384 - {48Bytes}":
                    if (eof == -1) returnValue = HmacSha384.Compute(offset, buffer);
                    else returnValue = HmacSha384.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 512 - {64Bytes}":
                    if (eof == -1) returnValue = HmacSha512.Compute(offset, buffer);
                    else returnValue = HmacSha512.Compute(offset, buffer, eof);
                    break;
                case "MD5 - {16Bytes}":
                    if (eof == -1) returnValue = Md5.Compute(offset, buffer);
                    else returnValue = Md5.Compute(offset, buffer, eof);
                    break;
                case "MD5 CNG - {16Bytes}":
                    if (eof == -1) returnValue = Md5Cng.Compute(offset, buffer);
                    else returnValue = Md5Cng.Compute(offset, buffer, eof);
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
