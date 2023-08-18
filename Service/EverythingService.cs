using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static EverythingSearch.Service.EverythingSDK;

namespace EverythingSearch.Service
{
    public static class EverythingService
    {
        public const int MAX_RESULTS = 10;

        public static IEnumerable<Result> Search(string content)
        {
            Everything_SetSearchW(content);
            Everything_SetRequestFlags(
                EVERYTHING_REQUEST_FILE_NAME | 
                EVERYTHING_REQUEST_PATH | 
                EVERYTHING_REQUEST_DATE_MODIFIED | 
                EVERYTHING_REQUEST_SIZE);

            Everything_QueryW(true);
            var resultCount = Everything_GetNumResults();

            for (uint i = 0; i < (resultCount > MAX_RESULTS ? MAX_RESULTS : resultCount); i++)
            {
                const int STRING_BUILDER_CAPACITY = 900;
                var stringBuilder = new StringBuilder(STRING_BUILDER_CAPACITY);
                Everything_GetResultFullPathName(i, stringBuilder, STRING_BUILDER_CAPACITY);
                Everything_GetResultDateModified(i, out long dateModified);
                Everything_GetResultSize(i, out long byteSize);

                yield return new Result()
                {
                    DateModified = DateTime.FromFileTime(dateModified),
                    ByteSize = byteSize,
                    Filename = Marshal.PtrToStringUni(Everything_GetResultFileName(i)) ?? "Unknown File Name",
                    Path = stringBuilder.ToString()
                };
            }
        }

        public struct Result
        {
            public long ByteSize;
            public DateTime DateModified;
            public string Filename;
            public string Path;

            public bool IsFolder => ByteSize < 0;

            public override string ToString() => 
                $"Name: {Filename}\t" +
                $"Size (B): {(IsFolder ? "(Folder)" : ByteSize)}\t" +
                $"Modified: {DateModified:d}\t" +
                $"Path: {Path[..15]}...";
        }
    }
}
