using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Leiterspiel.tests
{
    [TestFixture]
    public class test_Board
    {
        [Test]
        public void Deserialisation()
        {
            var sut = new Board("leiterspielbrett1.txt");
            Assert.AreEqual(5, sut.Zeilen);
            Assert.AreEqual(6, sut.Spalten);
            Assert.AreEqual(8, sut.Moves.Count);
        }
    }
}
