using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace simpleeventstore.tests
{
    [TestFixture]
    public class test_EventStore
    {
        [Test]
        public void Find_max_number_in_empty_store()
        {
            var sut = new EventStore("testdir\\teststore");
            Assert.AreEqual(-1, sut._currentNumber);
        }


        [Test]
        public void Record_event()
        {
            if (Directory.Exists("teststore")) Directory.Delete("teststore", true);
            var sut = new EventStore("teststore");

            var number = sut.Record("n", "d\ne", "c");
            Assert.AreEqual(0, number);

            var txt = File.ReadLines("teststore\\0.txt").ToArray();
            Assert.AreEqual("0", txt[0]);
            Assert.AreEqual("n", txt[2]);
            Assert.AreEqual("c", txt[3]);
            Assert.AreEqual("d", txt[4]);
            Assert.AreEqual("e", txt[5]);
        }


        [Test]
        public void Read_event()
        {
            if (Directory.Exists("teststore")) Directory.Delete("teststore", true);
            var es = new EventStore("teststore");
            var number = es.Record("n", "d\ne", "c");

            var sut = new EventPersistenceProvider("teststore");
            var e = sut.Read_event(number);

            Assert.AreEqual("n", e.Name);
            Assert.AreEqual("c", e.Context);
            Assert.AreEqual("d\ne", e.Data);
            Assert.AreEqual(number, e.Number);
            Console.WriteLine(e.Timestamp);
        }


        [Test]
        public void Play_all_events()
        {
            if (Directory.Exists("teststore")) Directory.Delete("teststore", true);
            var sut = new EventStore("teststore");
            sut.Record("n0", "0", "c");
            sut.Record("n1", "1", "c");
            sut.Record("n2", "2", "c");

            var es = sut.Play().ToArray();

            Assert.AreEqual("0", es[0].Data);
            Assert.AreEqual("1", es[1].Data);
            Assert.AreEqual("2", es[2].Data);
        }

        [Test]
        public void Play_events_in_range()
        {
            if (Directory.Exists("teststore")) Directory.Delete("teststore", true);
            var sut = new EventStore("teststore");
            sut.Record("n0", "0", "c");
            sut.Record("n1", "1", "c");
            sut.Record("n2", "2", "c");
            sut.Record("n3", "3", "c");

            var es = sut.Play(1,2).ToArray();

            Assert.AreEqual("1", es[0].Data);
            Assert.AreEqual("2", es[1].Data);
            Assert.AreEqual(2, es.Length);
        }

        [Test]
        public void Play_context_events()
        {
            if (Directory.Exists("teststore")) Directory.Delete("teststore", true);
            var sut = new EventStore("teststore");
            sut.Record("n0", "0", "c1");
            sut.Record("n1", "1", "c");
            sut.Record("n2", "2", "c1");
            sut.Record("n3", "3", "c");

            var es = sut.Play("c").ToArray();

            Assert.AreEqual(2, es.Length);   
            Assert.AreEqual("1", es[0].Data);
            Assert.AreEqual("3", es[1].Data);
        }
    }
}