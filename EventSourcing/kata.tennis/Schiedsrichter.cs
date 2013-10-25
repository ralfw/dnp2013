using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simpleeventstore;
using simpleeventstore.contract;

namespace kata.tennis
{
    public class Schiedsrichter
    {
        public enum Spieler { A, B };


        private readonly string _kontext;
        private readonly IEventStore _eventStore;

        public Schiedsrichter(IEventStore eventStore) : this(Guid.NewGuid().ToString(), eventStore) {}
        internal Schiedsrichter(string kontext, IEventStore eventStore)
        {
            _kontext = kontext; 
            _eventStore = eventStore;
        }


        public string Zählen(Spieler aufschlaggewinner)
        {
            _eventStore.Record("Aufschlag gewonnen", aufschlaggewinner.ToString(), _kontext);

            var spielstand = "";
            Spielphase_bestimmen(
                () => spielstand = Zählung_für_Phase1(),
                () => spielstand = Zählung_für_Phase2()
                );

            return spielstand;
        }


        private void Spielphase_bestimmen(Action bis_Einstand, Action nach_Einstand)
        {
            if (_eventStore.Play(_kontext).Any(e => e.Name == "Einstand erzielt"))
                nach_Einstand();
            else
                bis_Einstand();
        }


        private string Zählung_für_Phase1()
        {
            var punkte_SpielerA = Punkte_zählen_für("A");
            var punkte_SpielerB = Punkte_zählen_für("B");
            return Spielstand_nach_Punkten(punkte_SpielerA, punkte_SpielerB);
        }




        private int Punkte_zählen_für(string spieler)
        {
            return new[]{0, 15, 30, 40, 100}[_eventStore.Play(_kontext).Count(e => e.Data == spieler)];
        }

        private string Spielstand_nach_Punkten(int punkte_SpielerA, int punkte_SpielerB)
        {
            if ((punkte_SpielerA + punkte_SpielerB) > 100)
            {
                var gewinner = punkte_SpielerA == 100 ? "A" : "B";
                _eventStore.Record("Spiel gewonnen", gewinner, _kontext);
                return string.Format("Spieler {0} gewinnt!", gewinner);
            }

            if (punkte_SpielerA != 40 || punkte_SpielerB != 40)
                return string.Format("{0}:{1}", punkte_SpielerA, punkte_SpielerB);

            _eventStore.Record("Einstand erzielt", "", _kontext);
            return "Einstand!";
        }


        private string Zählung_für_Phase2()
        {
            var letzte_Aufschlaggewinne = Die_letzten_beiden_Ereignisse_ermitteln();
            return Spielstand_nach_Ereignisvergleich(letzte_Aufschlaggewinne[0], letzte_Aufschlaggewinne[1]);
        }

        private Event[] Die_letzten_beiden_Ereignisse_ermitteln()
        {
            var spielereignisse = _eventStore.Play(_kontext).ToArray();
            return spielereignisse.Where((_, i) => i >= spielereignisse.Length-2).ToArray();
        }
 
        private string Spielstand_nach_Ereignisvergleich(Event vorletzter, Event letzter)
        {
            if (vorletzter.Name == "Einstand erzielt")
                return string.Format("Vorteil {0}!", letzter.Data);

            if (letzter.Data == vorletzter.Data)
            {
                _eventStore.Record("Spiel gewonnen", letzter.Data, _kontext);
                return string.Format("Spieler {0} gewinnt!", letzter.Data);
            }

            _eventStore.Record("Einstand erzielt", "", _kontext);
            return "Einstand!";
        }
    }
}
