using System;
using System.Collections.Generic;

namespace simpleeventstore.contract
{
    public interface IEventStore
    {
        int Record(string name, string data, string context);

        IEnumerable<Event> Play();
        IEnumerable<Event> Play(int fromNumber, int toNumber);
        IEnumerable<Event> Play(string context);
        IEnumerable<Event> Play(string context, int fromNumber, int toNumber);

        event Action<Event> OnRecorded;
    }
}