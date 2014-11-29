using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptoBruteForce
{
    public class SearchResult
    {
        public int ChecksumFound { get; set; }
        public int ChecksumGeneratedLength { get; set; }
        public int ChecksumGeneratedOffset { get; set; }
        public string Checksum { get; set; }
    }
}
