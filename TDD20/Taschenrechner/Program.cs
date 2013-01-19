using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Taschenrechner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    class Zahlenakku
    {
        public void Füge_Ziffer_hinzu(int ziffer) {}
        public void Setze_Zahl(int zahl) {}

        public event Action<int> Aktuelle_Zahl;
    }

    class Rechenwerk
    {
        public void Berechne(char op) {}
        public void Setze_Top_Operand(int zahl) {}

        public event Action<int> Aktuelles_Ergebnis;
    }
}
