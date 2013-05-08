using System;
using System.Collections.Generic;
using System.Linq;

namespace heckle.console
{
    public class Credentials
    {
        public static Credentials Parse(string text)
        {
            return new Credentials(
                text.Split(new[]{"\r\n"},StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Split('='))
                    .Select(kv => new {Key=kv[0], Value=kv[1]})
                    .Aggregate(new Dictionary<string,string>(), (dict, kv) => { dict.Add(kv.Key, kv.Value); return dict; }));
        }


        private readonly Dictionary<string, string> _parameters;

        private Credentials(Dictionary<string, string> parameters) { _parameters = parameters; }

        public string this[string key]
        {
            get { return _parameters[key]; }
        }
    }
}