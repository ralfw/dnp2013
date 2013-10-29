using System;
using NUnit.Framework;

namespace json.uiproxy.tests
{
    [TestFixture]
    public class test_JsonExtensions
    {
        [Test]
        public void Test()
        {
            var json = Properties.Resources.json1;
            dynamic obj = json.FromJson();

            json = JsonExtensions.ToJson(obj.payload);
            Console.WriteLine(json);
        } 
    }
}