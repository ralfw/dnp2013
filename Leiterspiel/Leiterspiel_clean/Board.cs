using System.Collections.Generic;
using System.IO;

namespace Leiterspiel
{
    class Board
    {
        public int Zeilen { get; set; }
        public int Spalten { get; set; }
        Dictionary<int, int> Moves = new Dictionary<int, int>();

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
            using (TextReader f = File.OpenText(Filename))
            {
                string line;
                while ((line = f.ReadLine()) != null)
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
    }
}