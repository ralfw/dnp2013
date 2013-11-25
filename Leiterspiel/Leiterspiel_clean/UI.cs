﻿using System;
using System.Collections.Generic;

namespace Leiterspiel
{
    /*
     * Interaktionen
     *      - Start. in: dateiname, out: zeilenzahl, spaltenzahl, zielfeldindex
     *      - Anzahl spieler setzen. in: n, out: -
     *      - Zug durchführen. in: augenzahl, out: spieler, position, spielstand
     */

    interface UI
    {
        event Action Started; //
        event Action<int> Number_of_players_entered; //
        event Action<int> Rolled_the_dice; //

        void Show(); //
        void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex); //
        void Update_player_position(int player, int position); //
        void Game_over(int winning_player); //
    }

    partial class Game : UI
    {
        #region UI
        private int Roll_dice()
        {
            int draw = 0;
            string drawstring = "";
            do
            {
                Console.Write(string.Format("Spieler {0}: Position {1}. Gewürfelte Augenzahl: ", _player, _position));
                drawstring = Console.ReadKey().KeyChar.ToString();
            } while (!int.TryParse(drawstring, out draw) || (draw < 1 || draw > 6));
            Console.WriteLine();
            return draw;
        }

        private void Declare_winner()
        {
            Console.WriteLine(string.Format("Spieler {0} hat gewonnen!!!! Gratulation. ", _current_player_index));
            Console.ReadLine();
        }
        #endregion

        #region Logic



        #endregion

        #region IUI

        private int _number_of_rows, _number_of_cols, _goalIndex;


        public event Action Started;
        public event Action<int> Number_of_players_entered;
        public event Action<int> Rolled_the_dice;

        public void Show()
        {
            Started();
            Message_loop();
        }

        private void Message_loop()
        {
            Ask_for_number_of_players();
            NextPlayer();
            while (!PlayStep())
            {
                NextPlayer();
            }
            Declare_winner();
        }

        private void Ask_for_number_of_players()
        {
            Console.WriteLine(string.Format("Spielbrett mit {0} Zeilen und {1} Spalten. Sieger ist, wer zuerst Feld {2} erreicht hat",
                                            _number_of_rows, _number_of_cols, _goalIndex));

            Console.Write("Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]: ");
            var number_of_players = Console.ReadLine();

            Number_of_players_entered(int.Parse(number_of_players));
        }


        public void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex)
        {
            _number_of_rows = number_of_rows;
            _number_of_cols = number_of_cols;
            _goalIndex = goalIndex;
        }


        private int _player;
        private int _position;

        public void Update_player_position(int player, int position)
        {
            _player = player;
            _position = position;
        }

        public void Game_over(int winning_player)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}