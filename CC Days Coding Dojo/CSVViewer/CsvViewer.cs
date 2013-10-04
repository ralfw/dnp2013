using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVnachTabelle;
using CsvViewer;

namespace CSVViewer
{
    class CsvViewer
    {
        private readonly CommandLine.CommandLine _cmd;
        private readonly FileIO _fio;
        private readonly Ui _ui;
        private readonly Formatieren _format;

        public CsvViewer(CommandLine.CommandLine cmd, FileIO fio, Ui ui, Formatieren format)
        {
            _cmd = cmd;
            _fio = fio;
            _ui = ui;
            _format = format;
        }

        public void Run()
        {
            var dateiname = _cmd.Filename();
            var zeilen = _fio.ReadLines(dateiname);

            //var pager = new Pager();
            //zeilen = pager.First_page(zeilen, 5);
            
            zeilen = _format.Formatiere_als_Tabelle(zeilen.ToArray());
            _ui.ShowLines(zeilen);
        }
    }
}
