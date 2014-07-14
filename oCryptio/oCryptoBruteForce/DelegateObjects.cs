using System;

namespace oCryptoBruteForce
{
    [Serializable]
    public class DelegateObject
    {
        public byte[] DataArray { get; set; }
        public byte[] PossibleChecksumsArray { get; set; }

        public string WorkerId { get; set; }
        public string Checksum { get; set; }

        public int StartSearch { get; set; }
        public int StopSearchAt { get; set; }
        public int StartGeneratedChecksumFrom { get; set; }
        public int ChecksumLength { get; set; } 
        public int SkipSearchBytesBy { get; set; }

        public bool UseParallelComputation { get; set;}
        public bool Status { get; set; }
        public SearchTypeEnum searchType { get; set; }
    }

    public delegate void DelegateObjectDelegate(DelegateObject searchInformationObject);
}
