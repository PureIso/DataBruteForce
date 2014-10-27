using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oCryptoBruteForce
{
    public static class NormalSearch
    {
        private static int Search(DelegateObject input, byte[] checksum)
        {
            int checksumFound = -1;
            for (int i = input.StartSearch; i < input.StopSearchAt - input.ChecksumLength; )
            {
                if (input.IsWorkDone) return checksumFound;
                input.ChecksumOffset = i;

                #region Compare
                //Normal Compare
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

                if (input.IsWorkDone) return checksumFound;
                if (checksumFound == -1)
                {
                    //Reverse Compare
                    Array.Reverse(checksum);
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
                }
                #endregion

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
            return checksumFound;
        }

        private static int SearchBase64(DelegateObject input, byte[] checksum)
        {
            int checksumFound = -1;
            for (int i = input.StartSearch; i < input.DataArrayBase64.Length - 1; )
            {
                if (input.IsWorkDone) return checksumFound;
                input.ChecksumOffset = i;
                foreach (byte item in checksum)
                {
                    if (input.DataArrayBase64[i] == item)
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
                if (input.IsWorkDone) return checksumFound;
                if (checksumFound == -1)
                {
                    Array.Reverse(checksum);
                    foreach (byte item in checksum)
                    {
                        if (input.DataArrayBase64[i] == item)
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
            return checksumFound;
        }

        public static void OnSearchAndGenerate(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
            {
                for (int eof = input.DataArray.Length; eof >= 0; )
                {
                    if (input.IsWorkDone) return;
                    byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArray, eof);
                    input.ChecksumOffset = checksumGenerationIndex;
                    input.ChecksumGenerationLength = input.StopSearchAt - checksumGenerationIndex;
                    input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                    checksumFound = Search(input, checksum);

                    if (checksumFound != -1) break;
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            eof -= 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            eof -= 1;
                            break;
                    }
                    if (!input.ExhaustiveSearch) break;
                }
                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.FoundChecksum = false;
                input.ChecksumOffset = -1;
                Array.Reverse(input.DataArray);
                OnSearchAndGenerateReverse(input);
            }
            else
            {
                input.FoundChecksum = true;
            }
            input.IsWorkDone = true;
        }

        public static void OnSearchAndGenerateReverse(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
            {
                for (int eof = input.DataArray.Length; eof >= 0; )
                {
                    if (input.IsWorkDone) return;
                    byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArray);
                    input.ChecksumOffset = checksumGenerationIndex;
                    input.ChecksumGenerationLength = input.StopSearchAt - checksumGenerationIndex;
                    input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                    checksumFound = Search(input, checksum);

                    if (checksumFound != -1) break;
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            eof -= 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            eof -= 1;
                            break;
                    }
                    if (!input.ExhaustiveSearch) break;
                }

                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.FoundChecksum = false;
                input.ChecksumOffset = -1;
                if (input.ConvertFromBase64String)
                {
                    OnSearchAndGenerateBase64(input);
                }
            }
            else
            {
                input.FoundChecksum = true;
            }
            input.IsWorkDone = true;
        }

        public static void OnSearchAndGenerateBase64(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64.Length; )
            {
                for (int eof = input.DataArrayBase64.Length; eof >= 0; )
                {
                    if (input.IsWorkDone) return;
                    byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64, eof);

                    input.ChecksumOffset = checksumGenerationIndex;
                    input.ChecksumGenerationLength = input.DataArrayBase64.Length - checksumGenerationIndex;
                    input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                    checksumFound = SearchBase64(input, checksum);

                    if (checksumFound != -1) break;
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            eof -= 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            eof -= 1;
                            break;
                    }
                    if (!input.ExhaustiveSearch) break;
                }

                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.ChecksumOffset = -1;
                input.FoundChecksum = false;
                Array.Reverse(input.DataArrayBase64);
                OnSearchAndGenerateBase64Reverse(input);
            }
            else input.FoundChecksum = true;
        }

        public static void OnSearchAndGenerateBase64Reverse(DelegateObject input)
        {
            int checksumFound = -1;
            for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64.Length; )
            {
                for (int eof = input.DataArrayBase64.Length; eof >= 0; )
                {
                    if (input.IsWorkDone) return;
                    byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64);
                    input.ChecksumOffset = checksumGenerationIndex;
                    input.ChecksumGenerationLength = input.DataArrayBase64.Length - checksumGenerationIndex;
                    input.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);

                    checksumFound = SearchBase64(input, checksum);

                    if (checksumFound != -1) break;
                    switch (input.SearchType)
                    {
                        case SearchTypeEnum.LazyGenerateLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                            eof -= 1;
                            break;
                        case SearchTypeEnum.LazyGenerateNotLazySearch:
                            eof -= input.ChecksumLength;
                            break;
                        case SearchTypeEnum.NotLazyGenerateLazySearch:
                            eof -= 1;
                            break;
                    }
                    if (!input.ExhaustiveSearch) break;
                }

                if (checksumFound != -1) break;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        checksumGenerationIndex += input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        checksumGenerationIndex += 1;
                        break;
                }
            }

            if (checksumFound == -1)
            {
                input.ChecksumOffset = -1;
                input.FoundChecksum = false;
            }
            else input.FoundChecksum = true;
        }
    }
}
