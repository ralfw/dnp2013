using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDDdemo
{
    [TestFixture]
    public class test_Wrapper
    {
        [Test]
        public void Leerer_Text()
        {
            var resultat = Wrapper.Wrap("", 42);
            Assert.AreEqual("", resultat);
        }

        [Test]
        public void Text_mit_Wort_kürzer_als_Zeilenlänge()
        {
            var resultat = Wrapper.Wrap("wort", 4);
            Assert.AreEqual("wort", resultat);
        }

        [Test]
        public void Text_mit_Wort_länger_als_Zeilenlänge()
        {
            var resultat = Wrapper.Wrap("wort", 2);
            Assert.AreEqual("wo\nrt", resultat);
        }

        [Test]
        public void Text_mit_Wort_länger_als_mehrere_Zeilen()
        {
            var resultat = Wrapper.Wrap("wortwort", 3);
            Assert.AreEqual("wor\ntwo\nrt", resultat);
        }

        [Test]
        public void Text_mit_Umbruch_auf_Leerzeichen()
        {
            var resultat = Wrapper.Wrap("wort wort", 5);
            Assert.AreEqual("wort\nwort", resultat);            
        }

        [Test]
        public void Text_mit_Umbruch_direkt_nach_Wort()
        {
            var resultat = Wrapper.Wrap("wort wort", 4);
            Assert.AreEqual("wort\nwort", resultat);
        }

        [Test]
        public void Text_mit_Umbruch_in_Wort_nach_Leerzeichen()
        {
            var resultat = Wrapper.Wrap("wort wort", 6);
            Assert.AreEqual("wort\nwort", resultat);
        }

        [TestCase("wort wort wort", 6, Result = "wort\nwort\nwort")]
        [TestCase("wort wort wort", 11, Result = "wort wort\nwort")]
        public string Akzeptanzfälle(string text, int zeilenlänge)
        {
            return Wrapper.Wrap(text, zeilenlänge);
        }
    }


    public class Wrapper
    {
        public static string Wrap(string text, int zeilenlänge)
        {
            return Wrap("", text, zeilenlänge);
        }

        private static string Wrap(string umgebrochen, string text, int zeilenlänge)
        {
            if (text == "") return umgebrochen;

            var zeile = text.Substring(0, Math.Min(text.Length, zeilenlänge));
            var rest = zeilenlänge >= text.Length
                        ? ""
                        : text.Substring(zeilenlänge, text.Length - zeile.Length);

            if (!zeile.EndsWith(" ") && !rest.StartsWith(" "))
            {
                var index_of_last_whitespace = zeile.LastIndexOf(" ");
                if (index_of_last_whitespace > 0)
                {
                    var wortkopf = zeile.Substring(index_of_last_whitespace + 1);
                    zeile = zeile.Substring(0, index_of_last_whitespace);
                    rest = wortkopf + rest;
                }
            }

            zeile = zeile.Trim();
            rest = rest.Trim();

            umgebrochen += (umgebrochen == "" ? "" : "\n") + zeile;

            return Wrap(umgebrochen, rest, zeilenlänge);
        }
    }
}
