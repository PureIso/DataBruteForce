using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using oCryptio;
using oCryptio.Checksum;
using System.Windows.Forms;
using PortableLib;
using System.Net.Sockets;
using System.Net;

namespace oCryptoBruteForce
{
    public partial class MainForm : Form
    {
        public event DelegateObjectDelegate OnDelegateObject;

        #region Fields
        private string _fileName;
        private byte[] _fileBuffer;
        private byte[] _fileBase64Buffer;
        private byte[] _possibleChecksumFileBuffer;
        private byte[] _possibleChecksumBase64FileBuffer;
        private bool _stop;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            oDelegateFunctions.SetComboBoxSelectedIndex(lazyGenerateComboBox,0);
            oDelegateFunctions.SetComboBoxSelectedIndex(lazySearchComboBox, 0);
            oDelegateFunctions.SetComboBoxSelectedIndex(checksumComboBox, 0);
        }

        #region Open File and Load
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = "File Names|*.*"};
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _fileName = openFileDialog.FileNames[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            _fileBase64Buffer = Convert.FromBase64String(File.ReadAllText(_fileName));
            Console.WriteLine(_fileBase64Buffer[_fileBase64Buffer.Length - 4]);
            Console.WriteLine(_fileBase64Buffer[_fileBase64Buffer.Length - 3]);
            Console.WriteLine(_fileBase64Buffer[_fileBase64Buffer.Length - 2]);
            Console.WriteLine(_fileBase64Buffer[_fileBase64Buffer.Length - 1]);
            //Thread Safe Functions to prevent illegal cross threads.
            oDelegateFunctions.SetControlText(fileLocationTextBox, _fileName);
            oDelegateFunctions.SetControlText(stopAtPositionTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetNumericUpDownValues(skipBytesNumericUpDown, 1, _fileBuffer.Length,1);
        }

        private void loadPossibleChecksumFileButton_Click(object sender, EventArgs e)
        {
            //In the case where the possible key / checksum might be in a different file
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = "File Names|*.*"};
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            oDelegateFunctions.SetControlText(possibleChecksumFileLocationTextBox, openFileDialog.FileNames[0]);
            _possibleChecksumFileBuffer = File.ReadAllBytes(openFileDialog.FileNames[0]);
            _possibleChecksumBase64FileBuffer = Convert.FromBase64String(File.ReadAllText(openFileDialog.FileNames[0]));
        }
        #endregion

        #region Configuration
        private void lazySearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lazySearchComboBox.Text == "OFF")
            {
                //Using Lazy Search you can skip the specified amount of bytes
                oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown, true);
                oDelegateFunctions.SetEnableControl(byteSkippingCheckBox, true);
            }
            else
            {
                //This search is a lot slower since you are searching and comparing byte by byte
                oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown, false);
                oDelegateFunctions.SetEnableControl(byteSkippingCheckBox, false);
            }
        }

        private void byteSkippingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown,
                oDelegateFunctions.GetEnableControl(byteSkippingCheckBox));
        }

        #endregion
       
        #region Helper Functions
        private DelegateObject GetSearchTypeEnum(DelegateObject workObject)
        {
            try
            {
                bool isLazySearch = oDelegateFunctions.GetControlText(lazySearchComboBox) == "ON";
                bool isLazyGenerate = oDelegateFunctions.GetControlText(lazyGenerateComboBox) == "ON";

                if (!isLazySearch && !isLazyGenerate)
                {
                    if (_possibleChecksumFileBuffer != null)
                    {
                        workObject.SearchType = SearchTypeEnum.NotLazyGenerateNotLazySearch;
                        workObject.PossibleChecksumsArray = _possibleChecksumFileBuffer;
                    }
                    else
                    {
                        workObject.SearchType = SearchTypeEnum.NotLazyGenerateNotLazySearch;
                    }
                }
                else if (isLazySearch && isLazyGenerate)
                {
                    workObject.SearchType = SearchTypeEnum.LazyGenerateLazySearch;
                }
                else if (!isLazySearch)
                {
                    workObject.SearchType = SearchTypeEnum.LazyGenerateNotLazySearch;
                }
                else
                {
                    workObject.SearchType = SearchTypeEnum.NotLazyGenerateLazySearch;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return workObject;
        }

        private static byte[] GenerateChecksum(string checksum, int offset, byte[] buffer)
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
                    returnValue = Crc32.Compute(offset, buffer);
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

        private static int GetChecksumLength(string checksum)
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

        private DelegateObject CreateDelegateObject()
        {
            DelegateObject workObject = new DelegateObject();
            try
            {
                workObject.StartSearch = int.Parse(oDelegateFunctions.GetControlText(startSearchTextBox));
                workObject.StopSearchAt = int.Parse(oDelegateFunctions.GetControlText(stopAtPositionTextBox));
                workObject.StartGeneratedChecksumFrom = int.Parse(oDelegateFunctions.GetControlText(startChecksumPositionTextBox));
                workObject.ChecksumType = oDelegateFunctions.GetControlText(checksumComboBox);
                workObject = GetSearchTypeEnum(workObject);
                workObject.ChecksumLength = GetChecksumLength(workObject.ChecksumType);
                workObject.UseParallelComputation = oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox);
                workObject.ConvertFromBase64String = oDelegateFunctions.GetCheckBoxCheck(convertFromBase64StringCheckBox);
                if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                    workObject.SkipSearchBytesBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
                else workObject.SkipSearchBytesBy = 1;
                workObject.DataArray = _fileBuffer;
                workObject.DataArrayBase64 = _fileBase64Buffer;
                workObject.PossibleChecksumsArray = _possibleChecksumFileBuffer;
                workObject.PossibleChecksumsBase64Array = _possibleChecksumBase64FileBuffer;
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return workObject;
        }
        #endregion

        #region Remote Functions
        private void OnStartWork(DelegateObject searchInformationObject)
        {
            if (searchInformationObject.PossibleChecksumsArray != null)
            {
                OnSearchAndGenerateUsingPossibleChecksumFile(searchInformationObject);
            }
            else
            {
                if (searchInformationObject.UseParallelComputation) OnParallelSearchAndGenerate(searchInformationObject);
                else OnSearchAndGenerate(searchInformationObject);
            }
        }

        private void OnSearchAndGenerateUsingPossibleChecksumFile(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenIndex = input.StartGeneratedChecksumFrom; checksumGenIndex < input.StopSearchAt; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenIndex, input.DataArray);
                #region Main Search
                for (int i = input.StartSearch; i < input.PossibleChecksumsArray.Length - 1; )
                {
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (i == input.PossibleChecksumsArray.Length) break;

                        if (input.PossibleChecksumsArray[i] == item)
                        {
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }

                    if (checksumFound != -1) break;

                    #region Search Type to determing next index
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
                #endregion

                if (checksumFound != -1) break;

                #region Checksum Generation
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenIndex += 1;
                        break;
                }
                #endregion
            }

            if (checksumFound == -1) input.FoundChecksum = false;
            else input.FoundChecksum = true;
            input.IsWorkDone = true;
        }

        #region Search and Generate
        private void OnSearchAndGenerate(DelegateObject input)
        {
            int checksumFound = -1;
            input.StartTime = DateTime.Now;
            for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArray);
                input.ChecksumOffset = checksumGenerationIndex;
                input.ChecksumGenerationLength = input.StopSearchAt - checksumGenerationIndex;
                input.Checksum = BitConverter.ToString(checksum).Replace("-",string.Empty);

                for (int i = input.StartSearch; i < input.StopSearchAt - input.ChecksumLength; )
                {
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (input.DataArray[i] == item)
                        {
                            input.ChecksumOffset = i;
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }
                    if (input.IsWorkDone) return;
                    if (checksumFound == -1)
                    {
                        Array.Reverse(checksum);
                        foreach (byte item in checksum)
                        {
                            if (input.DataArray[i] == item)
                            {
                                input.ChecksumOffset = i;
                                checksumFound = i;
                                i++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }
                    }

                    if (checksumFound != -1) break;
                    #region Search Type to determing next index
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.FoundChecksum = false;
                Array.Reverse(input.DataArray);
                OnSearchAndGenerateReverse(input);
            }
            else
            {
                input.FoundChecksum = true;
            }
            input.EndTime = DateTime.Now;
            input.IsWorkDone = true;
        }

        private void OnSearchAndGenerateReverse(DelegateObject input)
        {
            int checksumFound = -1;
            input.StartTime = DateTime.Now;
            for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArray);
                input.ChecksumOffset = checksumGenerationIndex;
                input.ChecksumGenerationLength = input.StopSearchAt - checksumGenerationIndex;
                input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                for (int i = input.StartSearch; i < input.StopSearchAt - input.ChecksumLength; )
                {
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (input.DataArray[i] == item)
                        {
                            input.ChecksumOffset = i;
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }
                    if (input.IsWorkDone) return;
                    if (checksumFound == -1)
                    {
                        Array.Reverse(checksum);
                        foreach (byte item in checksum)
                        {
                            if (input.DataArray[i] == item)
                            {
                                input.ChecksumOffset = i;
                                checksumFound = i;
                                i++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }
                    }

                    if (checksumFound != -1) break;
                    #region Search Type to determing next index
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.FoundChecksum = false;
                if (input.ConvertFromBase64String)
                {
                    OnSearchAndGenerateBase64(input);
                }
            }
            else
            {
                input.FoundChecksum = true;
            }
            input.EndTime = DateTime.Now;
            input.IsWorkDone = true;
        }

        private void OnSearchAndGenerateBase64(DelegateObject input)
        {
            int checksumFound = -1;
            input.StartTime = DateTime.Now;
            for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64.Length; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64);
                input.ChecksumOffset = checksumGenerationIndex;
                input.ChecksumGenerationLength = input.DataArrayBase64.Length - checksumGenerationIndex;
                input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                if (input.DataArrayBase64.Length - 4 == checksumGenerationIndex)
                {
                    Console.WriteLine(checksum[0]);
                    Console.WriteLine(checksum[1]);
                    Console.WriteLine(checksum[2]);
                    Console.WriteLine(checksum[3]);
                }

                for (int i = input.StartSearch; i < input.DataArrayBase64.Length; )
                {
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (input.DataArray[i] == item)
                        {
                            input.ChecksumOffset = i;
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }
                    if (input.IsWorkDone) return;
                    if (checksumFound == -1)
                    {
                        Array.Reverse(checksum);
                        foreach (byte item in checksum)
                        {
                            if (input.DataArray[i] == item)
                            {
                                input.ChecksumOffset = i;
                                checksumFound = i;
                                i++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }
                    }

                    if (checksumFound != -1) break;
                    #region Search Type to determing next index
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.FoundChecksum = false;
                Array.Reverse(input.DataArrayBase64);
                OnSearchAndGenerateBase64Reverse(input);
            }
            else input.FoundChecksum = true;
        }

        private void OnSearchAndGenerateBase64Reverse(DelegateObject input)
        {
            int checksumFound = -1;
            input.StartTime = DateTime.Now;
            for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64.Length; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64);
                input.ChecksumOffset = checksumGenerationIndex;
                input.ChecksumGenerationLength = input.DataArrayBase64.Length - checksumGenerationIndex;
                input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                if (input.DataArrayBase64.Length - 4 == checksumGenerationIndex)
                {
                    Console.WriteLine(checksum[0]);
                    Console.WriteLine(checksum[1]);
                    Console.WriteLine(checksum[2]);
                    Console.WriteLine(checksum[3]);
                }

                for (int i = input.StartSearch; i < input.DataArrayBase64.Length; )
                {
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (input.DataArray[i] == item)
                        {
                            input.ChecksumOffset = i;
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }
                    if (input.IsWorkDone) return;
                    if (checksumFound == -1)
                    {
                        Array.Reverse(checksum);
                        foreach (byte item in checksum)
                        {
                            if (input.DataArray[i] == item)
                            {
                                input.ChecksumOffset = i;
                                checksumFound = i;
                                i++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }
                    }

                    if (checksumFound != -1) break;
                    #region Search Type to determing next index
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1) input.FoundChecksum = false;
            else input.FoundChecksum = true;
        }
        #endregion

        private void OnParallelSearchAndGenerate(DelegateObject input)
        {
            Parallel.For(input.StartGeneratedChecksumFrom, input.StopSearchAt, checksumGenerationIndex =>
            {
                if (input.IsWorkDone) return;
                byte[] checksum = GenerateChecksum(input.ChecksumType, checksumGenerationIndex, _fileBuffer);
                for (int i = input.StartSearch; i < input.StopSearchAt - input.ChecksumLength; )
                {
                    int checksumFound = -1;
                    if (input.IsWorkDone) return;
                    foreach (byte item in checksum)
                    {
                        if (input.DataArray[i] == item)
                        {
                            checksumFound = i;
                            i++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }

                    if (checksumFound != -1) break;

                    #region Search Type to determing next index

                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += input.SkipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
                }
            });
        }
        #endregion

        #region Server Mode
        private bool _isListening;
        private int _listeningPort;
        private void startListeningButton_Click(object sender, EventArgs e)
        {
            if (_isListening) return;
            try
            {
                _listeningPort = int.Parse(listeningTextBox.Text);
                Thread oThread = new Thread(ListeningThread) { IsBackground = true };
                oThread.Start();
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ListeningThread()
        {
            _isListening = true;
            SetInformationTextListening("Started listening on port: " + _listeningPort);
            AsynchronousSocketListener.OnSearchAndGenerateUsingPossibleChecksumFileEvent
                += OnSearchAndGenerateUsingPossibleChecksumFile;
            AsynchronousSocketListener.OnSearchAndGenerate
                += OnSearchAndGenerate;
            AsynchronousSocketListener.OnParallelSearchAndGenerate
                += OnParallelSearchAndGenerate;
            AsynchronousSocketListener.OnSetTextToInfo
                += SetInformationTextListening;
            AsynchronousSocketListener.StartListening(_listeningPort);
            SetInformationTextListening("Stopped listening on port: " + _listeningPort);
            _isListening = false;
        }

        private void stopListeningButton_Click(object sender, EventArgs e)
        {
            if (!_isListening) return;
            AsynchronousSocketListener.Listening = false;
        }

        private void SetInformationTextListening(string text)
        {
            oDelegateFunctions.SetInformationText(infoTextBox, text);
        }
        #endregion

        #region Client
        //Connect to a server and get the server to do some calculations for you
        //The server should be listening 
        private TcpClient _server;
        private void addServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(serverIPTextBox.Text)) throw new Exception("Server IP Address is null.");
                if (String.IsNullOrEmpty(serverPortTextBox.Text)) throw new Exception("Server Port is null.");
                int port = int.Parse(serverPortTextBox.Text);   //redundant port variable, just to validate
                ListViewItem item = new ListViewItem(new[]
                {
                    "Offline",                          //Status
                    "",                                 //Worker ID
                    serverIPTextBox.Text,               //Server IP Address
                    serverPortTextBox.Text              //Server Port
                });
                SetClientInformationText("Server Added: " + item.SubItems[2] + " - " + item.SubItems[3]);
                serverMonitorListView.Items.Add(item);
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //private void connectToServerButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!Connect(serverIPTextBox.Text, port)) throw new Exception("Could not connect to Server.");
        //        oDelegateFunctions.SetEnableControl(serverIPTextBox, false);
        //        oDelegateFunctions.SetEnableControl(serverPortTextBox, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        //private void disconnectFromServerButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(serverIPTextBox.Text)) throw new Exception("Server IP Address is null.");
        //        if (String.IsNullOrEmpty(serverPortTextBox.Text)) throw new Exception("Server Port is null.");
        //        int port = int.Parse(serverPortTextBox.Text);
        //        if (!Connect(serverIPTextBox.Text, port)) throw new Exception("Could not connect to Server.");
        //        oDelegateFunctions.SetEnableControl(serverIPTextBox, false);
        //        oDelegateFunctions.SetEnableControl(serverPortTextBox, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}

        private bool Connect(string ip, int port)
        {
            try
            {
                _server = new TcpClient();
                _server.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                return true;
            }
            catch (Exception ex)
            {
                SetClientInformationText("Server Connection Exception: " + ex);
                return false;
            }
        }

        private void SetClientInformationText(string text)
        {
            oDelegateFunctions.SetInformationText(clientInformationTextBox, text);
        }
        #endregion

        #region Mouse Down Events
        private int _selectedWorkMonitorItemIndex;
        private int _selectedServerMonitorItemIndex;
        private void workMonitorListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem selectedItem = workMonitorListView.GetItemAt(e.X, e.Y);
            if( selectedItem == null)return;
            _selectedWorkMonitorItemIndex = selectedItem.Index;
            Point screenPoint = workMonitorListView.PointToScreen(e.Location);
            workContextMenuStrip.Show(screenPoint);
        }

        private void serverMonitorListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem selectedItem = serverMonitorListView.GetItemAt(e.X, e.Y);
            if (selectedItem == null) return;
            _selectedServerMonitorItemIndex = selectedItem.Index;
            Point screenPoint = serverMonitorListView.PointToScreen(e.Location);
            clientModeContextMenuStrip.Show(screenPoint);
        }
        #endregion

        #region Work Monitor
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        private readonly List<DelegateObject> _listOfWorkObjects = new List<DelegateObject>(); 
        private void addWorkButton_Click(object sender, EventArgs e)
        {
            try
            {
                Random random = new Random();
                string workId = "";
                DelegateObject delegateObject = CreateDelegateObject();
                List<string> workIdList = _listOfWorkObjects
                    .Select(worker => worker.WorkerId).ToList();

                while (workIdList.Contains(workId) || workId == "")
                {
                    workId = new string(Enumerable.Repeat(Characters, 5)
                        .Select(s => s[random.Next(s.Length)]).ToArray());
                }
                delegateObject.WorkerId = workId;
                delegateObject.IsWorkDone = false;
                //Adding Event Handlers
                delegateObject.OnStatusChange += WorkMonitorStatusChangeEventHandler;

                ListViewItem item = new ListViewItem(new[]
                {
                    "Idle",                         //Status
                    workId,                         //Worker ID
                    "",                             //Security
                    "",                             //Checksum
                    "",                             //Checksum Offset
                    "",                             //Generation Length
                    "",                             //Start Time
                    "",                             //End Time
                });

                _listOfWorkObjects.Add(delegateObject);
                workMonitorListView.Items.Add(item);

                //Clear
                oDelegateFunctions.SetControlText(startSearchTextBox,"0");
                oDelegateFunctions.SetControlText(stopAtPositionTextBox,"0");
                oDelegateFunctions.SetControlText(startChecksumPositionTextBox, "0");
                oDelegateFunctions.SetComboBoxSelectedIndex(checksumComboBox, 0);
                _fileBuffer = null;
                _possibleChecksumFileBuffer = null;
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].IsWorkDone = false;
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].FoundChecksum = false;
            ThreadPool.QueueUserWorkItem(x => OnStartWork(_listOfWorkObjects[_selectedWorkMonitorItemIndex]));
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].IsWorkDone = true;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < workMonitorListView.Items.Count; i++)
            {
                if (workMonitorListView.Items[i].SubItems.Count > 0)
                {
                    //Worker Id
                    if (workMonitorListView.Items[i].SubItems[1].Text ==
                        _listOfWorkObjects[_selectedWorkMonitorItemIndex].WorkerId)
                    {
                        workMonitorListView.Items.RemoveAt(i);
                        _listOfWorkObjects.RemoveAt(_selectedWorkMonitorItemIndex);
                    }
                }
            }
        }
        #endregion

        #region Drag Drop Panel
        private void dragAndDropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData((DataFormats.FileDrop));
            _fileName = files[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            //Thread Safe Functions to prevent illegal cross threads.
            oDelegateFunctions.SetControlText(fileLocationTextBox, _fileName);
            oDelegateFunctions.SetControlText(stopAtPositionTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetNumericUpDownValues(skipBytesNumericUpDown, 1, _fileBuffer.Length, 1);
        }

        private void dragAndDropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }
        #endregion

        private void WorkMonitorStatusChangeEventHandler(DelegateObject sender)
        {
            if (workMonitorListView.InvokeRequired)
                workMonitorListView.Invoke((MethodInvoker)delegate {
                    WorkMonitorStatusChangeEventHandler(sender);
                });
            else
            {
                for (int i = 0; i < workMonitorListView.Items.Count; i++)
                {
                    if (workMonitorListView.Items[i].SubItems.Count > 0)
                    {
                        //Worker Id
                        if (workMonitorListView.Items[i].SubItems[1].Text == sender.WorkerId)
                        {
                            //Status
                            if (sender.IsWorkDone == true) workMonitorListView.Items[i].SubItems[0].Text = "Done";
                            else workMonitorListView.Items[i].SubItems[0].Text = "Searching";
                            //Result
                            if (sender.FoundChecksum == true) 
                            {
                                workMonitorListView.Items[i].SubItems[2].Text = "Found";
                                //Checksum
                                workMonitorListView.Items[i].SubItems[3].Text = sender.Checksum;
                                //Checksum Offset
                                workMonitorListView.Items[i].SubItems[4].Text = sender.ChecksumOffset.ToString();
                                //Generation Length
                                workMonitorListView.Items[i].SubItems[5].Text = sender.ChecksumGenerationLength.ToString();
                                //Start Time
                                workMonitorListView.Items[i].SubItems[6].Text = sender.StartTime.ToString();
                                //End Time
                                workMonitorListView.Items[i].SubItems[7].Text = sender.EndTime.ToString();
                            }
                            else workMonitorListView.Items[i].SubItems[2].Text = "Unknown";                 
                        }
                    }
                }
            }
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
