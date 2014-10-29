using oCryptio;
using oCryptio.Checksum;
using System;

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
                    returnValue = eof == -1 ? Adler8.Compute(offset, buffer) : Adler8.Compute(offset, buffer, eof);
                    break;
                case "Adler16 - {2Bytes}":
                    returnValue = eof == -1 ? Adler16.Compute(offset, buffer) : Adler16.Compute(offset, buffer, eof);
                    break;
                case "Adler32 - {4Bytes}":
                    returnValue = eof == -1 ? Adler32.Compute(offset, buffer) : Adler32.Compute(offset, buffer, eof);
                    break;
                case "Checksum8 - {1Bytes}":
                    returnValue = eof == -1 ? Checksum8.Compute(offset, buffer) : Checksum8.Compute(offset, buffer, eof);
                    break;
                case "Checksum16 - {2Bytes}":
                    returnValue = eof == -1 ? Checksum16.Compute(offset, buffer) : Checksum16.Compute(offset, buffer, eof);
                    break;
                case "Checksum24 - {3Bytes}":
                    returnValue = eof == -1 ? Checksum24.Compute(offset, buffer) : Checksum24.Compute(offset, buffer, eof);
                    break;
                case "Checksum32 - {4Bytes}":
                    returnValue = eof == -1 ? Checksum32.Compute(offset, buffer) : Checksum32.Compute(offset, buffer, eof);
                    break;
                case "Checksum40 - {5Bytes}":
                    returnValue = eof == -1 ? Checksum40.Compute(offset, buffer) : Checksum40.Compute(offset, buffer, eof);
                    break;
                case "Checksum48 - {6Bytes}":
                    returnValue = eof == -1 ? Checksum48.Compute(offset, buffer) : Checksum48.Compute(offset, buffer, eof);
                    break;
                case "Checksum56 - {7Bytes}":
                    returnValue = eof == -1 ? Checksum56.Compute(offset, buffer) : Checksum56.Compute(offset, buffer, eof);
                    break;
                case "Checksum64 - {8Bytes}":
                    returnValue = eof == -1 ? Checksum64.Compute(offset, buffer) : Checksum64.Compute(offset, buffer, eof);
                    break;
                case "CRC16 - {2Bytes}":
                    Crc16 crc16 = new Crc16();
                    returnValue = eof == -1 ? crc16.Compute(offset, buffer) : crc16.Compute(offset, buffer, eof);
                    break;
                case "CRC16 CCITT - {2Bytes}":
                    Crc16ccitt crc16Ccitt = new Crc16ccitt();
                    returnValue = eof == -1 ? crc16Ccitt.Compute(offset, buffer) : crc16Ccitt.Compute(offset, buffer, eof);
                    break;
                case "CRC32 - {4Bytes}":
                    returnValue = eof == -1 ? Crc32.Compute(offset, buffer) : Crc32.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 1 (128)  - {16Bytes}":
                    returnValue = eof == -1 ? HmacSha1.Compute(offset, buffer) : HmacSha1.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 256 - {32Bytes}":
                    returnValue = eof == -1 ? HmacSha256.Compute(offset, buffer) : HmacSha256.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 384 - {48Bytes}":
                    returnValue = eof == -1 ? HmacSha384.Compute(offset, buffer) : HmacSha384.Compute(offset, buffer, eof);
                    break;
                case "HMAC SHA 512 - {64Bytes}":
                    returnValue = eof == -1 ? HmacSha512.Compute(offset, buffer) : HmacSha512.Compute(offset, buffer, eof);
                    break;
                case "MD5 - {16Bytes}":
                    returnValue = eof == -1 ? Md5.Compute(offset, buffer) : Md5.Compute(offset, buffer, eof);
                    break;
                case "MD5 CNG - {16Bytes}":
                    returnValue = eof == -1 ? Md5Cng.Compute(offset, buffer) : Md5Cng.Compute(offset, buffer, eof);
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
                //TODO Add logger to .txt
                throw;
            }
            return checksumLength;
        }
    }
}
