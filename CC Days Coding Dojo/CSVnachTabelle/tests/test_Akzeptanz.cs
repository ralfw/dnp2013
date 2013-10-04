using NUnit.Framework;

namespace CSVnachTabelle.tests
{
    [TestFixture]
    public class test_Akzeptanz
    {
        [Test]
        public void Beispiel() // 18:43
        {
            var input = new[] {"Name;Straße;Ort",
                               "Peter;Am Hang 5;12345 Ort",
                               "Paul;Weg 9;54321 Stadt"};

            var sut = new Formatieren();
            var output = sut.Formatiere_als_Tabelle(input);

            Assert.That(output, Is.EqualTo(new[]{"+-----+---------+-----------+",
                                                 "|Name |Straße   |Ort        |",
                                                 "+-----+---------+-----------+",
                                                 "|Peter|Am Hang 5|12345 Ort  |",
                                                 "|Paul |Weg 9    |54321 Stadt|",
                                                 "+-----+---------+-----------+"}));
        } 
    }
}