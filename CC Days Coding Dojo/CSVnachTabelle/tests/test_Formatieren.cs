using NUnit.Framework;

namespace CSVnachTabelle.tests
{
    [TestFixture]
    public class test_Formatieren
    {
        [Test]
        public void Tabelle_bauen()
        {
            var records = new[]
                {
                    new[] {"A", "B"},
                    new[] {"1", "2"},
                    new[] {"10", "20"}
                };
            var sut = new Formatieren();

            var tabelle = sut.Tabelle_bauen(records, "---");

            Assert.That(tabelle, Is.EqualTo(new[]{"---",
                                                  "|A|B|",
                                                  "---",
                                                  "|1|2|",
                                                  "|10|20|",
                                                  "---"}));
        }


        [Test]
        public void Werte_auffüllen()
        {
            var records = new[]
                {
                    new[] {"A", "B"},
                    new[] {"1", "2"},
                    new[] {"10", "20"}
                };
            var spaltenbreiten = new[] {2, 3};
            var sut = new Formatieren();

            var aufgefüllt = sut.Werte_auffüllen(records, spaltenbreiten);

            Assert.That(aufgefüllt, Is.EqualTo(new[]
                {
                    new[]{"A ", "B  "},
                    new[]{"1 ", "2  "},
                    new[]{"10", "20 "}
                }));

        }


        [Test]
        public void Trennzeile_bauen()
        {
            var spaltenbreiten = new[] {1, 2};
            var sut = new Formatieren();

            var trennzeile = sut.Trennzeile_bauen(spaltenbreiten);

            Assert.That(trennzeile, Is.EqualTo("+-+--+"));
        }


        [Test]
        public void Parsen()
        {
            var zeilen = new[] {"a;b;c", "x;;y"};
            var sut = new Formatieren();

            var records = sut.Parsen(zeilen);

            Assert.That(records, Is.EqualTo(new[]
                {
                    new[]{"a", "b", "c"},
                    new[]{"x", "", "y"}
                }));
        }


        [Test]
        public void Spaltenbreiten_bestimmen()
        {
            var records = new[]
                {
                    new[]{"A", "BBB"},
                    new[]{"11", "22"}
                };
            var sut = new Formatieren();

            var spaltenbreiten = sut.Spaltenbreiten_bestimmen(records);

            Assert.That(spaltenbreiten, Is.EqualTo(new[]{2,3}));
        }
    }
}