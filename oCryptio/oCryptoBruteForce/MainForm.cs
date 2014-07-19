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
       
        #region Functions

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
                        workObject.searchType = SearchTypeEnum.NotLazyGenerateNotLazySearch;
                        workObject.PossibleChecksumsArray = _possibleChecksumFileBuffer;
                    }
                    else
                    {
                        workObject.searchType = SearchTypeEnum.NotLazyGenerateNotLazySearch;
                    }
                }
                else if (isLazySearch && isLazyGenerate)
                {
                    workObject.searchType = SearchTypeEnum.LazyGenerateLazySearch;
                }
                else if (!isLazySearch)
                {
                    workObject.searchType = SearchTypeEnum.LazyGenerateNotLazySearch;
                }
                else
                {
                    workObject.searchType = SearchTypeEnum.NotLazyGenerateLazySearch;
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
                workObject.Checksum = oDelegateFunctions.GetControlText(checksumComboBox);
                workObject = GetSearchTypeEnum(workObject);
                workObject.ChecksumLength = GetChecksumLength(workObject.Checksum);
                workObject.UseParallelComputation = oDelegateFunctions.GetCheckBoxCheck(parallelComputingCheckBox);
                if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                    workObject.SkipSearchBytesBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
                else workObject.SkipSearchBytesBy = 1;
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return workObject;
        }
        #endregion

        #region Main Search Functions
        private void SearchAndGenerateUsingPossibleChecksumFile(string theChecksum,int skipSearchBytesBy, int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int checksumLength, SearchTypeEnum searchType)
        {
            int checksumFound = -1;
            for (int checksumGenIndex = startGeneratedChecksumFrom; checksumGenIndex < stopSearchAt; )
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(theChecksum,checksumGenIndex, _fileBuffer);
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
        }

        private void SearchAndGenerate(string theChecksum,int skipSearchBytesBy, int startSearch, int stopSearchAt, int startGeneratedChecksumFrom, int checksumLength, SearchTypeEnum searchType)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = startGeneratedChecksumFrom; checksumGenerationIndex < stopSearchAt;)
            {
                if (_stop) return;
                byte[] checksum = GenerateChecksum(theChecksum,checksumGenerationIndex, _fileBuffer);
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
            }
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
            });
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

        private void OnSearchAndGenerateUsingPossibleChecksumFile(DelegateObject searchInformationObject)
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

        private void OnSearchAndGenerate(DelegateObject searchInformationObject)
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

        private void OnParallelSearchAndGenerate(DelegateObject searchInformationObject)
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

                ListViewItem item = new ListViewItem(new[]
                {
                    "Idle",                          //Status
                    workId                           //Worker ID
                });

                _listOfWorkObjects.Add(delegateObject);
                workMonitorListView.Items.Add(item);
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].Status = true;
            ThreadPool.QueueUserWorkItem(
                x => OnStartWork(_listOfWorkObjects[_selectedWorkMonitorItemIndex]));
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].Status = false;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects.RemoveAt(_selectedWorkMonitorItemIndex);
        }
        #endregion

        private void dragAndDropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData((DataFormats.FileDrop));
            _fileName = files[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            //Thread Safe Functions to prevent illegal cross threads.
            oDelegateFunctions.SetControlText(fileLocationTextBox, _fileName);
            oDelegateFunctions.SetControlText(fileLengthTextBox, _fileBuffer.Length.ToString());
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
    }
}
