using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvViewer
{
    class Ui
    {
        public void ShowLines(IEnumerable<string> lines)
        {
           //  Console.Clear();
            foreach(var line in lines) {
                Console.WriteLine(line);

            }
        }
    }
}
