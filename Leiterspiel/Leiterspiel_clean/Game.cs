using System;
using System.Collections.Generic;

namespace Leiterspiel
{
    /*
     * Interaktionen
     *      - Start. in: dateiname, out: zeilenzahl, spaltenzahl, zielfeldindex
     *      - Anzahl spieler setzen. in: n, out: -
     *      - Zug durchführen. in: augenzahl, out: spieler, position, spielstand
     */

    interface IUI
    {
        event Action Started;
        event Action<int> Number_of_players;
        event Action<int> Rolled_the_dice;

        void Show();
        void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex);
        void Let_the_game_begin();
        void Update_player_position(int player, int position);
        void Game_over(int winning_player);
    }

    class Game
    {
        int CurrentPlayerNumber = -1;
        Player CurrentPlayer;
        List<Player> Players = new List<Player>();
        Board board;


        public Game(Board board)
        {
            this.board = board;
        }

        public void Play()
        {
            Ask_for_number_of_players();
            NextPlayer();
            while (!PlayStep())
            {
                NextPlayer();
            }
            Declare_winner();
        }

        #region UI
        private void Ask_for_number_of_players()
        {
            Console.WriteLine(string.Format("Spielbrett mit {0} Zeilen und {1} Spalten. Sieger ist, wer zuerst Feld {2} erreicht hat",
                                            board.Zeilen, board.Spalten, board.Zeilen * board.Spalten));

            Console.Write("Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]: ");

            var NumberOfPlayers = int.Parse(Console.ReadLine());
            Set_number_of_players(NumberOfPlayers);
        }

        private int Roll_dice()
        {
            int draw = 0;
            string drawstring = "";
            do
            {
                Console.Write(string.Format("Spieler {0}: Position {1}. Gewürfelte Augenzahl: ", 
                                            CurrentPlayerNumber,
                                            CurrentPlayer.Position));
                drawstring = Console.ReadKey().KeyChar.ToString();
            } while (!int.TryParse(drawstring, out draw) || (draw < 1 || draw > 6));
            Console.WriteLine();
            return draw;
        }

        private void Declare_winner()
        {
            Console.WriteLine(string.Format("Spieler {0} hat gewonnen!!!! Gratulation. ", CurrentPlayerNumber));
            Console.ReadLine();
        }
        #endregion

        #region Logic
        private void Set_number_of_players(int NumberOfPlayers)
        {
            for (int i = 0; i < NumberOfPlayers; i++) Players.Add(new Player());
        }


        public void NextPlayer()
        {
            CurrentPlayerNumber = (CurrentPlayerNumber + 1) % Players.Count;
            CurrentPlayer = Players[CurrentPlayerNumber];
        }

        private bool PlayStep()
        {
            var draw = Roll_dice();
            CalculateStep(draw);
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
        #endregion
    }
}