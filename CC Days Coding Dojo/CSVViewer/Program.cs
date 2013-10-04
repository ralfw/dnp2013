using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVnachTabelle;
using CsvViewer;

namespace CSVViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            // build
            var cmd = new CommandLine.CommandLine();
            var fio = new FileIO();
            var ui = new Ui();

            var format = new Formatieren();

            // bind
            var app = new CsvViewer(cmd, fio, ui, format);

            // run
            app.Run();
        }
    }
}
