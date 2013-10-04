using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace json.uiproxy
{
    class TemplateProvider
    {
        private List<string> _templates;


        public void Auflisten()
        {
            var filenames = Directory.GetFiles(".", "*.json");

            _templates = new List<string>();
            filenames.ToList().ForEach(fn => _templates.Add(File.ReadAllText(fn)));
            
            Templatenamen(filenames.Select(Path.GetFileNameWithoutExtension).ToArray());
        }


        public void Template_laden(int index)
        {
            Template(_templates[index].Replace("\t", "  "));
        }


        public Action<string[]> Templatenamen;
        public Action<string> Template;
    }
}