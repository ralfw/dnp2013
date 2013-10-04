using System;
using System.Collections.Generic;
using System.IO;

namespace CsvViewer
{
    public class FileIO
    {
        public IEnumerable<String> ReadLines(String dateiName)
        {
            return File.ReadAllLines(dateiName);
        }
    }
}
