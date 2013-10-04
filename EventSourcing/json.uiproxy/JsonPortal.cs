using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json.uiproxy
{
    public class JsonPortal
    {
        private readonly JsonPortalDlg _ui;

        public JsonPortal()
        {
            _ui = new JsonPortalDlg();
            var tp = new TemplateProvider();

            _show = tp.Auflisten;
            tp.Templatenamen += _ui.Templates;
            _ui.Template_ausgewählt += tp.Template_laden;
            tp.Template += _ui.Template_anzeigen;
            _ui.JsonInput = _ => this.JsonInput(_);

            _jsonOutput_anzeigen = _ui.JsonOutput_anzeigen;
        }


        private readonly Action _show;
        public void Show()
        {
            _show();
            _ui.ShowDialog();
        }

        private readonly Action<string> _jsonOutput_anzeigen;
        public void JsonOutput_anzeigen(string jsonOutput)
        {
            _jsonOutput_anzeigen(jsonOutput);
        }

        public Action<string> JsonInput;
    }
}
