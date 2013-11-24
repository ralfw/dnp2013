using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Leiterspiel
{
    internal class Board
    {
        public int Zeilen { get; set; }
        public int Spalten { get; set; }
        internal Dictionary<int, int> Moves = new Dictionary<int, int>();

        public Board(string filename)
        {
            Load(filename);
        }

        public int CalculateNewPosition(int oldposition)
        {
            int j = 0;
            if (Moves.TryGetValue(oldposition, out j))
            {
                return j;
            }
            else return oldposition;
        }

        public void Load(string Filename)
        {
            var sr = new StringReader(File.ReadAllText(Filename));
            string line;
            while((line = sr.ReadLine()) != null)
                Load_line(line);
        }

        private void Load_line(string line)
        {
            if (line.IndexOf("=") >= 0)
            {
                string[] parts = line.Split('=');
                if (parts[0].Trim() == "Spalten") Spalten = int.Parse(parts[1].Trim());
                if (parts[0].Trim() == "Zeilen") Zeilen = int.Parse(parts[1].Trim());
                if (parts[0].Trim() == "Leiter" || parts[0].Trim() == "Schlange")
                {
                    string[] fields = parts[1].Split(',');
                    Moves.Add(int.Parse(fields[0].Trim()), int.Parse(fields[1].Trim()));
                }
            }
        }
    }
}