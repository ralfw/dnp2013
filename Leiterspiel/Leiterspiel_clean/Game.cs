using System;
using System.Collections.Generic;

namespace Leiterspiel
{
    class Game
    {
        int CurrentPlayerNumber = -1;
        Player CurrentPlayer;
        List<Player> Players = new List<Player>();
        Board board;

        public Game(Board board)
        {
            this.board = board;
            Initialize();
            NextPlayer();
            while (!PlayStep())
            {
                NextPlayer();
            }
            Finalize();
        }

        private void Initialize()
        {
            Console.WriteLine(string.Format("Spielbrett mit {0} Zeilen und {1} Spalten. Sieger ist, wer zuerst Feld {2} erreicht hat",
                                            board.Zeilen, board.Spalten, board.Zeilen * board.Spalten));

            Console.WriteLine("Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]");

            int NumberOfPlayers = int.Parse(Console.ReadLine());
            for (int i = 0; i < NumberOfPlayers; i++) Players.Add(new Player());
        }

        private void Finalize()
        {
            Console.WriteLine(string.Format("Spieler {0} hat gewonnen!!!! Gratulation. ", CurrentPlayerNumber));
            Console.ReadLine();
        }

        public void NextPlayer()
        {
            CurrentPlayerNumber = (CurrentPlayerNumber + 1) % Players.Count;
            CurrentPlayer = Players[CurrentPlayerNumber];
        }

        private bool PlayStep()
        {
            int draw = 0;
            string drawstring = "";
            do
            {
                Console.WriteLine(string.Format("Spieler {0}: Position {1}. Gewürfelte Augenzahl: ", CurrentPlayerNumber, CurrentPlayer.Position));
                drawstring = Console.ReadKey().KeyChar.ToString();
        
            } while (!int.TryParse(drawstring, out draw) || (draw < 1 || draw > 6));
     
            CalculateStep(draw);
            Console.WriteLine();
            return HasWon();
        }

        private void CalculateStep(int draw)
        {
            CurrentPlayer.Position = board.CalculateNewPosition(CurrentPlayer.Position + draw);
        }

        private bool HasWon()
        {
            return CurrentPlayer.Position >= board.Zeilen * board.Spalten;
        }
    }
}