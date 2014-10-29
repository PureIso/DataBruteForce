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
        private string _possibleChecksumFileName;
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
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "File Names|*.*" };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _fileName = openFileDialog.FileNames[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            oDelegateFunctions.SetCheckBoxCheck(convertFromBase64StringCheckBox, false);
            oDelegateFunctions.SetEnableControl(convertFromBase64StringCheckBox, true);

            try
            {
                _fileBase64Buffer = Convert.FromBase64String(File.ReadAllText(_fileName));
            }
            catch (FormatException)
            {
                _fileBase64Buffer = null;
                oDelegateFunctions.SetCheckBoxCheck(convertFromBase64StringCheckBox, false);
                oDelegateFunctions.SetEnableControl(convertFromBase64StringCheckBox, false);
            }
            //Thread Safe Functions to prevent illegal cross threads.
            oDelegateFunctions.SetControlText(stopAtPositionTextBox, _fileBuffer.Length.ToString());
            oDelegateFunctions.SetNumericUpDownValues(skipBytesNumericUpDown, 1, _fileBuffer.Length, 1);
        }

        private void openPossibleChecksumFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //In the case where the possible key / checksum might be in a different file
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "File Names|*.*" };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            _possibleChecksumFileName = openFileDialog.FileNames[0];
            _possibleChecksumFileBuffer = File.ReadAllBytes(_possibleChecksumFileName);
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
                workObject.ChecksumLength = Helper.GetChecksumLength(workObject.ChecksumType);
                workObject.UseTPL = oDelegateFunctions.GetCheckBoxCheck(tplCheckBox);
                workObject.ConvertFromBase64String = oDelegateFunctions.GetCheckBoxCheck(convertFromBase64StringCheckBox);
                if (oDelegateFunctions.GetCheckBoxCheck(byteSkippingCheckBox))
                    workObject.SkipSearchBytesBy = (int)oDelegateFunctions.GetNumericUpDown(skipBytesNumericUpDown);
                else workObject.SkipSearchBytesBy = 1;
                workObject.ExhaustiveSearch = oDelegateFunctions.GetCheckBoxCheck(exhaustiveSearchCheckBox);
                //====================================================================================================
                workObject.DataArray = _fileBuffer;
                workObject.DataArrayBase64 = _fileBase64Buffer;
                workObject.PossibleChecksumsArray = _possibleChecksumFileBuffer;
                workObject.PossibleChecksumsBase64Array = _possibleChecksumBase64FileBuffer;
                workObject.FileLocation = _fileName;
                workObject.FileName = Path.GetFileName(_fileName);
                workObject.PossibleChecksumFileLocation = _possibleChecksumFileName;
                workObject.PossibleChecksumFileName = Path.GetFileName(_possibleChecksumFileName);
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return workObject;
        }

        #region Remote Functions
        private void OnStartWork(DelegateObject searchInformationObject)
        {
            searchInformationObject.StartTime = DateTime.Now;
            if (searchInformationObject.PossibleChecksumsArray != null)
            {
                OnSearchAndGenerateUsingPossibleChecksumFile(searchInformationObject);
            }
            else
            {
                if (searchInformationObject.UseTPL) OnParallelSearchAndGenerate(searchInformationObject);
                else NormalSearch.OnSearchAndGenerate(searchInformationObject);
            }
        }

        private void OnSearchAndGenerateUsingPossibleChecksumFile(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenIndex = input.StartGeneratedChecksumFrom; checksumGenIndex < input.StopSearchAt; )
            {
                if (input.IsWorkDone) return;
                byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenIndex, input.DataArray);
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

        private void OnParallelSearchAndGenerate(DelegateObject input)
        {
            Parallel.For(input.StartGeneratedChecksumFrom, input.StopSearchAt, checksumGenerationIndex =>
            {
                if (input.IsWorkDone) return;
                byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, _fileBuffer);
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
                += NormalSearch.OnSearchAndGenerate;
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
                SetClientInformationText("Server Added: " + item.SubItems[2].Text + ":" + item.SubItems[3].Text);
                serverMonitorListView.Items.Add(item);
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                
                DelegateObject delegateObject = CreateDelegateObject();
                //Randomly generate a worker id
                Random random = new Random();
                string workId = "";
                //Create a work object
                List<string> workIdList = _listOfWorkObjects
                    .Select(worker => worker.WorkerId).ToList();

                while (workIdList.Contains(workId) || workId == "")
                {
                    workId = new string(Enumerable.Repeat(Characters, 8)
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
        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerViewForm workerViewForm = new WorkerViewForm(_listOfWorkObjects[_selectedWorkMonitorItemIndex]);
            workerViewForm.ShowDialog(this);
        }
        #endregion

        #region Drag Drop Panel
        private void dragAndDropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData((DataFormats.FileDrop));
            _fileName = files[0];
            _fileBuffer = File.ReadAllBytes(_fileName);
            //Thread Safe Functions to prevent illegal cross threads.
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
                                sender.EndTime = DateTime.Now;
                                workMonitorListView.Items[i].SubItems[7].Text = sender.EndTime.ToString();
                            }
                            else workMonitorListView.Items[i].SubItems[2].Text = "Unknown";                 
                        }
                    }
                }
            }
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           