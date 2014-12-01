using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oCryptoBruteForce
{
    public static class Search
    {
        private static int NormalSearch(DelegateObject input, byte[] dataArray, byte[] checksum)
        {
            int checksumOffset = -1;
            for (int i = input.StartSearch; i < input.StopSearchAt - input.ChecksumLength; )
            {
                #region Compare
                if (input.IsWorkDone) break;
                //Normal Compare
                foreach (byte item in checksum)
                {
                    if (i >= dataArray.Length) return -1;
                    if (dataArray[i] == item)
                    {
                        checksumOffset = i;
                        i++;
                    }
                    else
                    {
                        checksumOffset = -1;
                        break;
                    }
                }
                if (input.IsWorkDone) break;
                if (checksumOffset == -1)
                {
                    //Reverse Compare
                    Array.Reverse(checksum);
                    foreach (byte item in checksum)
                    {
                        if (i >= dataArray.Length) return -1;
                        if (dataArray[i] == item)
                        {
                            checksumOffset = i;
                            i++;
                        }
                        else
                        {
                            checksumOffset = -1;
                            break;
                        }
                    }
                }
                #endregion

                if (checksumOffset != -1) break;
                
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
            return checksumOffset;
        }
        private static int SearchBase64(DelegateObject input, byte[] dataArray, byte[] checksum)
        {
            int checksumOffset = -1;
            for (int i = input.StartSearch; i < dataArray.Length - 1; )
            {
                #region Compare
                if (input.IsWorkDone) break;
                foreach (byte item in checksum)
                {
                    if (i >= dataArray.Length) return -1;
                    if (dataArray[i] == item)
                    {
                        checksumOffset = i;
                        i++;
                    }
                    else
                    {
                        checksumOffset = -1;
                        break;
                    }
                }

                if (input.IsWorkDone) return checksumOffset;
                if (checksumOffset == -1)
                {
                    Array.Reverse(checksum);
                    foreach (byte item in checksum)
                    {
                        if (i >= dataArray.Length) return -1;
                        if (dataArray[i] == item)
                        {
                            checksumOffset = i;
                            i++;
                        }
                        else
                        {
                            checksumOffset = -1;
                            break;
                        }
                    }
                }
                #endregion

                if (checksumOffset != -1) break;
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
            return checksumOffset;
        }
        
        public static void OnSearchForwardAndReverse(DelegateObject input)
        {
            Task<SearchResult>[] tasks = new Task<SearchResult>[2];
            if (input.UseTPL)
            {
                if (input.ConvertFromBase64String)
                {
                    //Assign work
                    tasks[0] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerateBase64(input));
                    tasks[1] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerateBase64Reverse(input));
                }
                else
                {
                    tasks[0] = Task<SearchResult>.Factory.StartNew(() => OnParallelSearchAndGenerate(input));
                    tasks[1] = Task<SearchResult>.Factory.StartNew(() => OnParallelSearchAndGenerateReverse(input));
                }
            }
            else
            {
                if (input.ConvertFromBase64String)
                {
                    //Assign work
                    tasks[0] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerateBase64(input));
                    tasks[1] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerateBase64Reverse(input));
                }
                else
                {
                    tasks[0] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerate(input));
                    tasks[1] = Task<SearchResult>.Factory.StartNew(() => OnSearchAndGenerateReverse(input));
                }
            }

            //Wait for the first to finish
            int index = Task.WaitAny(tasks);
            //Get result of the finished task
            SearchResult result = tasks[index].Result;
            //Validate Result
            if (result.ChecksumFound != -1)
            {
                input.ChecksumGenerationLength = result.ChecksumGeneratedLength;
                input.ChecksumGeneratedOffset = result.ChecksumGeneratedOffset;
                input.Checksum = result.Checksum;
                input.ChecksumFound = true;
            }
            else
            {
                switch (index)
                {
                    case 0:
                        result = tasks[1].Result;
                        break;
                    case 1:
                        result = tasks[0].Result;
                        break;
                }

                if (result.ChecksumFound != -1)
                {
                    input.ChecksumGenerationLength = result.ChecksumGeneratedLength;
                    input.ChecksumGeneratedOffset = result.ChecksumGeneratedOffset;
                    input.Checksum = result.Checksum;
                    input.ChecksumFound = true;
                }
                else
                {
                    input.ChecksumGenerationLength = result.ChecksumGeneratedLength;
                    input.ChecksumGeneratedOffset = result.ChecksumGeneratedOffset;
                    input.Checksum = result.Checksum;
                    input.ChecksumFound = false;
                }
            }
        }

        private static SearchResult OnSearchAndGenerate(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };
            try
            {
                for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
                {
                    for (int eof = input.DataArray.Length; eof >= 0; )
                    {
                        if (input.IsWorkDone) break;
                        byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArray, eof);
                        searchResult.ChecksumGeneratedOffset = checksumGenerationIndex;
                        searchResult.ChecksumGeneratedLength = eof;
                        searchResult.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                        searchResult.ChecksumFound = NormalSearch(input, input.PossibleChecksumsArray ?? input.DataArray, checksum);

                        if (searchResult.ChecksumFound != -1) break;
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
                    if (searchResult.ChecksumFound != -1) break;
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
            }
            catch (Exception ex)
            {
                // ReSharper disable once LocalizableElement
                Console.WriteLine("---OnSearchAndGenerate---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 2");
            }
            return searchResult;
        }
        private static SearchResult OnSearchAndGenerateReverse(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };
            try
            {
                for (int checksumGenerationIndex = input.StartGeneratedChecksumFrom; checksumGenerationIndex < input.StopSearchAt; )
                {
                    for (int eof = input.DataArrayReversed.Length; eof >= 0; )
                    {
                        if (input.IsWorkDone) break;
                        byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayReversed);
                        searchResult.ChecksumGeneratedOffset = checksumGenerationIndex;
                        searchResult.ChecksumGeneratedLength = eof;
                        searchResult.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                        searchResult.ChecksumFound = NormalSearch(input, input.PossibleChecksumsArrayReversed ?? input.DataArrayReversed, checksum);

                        if (searchResult.ChecksumFound != -1) break;
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

                    if (searchResult.ChecksumFound != -1) break;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("---OnSearchAndGenerateReverse---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 1");
            }
            return searchResult;
        }
        private static SearchResult OnSearchAndGenerateBase64(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };
            try
            {
                for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64.Length; )
                {
                    for (int eof = input.DataArrayBase64.Length; eof >= 0; )
                    {
                        //if (token.IsCancellationRequested) break;
                        if (input.IsWorkDone) break;
                        byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64, eof);
                        searchResult.ChecksumGeneratedOffset = checksumGenerationIndex;
                        searchResult.ChecksumGeneratedLength = eof;
                        searchResult.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                        searchResult.ChecksumFound = SearchBase64(input, input.PossibleChecksumsBase64Array ?? input.DataArrayBase64, checksum);
                        if (searchResult.ChecksumFound != -1) break;

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

                    if (searchResult.ChecksumFound != -1) break;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("---OnSearchAndGenerateBase64---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 1");
            }
            return searchResult;
        }
        private static SearchResult OnSearchAndGenerateBase64Reverse(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };
            try
            {
                for (int checksumGenerationIndex = 0; checksumGenerationIndex < input.DataArrayBase64Reversed.Length; )
                {
                    for (int eof = input.DataArrayBase64Reversed.Length; eof >= 0; )
                    {
                        //if (token.IsCancellationRequested) return -1;
                        if (input.IsWorkDone) break;
                        byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex, input.DataArrayBase64Reversed);
                        searchResult.ChecksumGeneratedOffset = checksumGenerationIndex;
                        searchResult.ChecksumGeneratedLength = eof;
                        searchResult.Checksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                        searchResult.ChecksumFound = SearchBase64(input, input.PossibleChecksumsBase64ArrayReversed ?? input.DataArrayBase64Reversed, checksum);
                        if (searchResult.ChecksumFound != -1) break;
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

                    if (searchResult.ChecksumFound != -1) break;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("---OnSearchAndGenerateBase64Reverse---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 2");
            }
            return searchResult;
        }

        private static SearchResult OnParallelSearchAndGenerate(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };

            try
            {
                int increment = 1;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        increment = input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        increment = 1;
                        break;
                }

                List<int> indexesToSearch = new List<int>();
                for (int i = input.StartGeneratedChecksumFrom; i < input.StopSearchAt;)
                {
                    indexesToSearch.Add(i);
                    i += increment;
                }

                Parallel.ForEach(indexesToSearch, (checksumGenerationIndex, loopState) =>
                    {
                        for (int eof = input.DataArray.Length; eof >= 0;)
                        {
                            if (input.IsWorkDone) break;
                            byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex,
                                input.DataArray, eof);
                            int thisChecksumGeneratedOffset = checksumGenerationIndex;
                            int thisChecksumGeneratedLength = eof;
                            string thisChecksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                            int thisChecksumFound = NormalSearch(input, input.PossibleChecksumsArray ?? input.DataArray,
                                checksum);

                            if (thisChecksumFound != -1)
                            {
                                Console.WriteLine("---Checksum Normal---");
                                Console.WriteLine(thisChecksumFound);
                                searchResult = new SearchResult
                                {
                                    ChecksumFound = thisChecksumFound,
                                    ChecksumGeneratedLength = thisChecksumGeneratedLength,
                                    ChecksumGeneratedOffset = thisChecksumGeneratedOffset,
                                    Checksum = thisChecksum
                                };
                                loopState.Stop();
                                break;
                            }

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

                        if (searchResult.ChecksumFound != -1) loopState.Stop();
                        if (input.IsWorkDone) loopState.Stop();
                    });
            }
            catch (Exception ex)
            {
                // ReSharper disable once LocalizableElement
                Console.WriteLine("---OnSearchAndGenerate---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 2");
            }
            return searchResult;
        }
        private static SearchResult OnParallelSearchAndGenerateReverse(DelegateObject input)
        {
            SearchResult searchResult = new SearchResult
            {
                ChecksumFound = -1,
                ChecksumGeneratedLength = -1,
                ChecksumGeneratedOffset = -1,
                Checksum = "UNKNOWN"
            };
            try
            {
                int increment = 1;
                switch (input.SearchType)
                {
                    case SearchTypeEnum.LazyGenerateLazySearch:
                    case SearchTypeEnum.LazyGenerateNotLazySearch:
                        increment = input.ChecksumLength;
                        break;
                    case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                    case SearchTypeEnum.NotLazyGenerateLazySearch:
                        increment = 1;
                        break;
                }

                List<int> indexesToSearch = new List<int>();
                for (int i = input.StartGeneratedChecksumFrom; i < input.StopSearchAt;)
                {
                    indexesToSearch.Add(i);
                    i += increment;
                }

                Parallel.ForEach(indexesToSearch, (checksumGenerationIndex, loopState) =>
                {
                    for (int eof = input.DataArrayReversed.Length; eof >= 0; )
                    {
                        if (input.IsWorkDone) break;
                        byte[] checksum = Helper.GenerateChecksum(input.ChecksumType, checksumGenerationIndex,
                            input.DataArrayReversed);

                        int thisChecksumGeneratedOffset = checksumGenerationIndex;
                        int thisChecksumGeneratedLength = eof;
                        string thisChecksum = BitConverter.ToString(checksum).Replace("-", string.Empty);
                        int thisChecksumFound = NormalSearch(input,
                            input.PossibleChecksumsArrayReversed ?? input.DataArrayReversed, checksum);

                        if (thisChecksumFound != -1)
                        {
                            Console.WriteLine("---Checksum Reversed---");
                            Console.WriteLine(thisChecksumFound);
                            searchResult = new SearchResult
                            {
                                ChecksumFound = thisChecksumFound,
                                ChecksumGeneratedLength = thisChecksumGeneratedLength,
                                ChecksumGeneratedOffset = thisChecksumGeneratedOffset,
                                Checksum = thisChecksum
                            };
                            loopState.Stop();
                            break;
                        }

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

                    if (searchResult.ChecksumFound != -1) loopState.Stop();
                    if (input.IsWorkDone) loopState.Stop();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("---OnSearchAndGenerateReverse---");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Done 1");
            }
            return searchResult;
        }
    }
}
