using System;
using System.IO;
using System.Threading.Tasks;

namespace heckle.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Zwischenruf: ");
            var zwischenruf = Console.ReadLine();

            Console.Write("Betreff: ");
            var betreff = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();


            var c = Credentials.Parse(File.ReadAllText(@"..\..\..\..\unversioned\pushover.credentials.txt"));

            var po = new Pushover(c["apptoken"], c["userkey"]);
            po.Send(betreff, zwischenruf + " [" + (name == "" ? "anonym" : name) + "]", "");
        }
    }
}
