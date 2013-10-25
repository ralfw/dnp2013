using System;
using System.Globalization;
using System.IO;
using System.Linq;
using simpleeventstore.contract;

namespace simpleeventstore
{
    internal class EventPersistenceProvider
    {
        private readonly string _path;

        public EventPersistenceProvider(string _path) { this._path = _path; }


        public void Write_event(Event e)
        {
            using (var sw = new StreamWriter(Build_event_filename(e.Number)))
            {
                sw.WriteLine(e.Number);
                sw.WriteLine(e.Timestamp.ToString("s"));
                sw.WriteLine(e.Name);
                sw.WriteLine(e.Context);
                sw.WriteLine(e.Data);
            }
        }


        public Event Read_event(int number)
        {
            var lines = File.ReadAllLines(Build_event_filename(number)).ToArray();
            return new Event(lines[2],
                             string.Join("\n", lines.Skip(4)),
                             lines[3],
                             number,
                             DateTime.ParseExact(lines[1], "s", CultureInfo.CurrentCulture));
        }


        private string Build_event_filename(int number)
        {
            return string.Format(@"{0}\{1}.txt", _path, number);
        }
    }
}