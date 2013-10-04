using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandLine
{
    public class CommandLine
    {
        public string Filename()
        {
            var commandLineArgs = Environment.GetCommandLineArgs();

            return commandLineArgs.Skip(1).First();
        }
    }
}
