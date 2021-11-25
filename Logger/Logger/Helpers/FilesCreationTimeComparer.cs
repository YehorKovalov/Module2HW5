using System;
using System.Collections.Generic;
using System.IO;

namespace Logger.Helpers
{
    public class FilesCreationTimeComparer : IComparer<string>
    {
        public int Compare(string fileName1, string fileName2)
        {
            if (string.IsNullOrWhiteSpace(fileName1) ||
                string.IsNullOrWhiteSpace(fileName2))
            {
                return 0;
            }

            var dateTime1 = new FileInfo(fileName1).CreationTimeUtc;
            var dateTime2 = new FileInfo(fileName2).CreationTimeUtc;
            return DateTime.Compare(dateTime1, dateTime2);
        }
    }
}
