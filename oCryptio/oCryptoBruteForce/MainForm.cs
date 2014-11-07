using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PortableLib;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace oCryptoBruteForce
{
    public partial class MainForm : Form
    {
        #region Fields
        private string _fileName;
        private string _possibleChecksumFileName;
        private byte[] _fileBuffer;
        private byte[] _fileBase64Buffer;
        private byte[] _possibleChecksumFileBuffer;
        private byte[] _possibleChecksumBase64FileBuffer;
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
            try
            {
                _fileBase64Buffer = Convert.FromBase64String(File.ReadAllText(_fileName));
                oDelegateFunctions.SetCheckBoxCheck(convertFromBase64StringCheckBox, true);
                oDelegateFunctions.SetEnableControl(convertFromBase64StringCheckBox, true);
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
            ThreadPool.QueueUserWorkItem(x =>
            {
                //Adding Event Handlers
                searchInformationObject.OnStatusChange += WorkMonitorStatusChangeEventHandler;

                if (searchInformationObject.LocalEndpoint != null)
                    SetInformationTextListening("Started Work WorkId: " + searchInformationObject.WorkerId);

                searchInformationObject.IsWorkDone = false;
                searchInformationObject.FoundChecksum = false;

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

                searchInformationObject.IsWorkDone = true;
            });
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
        private readonly List<DelegateObject> _listOfServerWorkObjects = new List<DelegateObject>(); 

        #region Application Listen for incoming requests
        private bool _isListening;
        private int _listeningPort;
        private void startListeningButton_Click(object sender, EventArgs e)
        {
            StartListening();
        }
        private void StartListening(int port = 0)
        {
            if (_isListening) return;
            try
            {
                _listeningPort = port == 0 ? int.Parse(listeningTextBox.Text) : port;
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
            AsynchronousSocketListener.OnEndWork += WorkMonitorStatusChangeEventHandler;
            AsynchronousSocketListener.OnStartWork += OnStartWork;
            AsynchronousSocketListener.OnSetTextToInfo += SetInformationTextListening;
            AsynchronousSocketListener.OnClientToServerRequest += ServerMonitorStatusChangeEventHandler;
            AsynchronousSocketListener.StartListening(_listeningPort);
            SetInformationTextListening("Stopped listening on port: " + _listeningPort);
            _isListening = false;
        }
        #endregion

        private void stopListeningButton_Click(object sender, EventArgs e)
        {
            if (!_isListening) return;
            AsynchronousSocketListener.Listening = false;
        }
        private void SendResultRequest(DelegateObject delegateObject)
        {
            ServerMonitorStatusChangeEventHandler(delegateObject);
            //Serialise object
            string sendString;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, delegateObject);
                sendString = BitConverter.ToString(memoryStream.ToArray()).Replace("-", string.Empty);
            }
            byte[] sendBuffer = Encoding.ASCII.GetBytes(sendString);

            ThreadPool.QueueUserWorkItem(x =>
            {
                try
                {
                    Socket clientSocket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp);
                    try
                    {
                        clientSocket.Connect(delegateObject.LocalEndpoint);
                        byte[] sofBuffer = Encoding.ASCII.GetBytes("<OnEndWork>");
                        byte[] eofBuffer = Encoding.ASCII.GetBytes("<EOF>");
                        byte[] finalBuffer = new byte[sendBuffer.Length + sofBuffer.Length + eofBuffer.Length];
                        //==========================================================
                        Array.Copy(sofBuffer, 0, finalBuffer, 0, sofBuffer.Length);
                        Array.Copy(sendBuffer, 0, finalBuffer, sofBuffer.Length, sendBuffer.Length);
                        Array.Copy(eofBuffer, 0, finalBuffer, sofBuffer.Length + sendBuffer.Length, eofBuffer.Length);
                        clientSocket.Send(finalBuffer);
                        //==========================================================
                        //Peek into the socket for the data size
                        byte[] size = new byte[4];
                        clientSocket.Receive(size, 4, SocketFlags.Peek);
                        //Covert the size to integer
                        int intSize = BitConverter.ToInt32(size, 0);

                        //Initialize a buffer for incoming data
                        byte[] receiveBuffer = new byte[intSize + size.Length];
                        clientSocket.Receive(receiveBuffer);
                        byte[] data = new byte[intSize];
                        Array.Copy(receiveBuffer, 4, data, 0, intSize);
                        string result = Encoding.ASCII.GetString(data);
                        if (result == "FALSE")
                        {
                            SetInformationTextListening("Server Request End was not successful. WorkId: " + delegateObject.WorkerId);
                        }
                    }
                    catch (SocketException)
                    {
                        SetInformationTextListening("Server Offline. EndPoint: " + delegateObject.LocalEndpoint);
                    }
                    catch (Exception ex)
                    {
                        SetInformationTextListening("Send Exception: " + ex + " WorkId: " + delegateObject.WorkerId);
                    }
                    finally
                    {
                        clientSocket.Dispose();
                        clientSocket.Close();
                    }
                }
                catch (Exception nestedException)
                {
                    SetInformationTextListening("[Nested] Send Exception: " + nestedException + " WorkId: " + delegateObject.WorkerId);
                }
            });
        }
        private void SetInformationTextListening(string text)
        {
            oDelegateFunctions.SetInformationText(infoTextBox, text);
        }
        #endregion

        #region Client
        //Connect to a server and get the server to do some calculations for you
        //The server should be listening 
        private void addServerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(serverIPTextBox.Text)) throw new Exception("Server IP Address is null.");
                if (String.IsNullOrEmpty(serverPortTextBox.Text)) throw new Exception("Server Port is null.");
                int port = int.Parse(serverPortTextBox.Text);   //redundant port variable, just to validate
                ComboboxItem newComboBoxItem = new ComboboxItem
                {
                    Value = new IPEndPoint(IPAddress.Parse(serverIPTextBox.Text), port),
                    Text = serverIPTextBox.Text + ":" + serverPortTextBox.Text
                };
                
                clientServerAddressComboBox.Items.Add(newComboBoxItem);
                clientServerAddressComboBox.SelectedIndex = 0;
                SetClientInformationText("Server Added: " + newComboBoxItem.Text);
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(this, ex.Message, "Crypto BruteForce", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void selectAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clientServerAddressComboBox.Items.Count < 1)
            {
                oDelegateFunctions.MessageBoxShow(this, "Please add an IPEndpoint.", "Crypto BruteForce", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ComboboxItem comboboxItem = clientServerAddressComboBox.SelectedItem as ComboboxItem;
            if (comboboxItem == null) return;
            IPEndPoint endPoint = (IPEndPoint)comboboxItem.Value;

            for (int i = 0; i < serverMonitorListView.Items.Count; i++)
            {
                if (serverMonitorListView.Items[i].SubItems.Count <= 0) continue;
                //Worker Id
                if (serverMonitorListView.Items[i].SubItems[1].Text != _listOfWorkObjects[_selectedWorkMonitorItemIndex].WorkerId) continue;
               
                _listOfWorkObjects[_selectedWorkMonitorItemIndex].RemoteEndpoint = endPoint;
                SetClientInformationText("RemoteEndpoint Set for WorkId: " + 
                    _listOfWorkObjects[_selectedWorkMonitorItemIndex].WorkerId +
                    " Address: " + endPoint);
            }
        }
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StartListening(_listOfWorkObjects[_selectedWorkMonitorItemIndex].RemoteEndpoint.Port);
                _listOfWorkObjects[_selectedWorkMonitorItemIndex].ListeningPort = _listeningPort;
                SendRequest(_listOfWorkObjects[_selectedWorkMonitorItemIndex]);
            }
            catch (Exception ex)
            {
                SetClientInformationText("Server Connection Exception: " + ex);
            }
        }
        private void clientRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void Remove()
        {
            for (int i = 0; i < serverMonitorListView.Items.Count; i++)
            {
                if (serverMonitorListView.Items[i].SubItems.Count <= 0) continue;
                //Worker Id
                if (serverMonitorListView.Items[i].SubItems[1].Text !=
                    _listOfWorkObjects[_selectedWorkMonitorItemIndex].WorkerId) continue;
                serverMonitorListView.Items.RemoveAt(i);
                _listOfWorkObjects.RemoveAt(_selectedWorkMonitorItemIndex);
            }

            for (int i = 0; i < workMonitorListView.Items.Count; i++)
            {
                if (workMonitorListView.Items[i].SubItems.Count <= 0) continue;
                //Worker Id
                if (workMonitorListView.Items[i].SubItems[1].Text !=
                    _listOfWorkObjects[_selectedWorkMonitorItemIndex].WorkerId) continue;
                workMonitorListView.Items.RemoveAt(i);
                _listOfWorkObjects.RemoveAt(_selectedWorkMonitorItemIndex);
            }
        }
        private void clientViewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerViewForm workerViewForm = new WorkerViewForm(_listOfWorkObjects[_selectedWorkMonitorItemIndex]);
            workerViewForm.ShowDialog(this);
        }
        
        private void SendRequest(DelegateObject delegateObject)
        {
            //Serialise object
            string sendString;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, delegateObject);
                sendString = BitConverter.ToString(memoryStream.ToArray()).Replace("-", string.Empty);
            }
            byte[] sendBuffer = Encoding.ASCII.GetBytes(sendString);

            ThreadPool.QueueUserWorkItem(x =>
            {
                try
                {
                    Socket clientSocket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Stream,
                        ProtocolType.Tcp);
                    try
                    {
                        clientSocket.Connect(delegateObject.RemoteEndpoint);
                        byte[] sofBuffer = Encoding.ASCII.GetBytes("<OnStartWork>");
                        byte[] eofBuffer = Encoding.ASCII.GetBytes("<EOF>");
                        byte[] finalBuffer = new byte[sendBuffer.Length + sofBuffer.Length + eofBuffer.Length];
                        //==========================================================
                        Array.Copy(sofBuffer, 0, finalBuffer, 0, sofBuffer.Length);
                        Array.Copy(sendBuffer, 0, finalBuffer, sofBuffer.Length, sendBuffer.Length);
                        Array.Copy(eofBuffer, 0, finalBuffer, sofBuffer.Length + sendBuffer.Length, eofBuffer.Length);
                        SetClientInformationText("Sending WorkId: " + delegateObject.WorkerId);
                        clientSocket.Send(finalBuffer);
                        //==========================================================
                        //Peek into the socket for the data size
                        byte[] size = new byte[4];
                        clientSocket.Receive(size, 4, SocketFlags.Peek);
                        //Covert the size to integer
                        int intSize = BitConverter.ToInt32(size, 0);

                        //Initialize a buffer for incoming data
                        byte[] receiveBuffer = new byte[intSize + size.Length];
                        clientSocket.Receive(receiveBuffer);
                        byte[] data = new byte[intSize];
                        Array.Copy(receiveBuffer, 4, data, 0, intSize);
                        string result = Encoding.ASCII.GetString(data);
                        if (result == "FALSE")
                        {
                            SetClientInformationText("Server Request Start was not successful. WorkId: " + delegateObject.WorkerId);
                        }

                        SetClientInformationText("Server Request Waiting: WorkId: " + delegateObject.WorkerId);
                        //==========================================================
                        //Wait For Result
                        //==========================================================
                        NetworkStream netStream = new NetworkStream(clientSocket);
                        byte[] myReadBuffer = new byte[1024];
                        byte[] newBytes = new byte[0];
                        try
                        {
                            do
                            {
                                if (!netStream.CanRead) break;
                                myReadBuffer = new byte[1024];
                                int read = netStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                if (read == 0) break;
                                newBytes = CombineBytes(newBytes, myReadBuffer, read);
                                Thread.Sleep(300);
                            } while (netStream.DataAvailable);
                        }
                        catch (Exception ex)
                        {
                            SetClientInformationText("Server Request Waiting Exception: " + ex + " WorkId: " + delegateObject.WorkerId);
                        }
                    }
                    catch (SocketException)
                    {
                        SetClientInformationText("Server Offline. EndPoint: " + delegateObject.RemoteEndpoint);
                    }
                    catch (Exception ex)
                    {
                        SetClientInformationText("Send Exception: " + ex + " WorkId: " + delegateObject.WorkerId);
                    }
                    finally
                    {
                        clientSocket.Dispose();
                        clientSocket.Close();
                    }
                }
                catch (Exception nestedException)
                {
                    SetClientInformationText("[Nested] Send Exception: " + nestedException + " WorkId: " + delegateObject.WorkerId);
                }
            });
        }
        private static byte[] CombineBytes(byte[] first, byte[] second, int length)
        {
            try
            {
                int array1OriginalLength = first.Length;
                Array.Resize(ref first, array1OriginalLength + length);
                Array.Copy(second, 0, first, array1OriginalLength, length);
                return first;
            }
            catch (Exception ex)
            {
                Console.WriteLine("--- Combine Bytes ---");
                Console.WriteLine(ex.ToString());
                return first;
            }
        }
        private void SetClientInformationText(string text)
        {
            oDelegateFunctions.SetInformationText(clientInformationTextBox, text);
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

                ListViewItem workMonitorListViewItem = new ListViewItem(new[]
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

                ListViewItem serverMonitorListViewItem = new ListViewItem(new[]
                {
                    "Idle",                         //Status
                    workId,                         //Worker ID
                    "",                             //Server Ip
                    ""                              //Port
                });

                _listOfWorkObjects.Add(delegateObject);
                workMonitorListView.Items.Add(workMonitorListViewItem);
                serverMonitorListView.Items.Add(serverMonitorListViewItem);

                delegateObject.OnEndPointChange += WorkIPEndPontChangeEventHandler;

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
            OnStartWork(_listOfWorkObjects[_selectedWorkMonitorItemIndex]);
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _listOfWorkObjects[_selectedWorkMonitorItemIndex].IsWorkDone = true;
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove();
        }
        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerViewForm workerViewForm = new WorkerViewForm(_listOfWorkObjects[_selectedWorkMonitorItemIndex]);
            workerViewForm.ShowDialog(this);
        }
        #endregion



        #region Mouse Down Events
        private int _selectedWorkMonitorItemIndex;
        private void workMonitorListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem selectedItem = workMonitorListView.GetItemAt(e.X, e.Y);
            if (selectedItem == null) return;
            _selectedWorkMonitorItemIndex = selectedItem.Index;
            Point screenPoint = workMonitorListView.PointToScreen(e.Location);
            workContextMenuStrip.Show(screenPoint);
        }
        private void serverMonitorListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            ListViewItem selectedItem = serverMonitorListView.GetItemAt(e.X, e.Y);
            if (selectedItem == null) return;
            _selectedWorkMonitorItemIndex = selectedItem.Index;
            Point screenPoint = serverMonitorListView.PointToScreen(e.Location);
            clientModeContextMenuStrip.Show(screenPoint);
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

        #region Custom Handlers
        private void ServerMonitorStatusChangeEventHandler(DelegateObject sender)
        {
            if (clientMonitorListView.InvokeRequired)
                clientMonitorListView.Invoke((MethodInvoker)delegate
                {
                    ServerMonitorStatusChangeEventHandler(sender);
                });
            else
            {
                //Create a work object
                List<string> workIdList = _listOfServerWorkObjects
                    .Select(worker => sender.WorkerId).ToList();

                if (workIdList.Count == 0)
                {
                    _listOfServerWorkObjects.Add(sender);
                    ListViewItem clientMonitorListViewItem = new ListViewItem(new[]
                    {
                        "Idle",                         //Status
                        sender.WorkerId,                //Worker ID
                        "",                             //Server Ip
                        ""                              //Port
                    });
                    clientMonitorListView.Items.Add(clientMonitorListViewItem);
                }

                #region Worker Monitor Only
                for (int i = 0; i < clientMonitorListView.Items.Count; i++)
                {
                    if (clientMonitorListView.Items[i].SubItems.Count <= 0) continue;
                    //Worker Id
                    if (clientMonitorListView.Items[i].SubItems[1].Text != sender.WorkerId) continue;
                    //Status
                    clientMonitorListView.Items[i].SubItems[0].Text = sender.IsWorkDone ? "Done" : "Searching";
                    //IP 2
                    clientMonitorListView.Items[i].SubItems[2].Text = sender.LocalEndpoint.Address.ToString();
                    //Port 3
                    clientMonitorListView.Items[i].SubItems[3].Text = sender.LocalEndpoint.Port.ToString();
                }
                #endregion
            }
        }
        private void WorkMonitorStatusChangeEventHandler(DelegateObject sender)
        {
            if (workMonitorListView.InvokeRequired)
                workMonitorListView.Invoke((MethodInvoker)delegate {
                    WorkMonitorStatusChangeEventHandler(sender);
                });
            else
            {
                if (sender.IsWorkDone)
                {
                    if (sender.LocalEndpoint != null) SendResultRequest(sender);
                }

                #region Worker Monitor Only
                for (int i = 0; i < workMonitorListView.Items.Count; i++)
                {
                    if (workMonitorListView.Items[i].SubItems.Count <= 0) continue;
                    //Worker Id
                    if (workMonitorListView.Items[i].SubItems[1].Text != sender.WorkerId) continue;
                    //Status
                    workMonitorListView.Items[i].SubItems[0].Text = sender.IsWorkDone ? "Done" : "Searching";
                    //Result
                    if (sender.FoundChecksum) 
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
                #endregion

                #region Client To Server Monitor
                for (int i = 0; i < serverMonitorListView.Items.Count; i++)
                {
                    if (serverMonitorListView.Items[i].SubItems.Count <= 0) continue;
                    //Worker Id
                    if (serverMonitorListView.Items[i].SubItems[1].Text != sender.WorkerId) continue;
                    //Status
                    serverMonitorListView.Items[i].SubItems[0].Text = sender.IsWorkDone ? "Done" : "Searching";
                }
                #endregion
            }
        }
        private void WorkIPEndPontChangeEventHandler(DelegateObject sender)
        {
            if (serverMonitorListView.InvokeRequired)
                serverMonitorListView.Invoke((MethodInvoker)delegate
                {
                    WorkIPEndPontChangeEventHandler(sender);
                });
            else
            {
                for (int i = 0; i < serverMonitorListView.Items.Count; i++)
                {
                    if (serverMonitorListView.Items[i].SubItems.Count <= 0) continue;
                    //Worker Id
                    if (serverMonitorListView.Items[i].SubItems[1].Text != sender.WorkerId) continue;
                    //IP 2
                    serverMonitorListView.Items[i].SubItems[2].Text = sender.RemoteEndpoint.Address.ToString();
                    //Port 3
                    serverMonitorListView.Items[i].SubItems[3].Text = sender.RemoteEndpoint.Port.ToString();
                }
            }
        }
        #endregion  
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           