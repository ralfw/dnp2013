using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using ImpromptuInterface;

namespace jsonserialization
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic o;
            string json;
            o = "{'name':'value', 'array':['a', 'b', {'baz': 99}], 'obj': {'foo': 42}}".FromJson();
            Console.WriteLine(o.name);
            Console.WriteLine(o.array[1]);
            Console.WriteLine(o.obj.foo);

            Console.WriteLine(JsonExtensions.ToJson(o));

            o = new ExpandoObject();
            o.name = "value2";
            o.array = new[] { "a2", "b2" };
            o.obj = new ExpandoObject();
            o.obj.foo = 422;
            json = JsonExtensions.ToJson(o);
            Console.WriteLine(json);

            var b = new Bar {name="hello", ages=new[]{1, 2, 3}, bar = new Bar{name="world"}};
            Console.WriteLine(b.ToJson());
        }
    }


    class Bar
    {
        public string name;
        public int[] ages;
        public Bar bar;
    }
}
