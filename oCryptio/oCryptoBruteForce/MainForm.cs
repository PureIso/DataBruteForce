using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using oCryptio;
using oCryptio.Checksum;
using System.Windows.Forms;
using PortableLib;

namespace oCryptoBruteForce
{
    public partial class MainForm : Form
    {
        public event DelegateObject OnDelegateObject;

        #region Fields
        private string _fileName;
        private byte[] _fileBuffer;
        private byte[] _possibleChecksumFileBuffer;
        private bool _stop;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            oDelegateFunctions.SetComboBoxSelectedIndex(lazyGenerateComboBox,0);
            oDelegateFunctions.SetComboBoxSelectedIndex(lazySearchComboBox, 0);
            oDelegateFunctions.SetComboBoxSelectedIndex(checksumComboBox, 0);
        }

        #region Handlers

        #region Open File and Load
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = "File Names|*.*"};
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _fileName = openFileDialog.FileNames[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            //Thread Safe Functions to prevent illegal cross threads.
            oDelegateFunctions.SetControlText(fileLocationTextBox, _fileName);
            oDelegateFunctions.SetControlText(fileLengthTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetControlText(stopAtPositionTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetEnableControl(startSearchButton,true);
            oDelegateFunctions.SetNumericUpDownValues(skipBytesNumericUpDown, 1, _fileBuffer.Length,1);
        }

        private void loadPossibleChecksumFileButton_Click(object sender, EventArgs e)
        {
            //In the case where the possible key / checksum might be in a different file
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = "File Names|*.*"};
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            oDelegateFunctions.SetControlText(possibleChecksumFileLocationTextBox, openFileDialog.FileNames[0]);
            oDelegateFunctions.CheckCheckBoxButton(usePCFCheckBox, true);
            _possibleChecksumFileBuffer = File.ReadAllBytes(openFileDialog.FileNames[0]);
        }

        //TODO: Drag and Drop Files into the application via dragAndDropPanel
        #endregion

        #region Start and Stop
        private void startSearchButton_Click(object sender, EventArgs e)
        {
            _stop = false;
            Thread oThread = new Thread(SearchWorkThread) {IsBackground = true};
            oThread.IsBackground = true;
            oThread.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopBruteForceUserInterfaceFunction();
            _stop = true;
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
        #endregion
        #endregion

        #region Functions
        #region Search Work
        /// <summary>
        /// Search initialization function that will determine what search protocol to use base on your configuration
        /// </summary>
        private void SearchWorkThread()
        {
            StartBruteForceUserInterfaceFunction();

            int startSearch = int.Parse(oDelegateFunctions.GetControlText(startSearchTextBox));
            int stopSearchAt = int.Parse(oDelegateFunctions.GetControlText(stopAtPositionTextBox));
            int startGeneratedChecksumFrom = int.Parse(oDelegateFunctions.GetControlText(startChecksumPositionTextBox));

            bool isLazySearch = oDelegateFunctions.GetControlText(lazySearchComboBox) == "ON";
            bool isLazyGenerate = oDelegateFunctions.GetControlText(lazyGenerateComboBox) == "ON";

            #region Get Checksum Length
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
            #endregion

            #region Launch Bruteforce
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
            #endregion
        }

        #region Main Search Functions
        private void SearchAndGenerateUsingPossibleChecksumFile(int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int byteCount, SearchTypeEnum searchType)
        {
            int skipBy = 1;
            int checksumFound = -1;
            if(oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                skipBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
            
            for (int checksumGenIndex = startGeneratedChecksumFrom; checksumGenIndex < stopSearchAt; )
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(checksumGenIndex, _fileBuffer);
                oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));

                #region Main Search
                for (int i = startSearch; i < _possibleChecksumFileBuffer.Length - 1; )
                {
                    if (_stop) return;
                    int added = 0;
                    foreach (byte item in checksum)
                    {
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

                    #region Search Type to determing next index
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += skipBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += skipBy;
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
                #endregion
            }
            oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        private void SearchAndGenerate(int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int byteCount, SearchTypeEnum searchType)
        {
            int skipBy = 1;
            int checksumFound = -1;
            if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                skipBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);

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
                    #region Search Type to determing next index
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += skipBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += skipBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }
                    #endregion
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
            int skipBy = 1;
            if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                skipBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);

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

                        #region Search Type to determing next index
                        switch (searchType)
                        {
                            case SearchTypeEnum.LazyGenerateLazySearch:
                                i += checksum.Length;
                                break;
                            case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                                i += skipBy;
                                break;
                            case SearchTypeEnum.LazyGenerateNotLazySearch:
                                i += skipBy;
                                break;
                            case SearchTypeEnum.NotLazyGenerateLazySearch:
                                i += checksum.Length;
                                break;
                        }
                        #endregion
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
        #endregion
        #endregion

        #region Backup functions
        /// <summary>
        /// Set the display for brute force
        /// </summary>
        private void StartBruteForceUserInterfaceFunction()
        {
            oDelegateFunctions.SetEnableControl(stopButton, true);
            oDelegateFunctions.SetControlText(stopTimeTextBox, "");
            oDelegateFunctions.SetEnableControl(startSearchButton, false);
            oDelegateFunctions.SetEnableControl(checksumComboBox, false);
            oDelegateFunctions.SetControlText(startTimeTextBox, DateTime.Now.ToString());
        }

        /// <summary>
        /// Clear the display
        /// </summary>
        private void StopBruteForceUserInterfaceFunction()
        {
            oDelegateFunctions.SetEnableControl(checksumComboBox, true);
            oDelegateFunctions.SetEnableControl(stopButton, false);
            oDelegateFunctions.SetEnableControl(startSearchButton, true);
            //TODO Save a log before clearing
        }

        /// <summary>
        /// Function to generate checksum
        /// </summary>
        /// <param name="offset">The start of checksum generation</param>
        /// <param name="buffer">The byte array of data</param>
        /// <returns></returns>
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
    }
}
