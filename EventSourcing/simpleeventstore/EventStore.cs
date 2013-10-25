using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleeventstore.contract;

namespace simpleeventstore
{
    public class EventStore : IEventStore
    {
        private readonly string _path;
        private readonly EventPersistenceProvider _persistenceProvider;
        internal int _currentNumber;

        public EventStore(string path)
        {
            _path = path;
            Directory.CreateDirectory(path);
            _persistenceProvider = new EventPersistenceProvider(path);
            _currentNumber = Find_max_event_number();
        }


        public int Record(string name, string data, string context)
        {
            //TODO: Thread-safety bzw. Mehrbenutzerfähigkeit einbauen
            //Es sollte gleichzeitig von verschiedenen Quellen in einen EventStore auf einer File Share geschrieben werden können.
            ++_currentNumber;
            var e = new Event(name, data, context, _currentNumber, DateTime.Now);
            _persistenceProvider.Write_event(e);
            OnRecorded(e);
            return _currentNumber;
        }


        public IEnumerable<Event> Play()
        {
            return Play(0, int.MaxValue);
        }

        public IEnumerable<Event> Play(int fromNumber, int toNumber)
        {
            var event_numbers_in_range = Directory.GetFiles(_path)
                                                  .Select(Path.GetFileNameWithoutExtension)
                                                  .Select(int.Parse)
                                                  .Where(n => n >= fromNumber && n <= toNumber)
                                                  .OrderBy(n => n);
            return event_numbers_in_range.Select(_persistenceProvider.Read_event);
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

        
        private int Find_max_event_number()
        {
            return Directory.GetFiles(_path)
                            .Select(Path.GetFileNameWithoutExtension)
                            .Select(int.Parse)
                            .Union(new[]{-1})
                            .Max();
        }
    }
}
