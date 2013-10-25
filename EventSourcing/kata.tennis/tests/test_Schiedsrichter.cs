using System.IO;
using NUnit.Framework;
using simpleeventstore;

namespace kata.tennis.tests
{
    [TestFixture]
    public class test_Schiedsrichter
    {
        [Test]
        public void Akzeptanztest_für_1phasiges_Spiel()
        {
            var es = new InMemoryEventStore();
            es.Record("Aufschlag gewonnen", "A", "ctx");
            es.Record("Aufschlag gewonnen", "B", "ctx");
            es.Record("Aufschlag gewonnen", "A", "ctx");
            var sut = new Schiedsrichter("ctx", es);

            Assert.AreEqual("30:30", sut.Zählen(Schiedsrichter.Spieler.B));
            Assert.AreEqual("40:30", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("Spieler A gewinnt!", sut.Zählen(Schiedsrichter.Spieler.A));
        }

        [Test]
        public void Akzeptanztest_für_2phasiges_Spiel()
        {
            const string EVENTSTORE_PATH = "kata.tennis.eventstore";

            if (Directory.Exists(EVENTSTORE_PATH)) Directory.Delete(EVENTSTORE_PATH, true);

            var sut = new Schiedsrichter(new EventStore(EVENTSTORE_PATH));

            Assert.AreEqual("15:0", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("30:0", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("30:15", sut.Zählen(Schiedsrichter.Spieler.B));
            Assert.AreEqual("40:15", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("40:30", sut.Zählen(Schiedsrichter.Spieler.B));
            Assert.AreEqual("Einstand!", sut.Zählen(Schiedsrichter.Spieler.B));

            Assert.AreEqual("Vorteil A!", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("Einstand!", sut.Zählen(Schiedsrichter.Spieler.B));
            Assert.AreEqual("Vorteil B!", sut.Zählen(Schiedsrichter.Spieler.B));
            Assert.AreEqual("Einstand!", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("Vorteil A!", sut.Zählen(Schiedsrichter.Spieler.A));
            Assert.AreEqual("Spieler A gewinnt!", sut.Zählen(Schiedsrichter.Spieler.A));
        }
    }
}