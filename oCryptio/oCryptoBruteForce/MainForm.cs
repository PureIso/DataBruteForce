using System;
using System.IO;
using System.Net.Configuration;
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
            string checksum = oDelegateFunctions.GetControlText(checksumComboBox);
            bool isLazySearch = oDelegateFunctions.GetControlText(lazySearchComboBox) == "ON";
            bool isLazyGenerate = oDelegateFunctions.GetControlText(lazyGenerateComboBox) == "ON";

            int skipSearchBytesBy = 1;
            if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                skipSearchBytesBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);

            #region Get Checksum Length
            int checksumLength = -1;
            string[] value = checksum.Split('{', '}');
            switch (value[1].Replace("Bytes","").Replace("s",""))
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
            #endregion

            #region Launch Bruteforce
            if (!isLazySearch && !isLazyGenerate)
            {
                if (_possibleChecksumFileBuffer != null)
                {
                    SearchAndGenerateUsingPossibleChecksumFile(checksum, skipSearchBytesBy,startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                }
                else
                {
                    if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                        ParallelSearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                    else
                        SearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.NotLazyGenerateNotLazySearch);
                }
            }
            else if (isLazySearch && isLazyGenerate)
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.LazyGenerateLazySearch);
                else
                    SearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.LazyGenerateLazySearch);
            }
            else if (!isLazySearch)
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.LazyGenerateNotLazySearch);
                else
                    SearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.LazyGenerateNotLazySearch);
            }
            else
            {
                if (oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox))
                    ParallelSearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.NotLazyGenerateLazySearch);
                else
                    SearchAndGenerate(checksum, skipSearchBytesBy, startSearch, stopSearchAt, startGeneratedChecksumFrom, checksumLength, SearchTypeEnum.NotLazyGenerateLazySearch);
            }
            #endregion
        }

        #region Main Search Functions
        private void SearchAndGenerateUsingPossibleChecksumFile(string theChecksum,int skipSearchBytesBy, int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int checksumLength, SearchTypeEnum searchType)
        {
            int checksumFound = -1;
            for (int checksumGenIndex = startGeneratedChecksumFrom; checksumGenIndex < stopSearchAt; )
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(theChecksum,checksumGenIndex, _fileBuffer);
                //oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));
                #region Main Search
                for (int i = startSearch; i < _possibleChecksumFileBuffer.Length - 1; )
                {
                    if (_stop) return;
                    foreach (byte item in checksum)
                    {
                        if (i == _possibleChecksumFileBuffer.Length) break;

                        if (_possibleChecksumFileBuffer[i] == item)
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
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
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
                        checksumGenIndex += checksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenIndex += checksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenIndex += 1;
                        break;
                }
                oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
                #endregion
            }
            //oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        private void SearchAndGenerate(string theChecksum,int skipSearchBytesBy, int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int checksumLength, SearchTypeEnum searchType)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = startGeneratedChecksumFrom; checksumGenerationIndex < stopSearchAt;)
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(theChecksum,checksumGenerationIndex, _fileBuffer);
                //oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));
                for (int i = startSearch; i < stopSearchAt - checksumLength;)
                {
                    if (_stop) return;
                    foreach (byte item in checksum)
                    {
                        if (_fileBuffer[i] == item )
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
                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
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
                        checksumGenerationIndex += checksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += checksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
                //oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
            }
            //oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }

        private void ParallelSearchAndGenerate(string theChecksum,int skipSearchBytesBy, int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int checksumLength, SearchTypeEnum searchType)
        {

            Parallel.For(startGeneratedChecksumFrom, stopSearchAt, checksumGenerationIndex =>
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(theChecksum, checksumGenerationIndex, _fileBuffer);
                for (int i = startSearch; i < stopSearchAt - checksumLength;)
                {
                    int checksumFound = -1;
                    if (_stop) return;
                    foreach (byte item in checksum)
                    {
                        if (_fileBuffer[i] == item)
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

                    if (checksumFound != -1)
                    {
                        //oDelegateFunctions.SetControlText(checksumValueTextBox, BitConverter.ToString(checksum));
                        //oDelegateFunctions.SetControlText(currentChecksumPositionTextBox, checksumGenIndex.ToString());
                        //oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
                        break;
                    }

                    #region Search Type to determing next index

                    switch (searchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            i += skipSearchBytesBy;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            i += checksum.Length;
                            break;
                    }

                    #endregion
                }
            });
            //oDelegateFunctions.SetControlText(stopTimeTextBox, DateTime.Now.ToString());
        }
        #endregion

        #region Remote Functions
        private void OnSearchAndGenerateUsingPossibleChecksumFile(DelegateObjects searchInformationObject)
        {
            _fileBuffer = searchInformationObject.DataArray;
            _possibleChecksumFileBuffer = searchInformationObject.PossibleChecksumsArray;

            SearchAndGenerateUsingPossibleChecksumFile(
                searchInformationObject.Checksum,
                searchInformationObject.SkipSearchBytesBy,
                searchInformationObject.StartSearch,
                searchInformationObject.StopSearchAt,
                searchInformationObject.StartGeneratedChecksumFrom,
                searchInformationObject.ChecksumLength,
                searchInformationObject.searchType);
        }

        private void OnSearchAndGenerate(DelegateObjects searchInformationObject)
        {
            _fileBuffer = searchInformationObject.DataArray;

            SearchAndGenerate(
                searchInformationObject.Checksum,
                searchInformationObject.SkipSearchBytesBy,
                searchInformationObject.StartSearch,
                searchInformationObject.StopSearchAt,
                searchInformationObject.StartGeneratedChecksumFrom,
                searchInformationObject.ChecksumLength,
                searchInformationObject.searchType);
        }

        private void OnParallelSearchAndGenerate(DelegateObjects searchInformationObject)
        {
            _fileBuffer = searchInformationObject.DataArray;

            ParallelSearchAndGenerate(
                searchInformationObject.Checksum,
                searchInformationObject.SkipSearchBytesBy,
                searchInformationObject.StartSearch,
                searchInformationObject.StopSearchAt,
                searchInformationObject.StartGeneratedChecksumFrom,
                searchInformationObject.ChecksumLength,
                searchInformationObject.searchType);
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
        /// <param name="checksum">The checksum name</param>
        /// <param name="offset">The start of checksum generation</param>
        /// <param name="buffer">The byte array of data</param>
        /// <returns></returns>
        private static byte[] GenerateChecksum(string checksum, int offset, byte[] buffer)
        {
            byte[] returnValue = null;
            switch (checksum)
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

        private void byteSkippingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            oDelegateFunctions.SetEnableControl(skipBytesNumericUpDown,
                oDelegateFunctions.GetEnableControl(byteSkippingCheckBox));
        }
        #endregion

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
    }
}
