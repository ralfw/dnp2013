using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scratchpad
{
    class Umwelt
    {
        public void Konsumiere(object message)
        {
            
        }

        public void Konsum_anmelden<T>(Action<T> a)
        {
        }
    }
    
    class Program
    {
        static void Main()
        {
            var u = new Umwelt();
            u.Konsum_anmelden<string[]>(MainBlockingCollection);
        }







        static BlockingCollection<string> _bc = new BlockingCollection<string>();

        static void MainBlockingCollection(string[] args)
        {
            var t = new Task(() =>
                {
                    string s;
                    Console.WriteLine("<<");
                    while (_bc.TryTake(out s, Timeout.Infinite))
                    {
                        Console.WriteLine(s.ToUpper());
                    }
                    Console.Write(">>");
                });
            t.Start();

            while (true)
            {
                Console.Write("in:");
                var s = Console.ReadLine();
                if (s == "") break;

                _bc.Add(s);
            }

            _bc.CompleteAdding();

            Thread.Sleep(2000);
        }
    }
}
