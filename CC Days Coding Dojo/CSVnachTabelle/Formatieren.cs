using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVnachTabelle
{
    public class Formatieren
    {
        public string[] Formatiere_als_Tabelle(string[] zeilen)
        {
            var records = Parsen(zeilen);
            var spaltenbreiten = Spaltenbreiten_bestimmen(records);
            var trennzeile = Trennzeile_bauen(spaltenbreiten);
            records = Werte_auffüllen(records, spaltenbreiten);
            return Tabelle_bauen(records, trennzeile);
        }


        internal IEnumerable<string[]> Parsen(string[] zeilen)
        {
            return zeilen.Select(z => z.Split(';'));
        }


        internal int[] Spaltenbreiten_bestimmen(IEnumerable<string[]> records)
        {
            var spaltenbreiten = new int[records.First().Length];
            foreach (var r in records)
                for (var i = 0; i < spaltenbreiten.Length; i++)
                    if (spaltenbreiten[i] < r[i].Length) spaltenbreiten[i] = r[i].Length;
            return spaltenbreiten;
        }


        internal string Trennzeile_bauen(int[] spaltenbreiten)
        {
            var trennzeile = "+";
            spaltenbreiten.ToList().ForEach(sb => trennzeile += "".PadRight(sb, '-') + "+");
            return trennzeile;
        }


        internal IEnumerable<string[]> Werte_auffüllen(IEnumerable<string[]> records, int[] spaltenbreiten)
        {
            return records.Select(r => spaltenbreiten.Select((t, i) => r[i].PadRight(t)).ToArray());
        }


        internal string[] Tabelle_bauen(IEnumerable<string[]> records, string trennzeile)
        {
            var tabelle = new List<string>();
            tabelle.Add(trennzeile);
            records.ToList().ForEach(r => tabelle.Add(Tabellenzeile_bauen(r)));
            tabelle.Insert(2, trennzeile);
            tabelle.Add(trennzeile);
            return tabelle.ToArray();
        }

        private string Tabellenzeile_bauen(string[] record)
        {
            string zeile = "|";
            record.ToList().ForEach(s => zeile += s + "|");
            return zeile;
        }
    }
}
