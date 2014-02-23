#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using oCryptio.Checksum;

#endregion

namespace oCryptio
{
    public class oLibFile
    {
        private readonly List<byte[]> _bufferList;
        private readonly List<string> _filenames;
        private readonly int _size;

        public oLibFile()
        {
        }

        public oLibFile(ICollection<string> files)
        {
            _bufferList = new List<byte[]>(files.Count);
            _filenames = new List<string>(files.Count);
            foreach (string file in files)
            {
                _filenames.Add(Path.GetFileName(file));
                byte[] buffer = File.ReadAllBytes(file);
                _bufferList.Add(buffer);
                _size += buffer.Length + 4 + 32;
            }
        }

        public bool Save(string fileName)
        {
            try
            {
                string pathString = Path.ChangeExtension(fileName, ".OLIBFILE");

                if (File.Exists(pathString)) File.Delete(pathString);
                using (FileStream fileStream = File.Create(pathString))
                {
                    //Header 0 Length 16
                    //Checksum 16 Length 4
                    //File Count 32 Length 4

                    //Main 40 
                    //ASCII filename Length 32
                    //File Data Length 4
                    //File Data

                    byte[] headerDescriptor = new byte[_size + 40];
                    fileStream.Write(headerDescriptor, 0, headerDescriptor.Length);

                    byte[] headerToWrite = Encoding.ASCII.GetBytes("OLIBFILE");
                    Buffer.BlockCopy(
                        headerToWrite,
                        0,
                        headerDescriptor,
                        0,
                        headerToWrite.Length);
                    fileStream.Position = 0;
                    fileStream.Write(headerDescriptor, 0, 16);

                    int position = 32;
                    fileStream.Position = position;
                    byte[] fileCountByte = BitConverter.GetBytes(_filenames.Count);
                    fileStream.Write(fileCountByte, 0, fileCountByte.Length);

                    position = 40;
                    int fileIndex = 0;
                    foreach (byte[] fileBuffer in _bufferList)
                    {
                        byte[] fileNameByte = Encoding.ASCII.GetBytes(_filenames[fileIndex]);
                        fileIndex++;

                        fileStream.Position = position;
                        fileStream.Write(fileNameByte, 0, fileNameByte.Length);
                        position += 32;

                        fileStream.Position = position;
                        byte[] lengthByte = BitConverter.GetBytes(fileBuffer.Length);
                        fileStream.Write(lengthByte, 0, lengthByte.Length);
                        position += 4;

                        fileStream.Position = position;
                        fileStream.Write(fileBuffer, 0, fileBuffer.Length);
                        position += fileBuffer.Length;
                    }

                    byte[] bufferToGenerateChecksum = new byte[_size];
                    fileStream.Position = 40;
                    fileStream.Read(bufferToGenerateChecksum, 0, bufferToGenerateChecksum.Length);

                    byte[] checksum = Crc32.Compute(bufferToGenerateChecksum);
                    fileStream.Position = 16;
                    fileStream.Write(checksum, 0, 4);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Unload(string directoryPath, string fileName)
        {
            try
            {
                if (Path.GetExtension(fileName) != ".OLIBFILE") return false;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 0;
                    byte[] headerBytes = new byte[16];
                    fileStream.Read(headerBytes, 0, headerBytes.Length);
                    string header = Encoding.ASCII.GetString(headerBytes).Replace("\0", "");
                    if (header != "OLIBFILE") return false;

                    fileStream.Position = 40;
                    byte[] checksumCalculateBytes = new byte[fileStream.Length - 40];
                    fileStream.Read(checksumCalculateBytes, 0, checksumCalculateBytes.Length);
                    byte[] getChecksum = Crc32.Compute(checksumCalculateBytes);

                    fileStream.Position = 16;
                    byte[] checksumFileBytes = new byte[4];
                    fileStream.Read(checksumFileBytes, 0, checksumFileBytes.Length);
                    if (getChecksum.ToString() != checksumFileBytes.ToString()) return false;

                    fileStream.Position = 32;
                    byte[] fileCountBytes = new byte[4];
                    fileStream.Read(fileCountBytes, 0, fileCountBytes.Length);
                    int fileCount = BitConverter.ToInt32(fileCountBytes, 0);

                    int position = 40;
                    fileStream.Position = position;
                    for (int i = 0; i < fileCount; i++)
                    {
                        //File Name
                        //File Length
                        //Data
                        byte[] fileNameBytes = new byte[32];
                        fileStream.Read(fileNameBytes, 0, fileNameBytes.Length);
                        string fileNameString = Encoding.ASCII.GetString(fileNameBytes).Replace("\0", "");
                        position += 32;

                        fileStream.Position = position;
                        byte[] fileLengthBytes = new byte[4];
                        fileStream.Read(fileLengthBytes, 0, fileLengthBytes.Length);
                        int fileLengthInt = BitConverter.ToInt32(fileLengthBytes, 0);
                        position += 4;

                        fileStream.Position = position;
                        byte[] fileDataBytes = new byte[fileLengthInt];
                        fileStream.Read(fileDataBytes, 0, fileDataBytes.Length);

                        using (FileStream innerFileStream = File.Create(directoryPath + "\\" + fileNameString))
                        {
                            innerFileStream.Write(fileDataBytes, 0, fileDataBytes.Length);
                        }
                        position += fileLengthInt;
                        fileStream.Position = position;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VerifyChecksum(string fileName)
        {
            try
            {
                if (Path.GetExtension(fileName) != ".OLIBFILE") return false;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Position = 40;
                    byte[] checksumCalculateBytes = new byte[fileStream.Length - 40];
                    fileStream.Read(checksumCalculateBytes, 0, checksumCalculateBytes.Length);
                    byte[] getChecksum = Crc32.Compute(checksumCalculateBytes);

                    fileStream.Position = 16;
                    byte[] checksumFileBytes = new byte[4];
                    fileStream.Read(checksumFileBytes, 0, checksumFileBytes.Length);
                    return getChecksum.ToString() == checksumFileBytes.ToString();
                }
            }
            catch
            {
                return false;
            }
        }
    }
}