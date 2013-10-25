using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace diinjection
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Container();
            c.Register<IProvider>(() => new Provider2());
            var i = c.GetInstance<Integration>();

            Console.WriteLine(i.Calc(2,3));
        }
    }

    class Integration
    {
        private readonly Logic _logic;
        private readonly IProvider _provider;

        public Integration(Logic logic, IProvider provider)
        {
            _logic = logic;
            _provider = provider;
        }

        public int Calc(int a, int b)
        {
            return _logic.Mult(a, b) - _provider.Add(a, b);
        }
    }


    public interface IProvider
    {
        int Add(int a, int b);
    }


    internal class Provider : IProvider
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    internal class Provider2 : IProvider
    {
        public int Add(int a, int b)
        {
            return 2 * a + 2 * b;
        }
    }

    internal class Logic
    {
        public int Mult(int a, int b)
        {
            return a*b;
        }
    }
}
