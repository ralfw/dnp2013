using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leiterspiel.domain;
using NUnit.Framework;

namespace Leiterspiel.tests
{
    [TestFixture]
    public class test_Board
    {
        [Test]
        public void Deserialisation()
        {
            var text = File.ReadAllText("leiterspielbrett1.txt");

            var sut = Board.Parse(text);
            
            Assert.AreEqual(5, sut.Zeilen);
            Assert.AreEqual(6, sut.Spalten);
            Assert.AreEqual(8, sut.Moves.Count);
        }
    }
}
