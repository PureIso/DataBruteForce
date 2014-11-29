using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace oCryptoBruteForce
{
    [Serializable()]
    public class DelegateObject
    {
        private bool _workDone;
        private IPEndPoint _ipEndpoint;
        private IPEndPoint _localIpEndPoint;

        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string PossibleChecksumFileName { get; set; }
        public string PossibleChecksumFileLocation { get; set; }

        public byte[] DataArray { get; set; }
        public byte[] DataArrayReversed { get; set; }

        public byte[] DataArrayBase64 { get; set; }
        public byte[] DataArrayBase64Reversed { get; set; }

        public byte[] PossibleChecksumsArray { get; set; }
        public byte[] PossibleChecksumsBase64Array { get; set; }

        public byte[] PossibleChecksumsArrayReversed { get; set; }
        public byte[] PossibleChecksumsBase64ArrayReversed { get; set; }

        public string WorkerId { get; set; }
        public string ChecksumType { get; set; }
        public string Note { get; set; }

        public int StartSearch { get; set; }
        public int StopSearchAt { get; set; }
        public int StartGeneratedChecksumFrom { get; set; }
        public int ChecksumLength { get; set; } 
        public int SkipSearchBytesBy { get; set; }

        public bool UseTPL { get; set;}
        public bool ConvertFromBase64String { get; set; }
        public bool ExhaustiveSearch { get; set; }

        public SearchTypeEnum SearchType { get; set; }

        #region Result Properties
        public bool IsWorkDone
        {
            get
            {
                return _workDone;
            }
            set
            {
                _workDone = value;
                if (OnStatusChange != null) OnStatusChange(this);
            }
        }
        public bool ChecksumFound { get; set; }
        public string Checksum { get; set; }
        public int ChecksumGeneratedOffset { get; set; }
        public int ChecksumGenerationLength { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        #endregion

        #region Network Address
        public IPEndPoint RemoteEndpoint
        {
            get
            {
                return _ipEndpoint;
            }
            set
            {
                _ipEndpoint = value;
                if (OnEndPointChange != null) OnEndPointChange(this);
            }
        }
        public IPEndPoint LocalEndpoint
        {
            get
            {
                return _localIpEndPoint;
            }
            set
            {
                _localIpEndPoint = value;
                if (OnEndPointChange != null) OnEndPointChange(this);
            }
        }
        public int ListeningPort { get; set; }
        #endregion

        //Public Events
        [field: NonSerialized]
        public event DelegateObjectInVoidOut OnStatusChange;
        [field: NonSerialized]
        public event DelegateObjectInVoidOut OnEndPointChange;
    }

    public delegate void DelegateObjectDelegate(DelegateObject searchInformationObject);
    public delegate void DelegateObjectInVoidOut(DelegateObject searchInformationObject);
}
