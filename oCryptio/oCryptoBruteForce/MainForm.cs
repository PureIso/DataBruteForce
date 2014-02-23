using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using oCryptio;
using oCryptio.Checksum;
using System.Windows.Forms;
using oCryptio.Encryption;
using PortableLib;

namespace oCryptoBruteForce
{
    public partial class MainForm : Form
    {
        private string _fileName;
        private byte[] _fileBuffer;
        private byte[] _possibleChecksumFileBuffer;
        private bool _stop;

        public MainForm()
        {
            InitializeComponent();
            oDelegateFunctions.SetComboBoxSelectedIndex(lazyGenerateComboBox,0);
            oDelegateFunctions.SetComboBoxSelectedIndex(lazySearchComboBox, 0);
            oDelegateFunctions.SetComboBoxSelectedIndex(checksumComboBox, 0);
        }

        #region Handlers
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File Names|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _fileName = openFileDialog.FileNames[0];
            _fileBuffer = File.ReadAllBytes(_fileName);

            oDelegateFunctions.SetControlText(fileLocationTextBox, _fileName);
            oDelegateFunctions.SetControlText(fileLengthTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetControlText(stopAtPositionTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetEnableControl(startSearchButton,true);
            oDelegateFunctions.SetNumericUpDownValues(skipBytesNumericUpDown, 1, _fileBuffer.Length,1);
        }

        private void loadPossibleChecksumFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File Names|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            oDelegateFunctions.SetControlText(possibleChecksumFileLocationTextBox, openFileDialog.FileNames[0]);
            oDelegateFunctions.CheckCheckBoxButton(usePCFCheckBox, true);
            _possibleChecksumFileBuffer = File.ReadAllBytes(openFileDialog.FileNames[0]);
        }

        private void startSearchButton_Click(object sender, EventArgs e)
        {
            _stop = false;
            Thread oThread = new Thread(SearchWorkThread) {IsBackground = true};
            oThread.IsBackground = true;
            oThread.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopClearFunction();
            _stop = true;
        }

        private void lazySearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lazySearchComboBox.Text == "OFF")
            {
                oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown, true);
                oDelegateFunctions.SetEnableControl(byteSkippingCheckBox, true);
            }
            else
            {
                oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown, false);
                oDelegateFunctions.SetEnableControl(byteSkippingCheckBox, false);
            }

        }
        #endregion

        #region Functions
        private void SearchWorkThread()
        {
            StartClearFunction();

            int startSearch = int.Parse(oDelegateFunctions.GetControlText(startSearchTextBox));
            int stopSearchAt = int.Parse(oDelegateFunctions.GetControlText(stopAtPositionTextBox));
            int startGeneratedChecksumFrom = int.Parse(oDelegateFunctions.GetControlText(startChecksumPositionTextBox));

            bool isLazySearch = oDelegateFunctions.GetControlText(lazySearchComboBox) == "ON";
            bool isLazyGenerate = oDelegateFunctions.GetControlText(lazyGenerateComboBox) == "ON";

            int byteCount = -1;
            string[] value = oDelegateFunctions.GetControlText(checksumComboBox).Split('{','}');

            switch (value[1].Replace("Bytes","").Replace("s",""))
            {
                case "1":
                    byteCount = 1;
                    break;
                case "2":
                    byteCount = 2;
                    break;
                case "4":
                    byteCount = 4;
                    break;
                case "8":
                    byteCount = 8;
                    break;
                case "16":
                    byteCount = 16;
                    break;
                case "32":
                    byteCount = 32;
                    break;
                case "48":
                    byteCount = 48;
                    break;
                case "64":
                    byteCount = 64;
                    break;
            }

            if (!isLazySearch && !isLazyGenerate)
            {
                if (oDelegateFunctions.GetCheckBoxCheck(usePCFCheckBox))
                {
                    SearchAndGenerateUsingPossibleChecksumFile(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                }
                else
                {
                    if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                        ParallelSearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                    else
                        SearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                }
            }
            else if (isLazySearch && isLazyGenerate)
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.LazyGenerateLazySearch);
                else
                    SearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.LazyGenerateLazySearch);
            }
            else if (!isLazySearch)
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.LazyGenerateNotLazySearch);
                else
                    SearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.LazyGenerateNotLazySearch);
            }
            else
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.NotLazyGenerateLazySearch);
                else
                    SearchAndGenerate(startSearch, stopSearchAt, startGeneratedChecksumFrom, byteCount, SearchTypeEnum.NotLazyGenerateLazySearch);
            }
        }

        private void SearchAndGenerateUsingPossibleChecksumFile(int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int byteCount, SearchTypeEnum searchType)
        {
            bool skip = oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox);
            int skipBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
            int checksumFound = -1;
            for (int checksumGenIndex = startGeneratedChecksumFrom; checksumGenIndex < stopSearchAt; )
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(checksumGenIndex, _fileBuffer);
                oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));

                for (int i = 0; i < _possibleChecksumFileBuffer.Length-1;)
                {
                    if (_stop) return;
                    int added = 0;
                    for (int index = 0; index < checksum.Length; index++)
                    {
                        byte item = checksum[index];
                        if (i == _possibleChecksumFileBuffer.Length) break;
                        if (_possibleChecksumFileBuffer[i] == item)
                        {
                            checksumFound = i;
                            i++;
                            added++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }

                    #region Reverse Search
                    if (oDelegateFunctions.GetCheckBoxCheck(searchReverseValueCheckBox))
                    {
                        if (checksumFound != -1) break;
                        i -= added;
                        Array.Reverse(checksum, 0, checksum.Length);
                        for (int j = 0; j < checksum.Length; j++)
                        {
                            if (_possibleChecksumFileBuffer[i] == checksum[j])
                            {
                                checksumFound = i;
                                i++;
                                added++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }
                    }
                    #endregion

                    if (checksumFound != -1) break;
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += 1;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                }
                if (checksumFound != -1) break;
                switch (searchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenIndex += byteCount;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenIndex += byteCount;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenIndex += 1;
                        break;
                }
                oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
            }
            oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        private void SearchAndGenerate(int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int byteCount, SearchTypeEnum searchType)
        {
            bool skip = oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox);
            int skipBy = (int) oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
            int checksumFound = -1;
            for (int checksumGenIndex = startGeneratedChecksumFrom; checksumGenIndex < stopSearchAt;)
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(checksumGenIndex, _fileBuffer);
                oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));
                for (int i = startSearch; i < stopSearchAt - byteCount;)
                {
                    if (_stop) return;
                    int added = 0;
                    //==================Forward Search============//
                    foreach (byte item in checksum)
                    {
                        if (_fileBuffer[i] == item )
                        {
                            checksumFound = i;
                            i++;
                            added++;
                        }
                        else
                        {
                            checksumFound = -1;
                            break;
                        }
                    }

                    //#region Reverse Search
                    //if (oDelegateFunctions.GetCheckBoxCheck(searchReverseValueCheckBox))
                    //{
                    //    if (checksumFound != -1) break;
                    //    i -= added;
                    //    Array.Reverse(checksum, 0, checksum.Length);
                    //    for (int j = 0; j < checksum.Length; j++)
                    //    {
                    //        if (_buffer[i] == checksum[j])
                    //        {
                    //            checksumFound = i;
                    //            i++;
                    //            added++;
                    //        }
                    //        else
                    //        {
                    //            checksumFound = -1;
                    //            break;
                    //        }
                    //    }
                    //}
                    //#endregion

                    if (checksumFound != -1) break;
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += 1;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                }
                if (checksumFound != -1) break;
                switch (searchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenIndex += byteCount;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenIndex += byteCount;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenIndex += 1;
                        break;
                }
                oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
            }
            oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        private void ParallelSearchAndGenerate(int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int byteCount, SearchTypeEnum searchType)
        {
            bool skip = oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox);
            int skipBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
            Parallel.For(startGeneratedChecksumFrom, stopSearchAt, (checksumGenIndex, y) =>
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(checksumGenIndex, _fileBuffer);
                for (int i = startSearch; i < stopSearchAt - byteCount; )
                {
                    int checksumFound = -1;
                    if (_stop) return;
                    int added = 0;
                        //==================Forward Search============//
                        foreach (byte item in checksum)
                        {
                            if (_fileBuffer[i] == item)
                            {
                                checksumFound = i;
                                i++;
                                added++;
                            }
                            else
                            {
                                checksumFound = -1;
                                break;
                            }
                        }

                        if (checksumFound != -1)
                        {
                            oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));
                            oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
                            oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
                            break;
                        }
                        switch (searchType)
                        {
                            case SearchTypeEnum.LazyGenerateLazySearch:
                                i += checksum.Length;
                                break;
                            case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                                i += 1;
                                break;
                            case SearchTypeEnum.LazyGenerateNotLazySearch:
                                i += 1;
                                break;
                            case SearchTypeEnum.NotLazyGenerateLazySearch:
                                i += checksum.Length;
                                break;
                        }
                    }
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            checksumGenIndex += byteCount;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            checksumGenIndex += 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            checksumGenIndex += byteCount;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            checksumGenIndex += 1;
                            break;
                    }
                    
                });
            oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        #region Backup functions
        private void StartClearFunction()
        {
            oDelegateFunctions.SetEnableControl(stopButton, true);
            oDelegateFunctions.SetControlText(stopTimeTextBox, "");
            oDelegateFunctions.SetEnableControl(startSearchButton, false);
            oDelegateFunctions.SetEnableControl(checksumComboBox, false);
            oDelegateFunctions.SetControlText(startTimeTextBox, DateTime.Now.ToString());
        }

        private void StopClearFunction()
        {
            oDelegateFunctions.SetEnableControl(checksumComboBox, true);
            oDelegateFunctions.SetEnableControl(stopButton, false);
            oDelegateFunctions.SetEnableControl(startSearchButton, true);
        }

        private byte[] GenerateChecksum(int offset, byte[] buffer)
        {
            byte[] returnValue = null;
            switch (oDelegateFunctions.GetControlText(checksumComboBox))
            {
                case "Adler8 - {1Byte}":
                    returnValue = Adler8.Compute(offset, buffer);
                    break;
                case "Adler16 - {2Bytes}":
                    returnValue = Adler16.Compute(offset, buffer);
                    break;
                case "Adler32 - {4Bytes}":
                    returnValue = Adler32.Compute(offset, buffer);
                    break;
                case "CRC32 - {4Bytes}":
                    returnValue = Crc32.Compute(offset,buffer);
                    break;
                case "HMAC SHA 1 (128)  - {16Bytes}":
                    returnValue = HmacSha1.Compute(offset, buffer);
                    break;
                case "HMAC SHA 256 - {32Bytes}":
                    returnValue = HmacSha256.Compute(offset, buffer);
                    break;
                case "HMAC SHA 384 - {48Byte}":
                    returnValue = HmacSha384.Compute(offset, buffer);
                    break;
                case "HMAC SHA 512 - {64Byte}":
                    returnValue = HmacSha512.Compute(offset, buffer);
                    break;
                case "MD5 - {16Byte}":
                    returnValue = Md5.Compute(offset, buffer);
                    break;
                case "MD5 CNG - {16Byte}":
                    returnValue = Md5Cng.Compute(offset, buffer);
                    break;
            }
            return returnValue;
        }
        #endregion

        #endregion






















        private void button2_Click(object sender, EventArgs e)
        {
            AES a = new AES();
        }

        

        
    }
}
