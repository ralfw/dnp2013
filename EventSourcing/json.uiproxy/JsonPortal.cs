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

            _ui.JsonInput = json => {
                dynamic obj = json.FromJson();
                this.Input(obj);
            };

            _objekt_anzeigen = obj => {
                var json = obj.ToJson();
                _ui.JsonOutput_anzeigen(json);
            };

            _json_anzeigen = json => {
                 var prettyJson = json.PrettifyJson();
                 _ui.JsonOutput_anzeigen(prettyJson);
            };
        }


        private readonly Action _show;
        public void Show()
        {
            _show();
            _ui.ShowDialog();
        }


        private readonly Action<object> _objekt_anzeigen;
        public void Objekt_anzeigen(object output)
        {
            _objekt_anzeigen(output);
        }


        private readonly Action<string> _json_anzeigen;
        public void Json_anzeigen(string json)
        {
            _json_anzeigen(json);
        }

        public Action<dynamic> Input;
    }
}
