using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Leiterspiel
{
    internal class Board
    {
        public static Board Parse(string text)
        {
            var board = new Board();
            board.Deserialize_from_text(text);
            return board;
        }


        public int Zeilen { get; set; }
        public int Spalten { get; set; }
        internal Dictionary<int, int> Moves = new Dictionary<int, int>();


        public int CalculateNewPosition(int oldposition)
        {
            int j = 0;
            if (Moves.TryGetValue(oldposition, out j))
            {
                return j;
            }
            else return oldposition;
        }


        private void Deserialize_from_text(string text)
        {
            var sr = new StringReader(text);
            string line;
            while((line = sr.ReadLine()) != null)
                Deserialize_from_line(line);
        }

        private void Deserialize_from_line(string line)
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