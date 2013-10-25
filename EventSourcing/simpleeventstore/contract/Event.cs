using System;

namespace simpleeventstore.contract
{
    public struct Event
    {
        public readonly string Name;
        public readonly string Data;
        public readonly string Context;
        public readonly int Number;
        public readonly DateTime Timestamp;

        public Event(string name, string data, string context, int number, DateTime timestamp)
        {
            Name = name;
            Data = data;
            Context = context;
            Number = number;
            Timestamp = timestamp;
        }
    }
}