using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleeventstore.contract;

namespace simpleeventstore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly List<Event> _events; 

        public InMemoryEventStore()
        {
            _events = new List<Event>();
        }


        public int Record(string name, string data, string context)
        {
            //TODO: Thread-safety bzw. Mehrbenutzerfähigkeit einbauen
            var e = new Event(name, data, context, _events.Count, DateTime.Now);
            _events.Add(e);
            OnRecorded(e);
            return e.Number;
        }


        public IEnumerable<Event> Play()
        {
            return Play(0, int.MaxValue);
        }

        public IEnumerable<Event> Play(int fromNumber, int toNumber)
        {
            return _events.Where((_, i) => i >= fromNumber && i <= toNumber);
        }

        public IEnumerable<Event> Play(string context)
        {
            return Play(context, 0, int.MaxValue);
        }

        public IEnumerable<Event> Play(string context, int fromNumber, int toNumber)
        {
            var events_in_range = Play(fromNumber, toNumber);
            return events_in_range.Where(e => e.Context == context);
        }


        public event Action<Event> OnRecorded = _ => {};
    }
}
