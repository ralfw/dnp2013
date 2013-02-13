using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDDdemo
{
    [TestFixture]
    public class test_Wrapper20
    {
        [Test]
        public void Leerer_Text()
        {
            var resultat = WrapperV2.Wrap("", 42);
            Assert.AreEqual("", resultat);
        }

        [Test]
        public void Text_mit_Wort_kürzer_als_Zeilenlänge()
        {
            var resultat = WrapperV2.Wrap("wort", 42);
            Assert.AreEqual("wort", resultat);
        }

        [Test]
        public void Text_mit_Wort_länger_als_Zeilenlänge()
        {
            var resultat = WrapperV2.Wrap("wort", 2);
            Assert.AreEqual("wo\nrt", resultat);
        }


        [Test]
        public void Text_mit_Wort_länger_als_mehrere_Zeilen()
        {
            var resultat = WrapperV2.Wrap("wortwort", 3);
            Assert.AreEqual("wor\ntwo\nrt", resultat);
        }

        [Test]
        public void Text_mit_Umbruch_auf_Leerzeichen()
        {
            var resultat = WrapperV2.Wrap("wort wort", 5);
            Assert.AreEqual("wort\nwort", resultat);
        }

        [Test]
        public void Text_mit_Umbruch_direkt_nach_Wort()
        {
            var resultat = WrapperV2.Wrap("wort wort", 4);
            Assert.AreEqual("wort\nwort", resultat);
        }

        [Test]
        public void Text_mit_Umbruch_in_Wort_nach_Leerzeichen()
        {
            var resultat = WrapperV2.Wrap("wort wort", 6);
            Assert.AreEqual("wort\nwort", resultat);
        }

        [TestCase("wort wort wort", 6, Result = "wort\nwort\nwort")]
        [TestCase("wort wort wort", 11, Result = "wort wort\nwort")]
        public string Akzeptanzfälle(string text, int zeilenlänge)
        {
            return WrapperV2.Wrap(text, zeilenlänge);
        }
    }

    class WrapperV2
    {
        public static string Wrap(string text, int zeilenlänge)
        {
            return Wrap("", text, zeilenlänge);
        }

        static string Wrap(string umgebrochen, string text, int zeilenlänge)
        {
            if (text == "") return umgebrochen;

            var abbruch = Zeile_abbrechen(text, zeilenlänge);
            abbruch = Abbruch_bei_Leerzeichen_präferieren(abbruch);
            umgebrochen = Umbrechen(umgebrochen, abbruch.Zeile);

            return Wrap(umgebrochen, abbruch.Rest, zeilenlänge);
        }

        static dynamic Zeile_abbrechen(string text, int zeilenlänge)
        {
            dynamic abbruch = new ExpandoObject();
            abbruch.Zeile = text.Substring(0, text.Length < zeilenlänge
                                                ? text.Length
                                                : zeilenlänge);
            abbruch.Rest = zeilenlänge >= text.Length
                            ? ""
                            : text.Substring(zeilenlänge);
            return abbruch;
        }

        static dynamic Abbruch_bei_Leerzeichen_präferieren(dynamic abbruch)
        {
            if (!abbruch.Zeile.EndsWith(" ") && !abbruch.Rest.StartsWith(" "))
            {
                var index_letztes_leerzeichen = abbruch.Zeile.LastIndexOf(" ");
                if (index_letztes_leerzeichen > 0)
                {
                    var wortkopf = abbruch.Zeile.Substring(index_letztes_leerzeichen + 1);
                    abbruch.Zeile = abbruch.Zeile.Substring(0, index_letztes_leerzeichen);
                    abbruch.Rest = wortkopf + abbruch.Rest;
                }
            }

            abbruch.Zeile = abbruch.Zeile.Trim();
            abbruch.Rest = abbruch.Rest.TrimStart();

            return abbruch;
        }

        static string Umbrechen(string umgebrochen, string zeile)
        {
            return umgebrochen + (umgebrochen == "" ? "" : "\n") + zeile;
        }
    }
}
