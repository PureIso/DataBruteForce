using System;

namespace oCryptoBruteForce
{
    [Serializable]
    public class DelegateObject
    {
        private bool _workDone;

        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string PossibleChecksumFileName { get; set; }
        public string PossibleChecksumFileLocation { get; set; }

        public byte[] DataArray { get; set; }
        public byte[] DataArrayBase64 { get; set; }
        public byte[] PossibleChecksumsArray { get; set; }
        public byte[] PossibleChecksumsBase64Array { get; set; }

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
        public bool FoundChecksum { get; set; }
        public string Checksum { get; set; }
        public int ChecksumOffset { get; set; }
        public int ChecksumGenerationLength { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        #endregion

        //Public Events
        public event DelegateObjectInVoidOut OnStatusChange;
    }

    public delegate void DelegateObjectDelegate(DelegateObject searchInformationObject);
    public delegate void DelegateObjectInVoidOut(DelegateObject searchInformationObject);
}
